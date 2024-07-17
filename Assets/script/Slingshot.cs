using UnityEngine;
using System.Collections;
using TMPro;

public class Slingshot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance = 1f; // Maximum distance the slingshot can be pulled
    public float forceMultiplier = 10f;
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isFlying = false;
    public float dragRegionRadius = 0.1f;
    private Rigidbody2D rb;
    private Vector3 dragDirection;
    public int trajectoryPoints = 30; // Number of points to display in the trajectory (higher = longer trajectory)
    public float hookCatchThreshold = 0.1f; // Maximum velocity to catch the hook
    private bool isProcessingTrigger = false;
    public int initialWeight = 100;
    public TextMeshProUGUI weightText;
    
    // following variables set using setReferences script 
    [HideInInspector] public cameraMovement cameraFollow;
    [HideInInspector] public hookSpawner hookSpawner;
    [HideInInspector] public worldShift worldShift;
    [HideInInspector] public hookController hookController;
    [HideInInspector] public scoreManager scoreManager;
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.gravityScale = 0; // Disable gravity initially

        WeightManager.getInstance().playerWeight = initialWeight;
        weightText.text = initialWeight.ToString();
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isProcessingTrigger) return; // Exit if we're already processing a trigger event
    
        isProcessingTrigger = true;
    
        Debug.Log("caught");
        if (other.gameObject.tag == "hook" && rb.velocity.magnitude < hookCatchThreshold && !isDragging) // Check if the player is close to the hook and almost stopped
        {
            other.enabled = false;
            catchHook(other.transform.position);
            scoreManager.updateScore();
            if(transform.position.y>-2) // shift iff higher hook is caught
                StartCoroutine(ShiftWorldAfterDelay(0f)); // hooks spawned after the world is shifted
            hookController.setHook(other.gameObject);
        }
    
        // Reset the flag after a short delay
        StartCoroutine(ResetTriggerProcessing());
    }
    
    IEnumerator ResetTriggerProcessing()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
        isProcessingTrigger = false;
    }


    void catchHook(Vector3 hookPosition)
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.position = hookPosition;
        startPosition.x = hookPosition.x;
        isFlying = false;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (!isFlying)
        {
            if (Input.GetMouseButtonDown(0) && mousePosition.x < rb.position.x + dragRegionRadius && mousePosition.x > rb.position.x - dragRegionRadius && mousePosition.y < rb.position.y + dragRegionRadius && mousePosition.y < rb.position.y + dragRegionRadius)
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
                StartCoroutine(hookController.EnableHookDelay(0.3f));

                float pullRatio = dragDirection.magnitude / maxDistance;

                rb.AddForce(-dragDirection.normalized * forceMultiplier * pullRatio, ForceMode2D.Impulse);
                isFlying = true;
                rb.gravityScale = 1; // Enable gravity upon release
            }
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
        hookSpawner.spawnHooks();
        worldShift.shiftWorld();
    }
}
