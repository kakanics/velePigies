using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance = 1f; // Maximum distance the slingshot can be pulled
    public float forceMultiplier = 10f; 
    private Vector3 startPosition;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private Vector3 dragDirection; 
    public int trajectoryPoints = 30; // Number of points to display in the trajectory (higher = longer trajectory)
    public float hookCatchThreshold = 0.1f; // Maximum velocity to catch the hook
    [HideInInspector]public cameraMovement cameraFollow;// Reference to the cameraFollow script, set using setReferences script 

    [HideInInspector]public worldShift worldShift; // Reference to the worldShift script, set using setReferences script
    void Start()
    {
        startPosition = transform.position; 
        rb = GetComponent<Rigidbody2D>(); 
        lineRenderer = GetComponent<LineRenderer>();
        rb.gravityScale = 0; // Disable gravity initially
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.tag == "hook" && rb.velocity.magnitude < hookCatchThreshold) // Check if the player is close to the hook and almost stopped
        {
            catchHook(other.transform.position);
            cameraFollow.MoveCamera();
            StartCoroutine(ShiftWorldAfterDelay(0.1f));
            other.enabled=false;
        }
    }
    

    void catchHook(Vector3 hookPosition)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.position = hookPosition;
        startPosition.x= hookPosition.x;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lineRenderer.enabled = true; // Enable the line renderer when dragging starts
        }

        if (isDragging)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

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



        if (Input.GetMouseButtonUp(0))
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
    }
}
