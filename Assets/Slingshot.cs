using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance = 1f; // Maximum distance the slingshot can be pulled
    public float forceMultiplier = 10f;
    private Vector3 startPosition;
    private bool isDragging = false;
    public float dragRegionRadius = 0.1f;
    private Rigidbody2D rb;
    private Vector3 dragDirection;
    public int trajectoryPoints = 30; // Number of points to display in the trajectory (higher = longer trajectory)
    public float hookCatchThreshold = 0.1f; // Maximum velocity to catch the hook
    // following variables set using setReferences script 
    [HideInInspector] public cameraMovement cameraFollow;
    [HideInInspector] public hookSpawner hookSpawner;
    [HideInInspector] public worldShift worldShift;
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.gravityScale = 0; // Disable gravity initially
        hookSpawner.spawnHooks();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.tag == "hook" && rb.velocity.magnitude < hookCatchThreshold) // Check if the player is close to the hook and almost stopped
        {
            catchHook(other.transform.position);
            cameraFollow.MoveCamera();
            StartCoroutine(ShiftWorldAfterDelay(0.1f)); // hooks spawned after the world is shifted
            other.enabled = false;
        }
    }


    void catchHook(Vector3 hookPosition)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.position = hookPosition;
        startPosition.x = hookPosition.x;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButtonDown(0) && mousePosition.x < rb.position.x +dragRegionRadius && mousePosition.x > rb.position.x-dragRegionRadius && mousePosition.y < rb.position.y +dragRegionRadius && mousePosition.y < rb.position.y +dragRegionRadius)
        {
            isDragging = true;
            lineRenderer.enabled = true; // Enable the line renderer when dragging starts
        }

        if (isDragging)
        {
            Vector3 direction = mousePosition - startPosition;

            if (direction.magnitude > maxDistance) // limitation of how much can be pulled
            {
                direction = direction.normalized * maxDistance;
            }
            dragDirection = direction;



            // simulate the pull
            rb.position = startPosition + dragDirection;

            float pullRatio = dragDirection.magnitude / maxDistance;
            Vector3 projectedVelocity = -dragDirection.normalized * forceMultiplier * pullRatio;
            UpdateTrajectory(transform.position, projectedVelocity);
        }



        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            lineRenderer.enabled = false;

            float pullRatio = dragDirection.magnitude / maxDistance;

            rb.AddForce(-dragDirection.normalized * forceMultiplier * pullRatio, ForceMode2D.Impulse);
            rb.gravityScale = 1; // Enable gravity upon release
        }
    }
    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity)
    {
        Vector3[] points = new Vector3[trajectoryPoints];
        lineRenderer.positionCount = trajectoryPoints;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * 0.1f; // Adjust time scale if needed
            points[i] = initialPosition + initialVelocity * time + Physics.gravity * time * time / 2f;
            points[i].z = 0;
        }

        lineRenderer.SetPositions(points);
    }
    IEnumerator ShiftWorldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        worldShift.shiftWorld();
        hookSpawner.spawnHooks();
    }
}
