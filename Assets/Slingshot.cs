using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance = 1f; // Maximum distance the slingshot can be pulled
    public float forceMultiplier = 10f; 
    private Vector3 startPosition;
    private bool isDragging = false;
    private Rigidbody2D rb;
    private Vector3 dragDirection; 
    public int trajectoryPoints = 30; // Number of points to display in the trajectory
    


    void Start()
    {
        startPosition = transform.position; 
        rb = GetComponent<Rigidbody2D>(); 
        lineRenderer = GetComponent<LineRenderer>();
        rb.gravityScale = 0; // Disable gravity initially
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
        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        dragDirection = direction;
        float pullRatio = dragDirection.magnitude / maxDistance;

        // Adjust projectedVelocity based on the actual pull ratio
        Vector3 projectedVelocity = -dragDirection.normalized * forceMultiplier * pullRatio;

        // Update the ball's position to simulate the pull
        rb.position = startPosition + dragDirection;

        UpdateTrajectory(transform.position, projectedVelocity);
    }



        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            lineRenderer.enabled = false; // Disable the line renderer when dragging stops

            // Calculate the pull ratio based on the drag distance and the maximum distance
            float pullRatio = dragDirection.magnitude / maxDistance;

            // Apply force based on the pull ratio and the forceMultiplier
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
}
