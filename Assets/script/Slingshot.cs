using UnityEngine;
using System.Collections;
using TMPro;

public class Slingshot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance = 1f; // Maximum distance the slingshot can be pulled
    public float forceMultiplier = 10f;
    public float torque = 5f;
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isFlying = false;
    public bool isWorldShifting = false;
    public float dragRegionRadius = 0.1f;
    private Rigidbody2D rb;
    private Vector3 dragDirection;
    public int trajectoryPoints = 30; // Number of points to display in the trajectory (higher = longer trajectory)
    public float hookCatchThreshold = 0.1f; // Maximum velocity to catch the hook
    public int initialWeight = 100;
    public TextMeshProUGUI weightText;
    public float hookReleaseTime = 0.5f;
    // following variables set using setReferences script 
    [HideInInspector] public cameraMovement cameraFollow;
    [HideInInspector] public animationMethods animMethods;
    [HideInInspector] public hookSpawner hookSpawner;
    [HideInInspector] public worldShift worldShift;
    [HideInInspector] public hookController hookController;
    [HideInInspector] public scoreManager scoreManager;
    [HideInInspector] public deathRoutine deathRoutine;
    [HideInInspector] public playParticleSystem particleSystemScript;
    void Start()
    {
        Application.targetFrameRate = 60;

        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.gravityScale = 0; // Disable gravity initially
        rb.velocity = Vector2.zero;

        WeightManager.getInstance().playerWeight = initialWeight;
        animMethods.modifyImage(0);
        weightText.text = initialWeight.ToString();
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("hook") && rb.velocity.magnitude < hookCatchThreshold && !isDragging) // Check if the player is close to the hook and almost stopped
        {
            other.enabled = false;
            soundMnaager.instance.PlaySound(SoundName.CATCH);
            catchHook(other.gameObject);
            scoreManager.updateScore();
            if (transform.position.y > -2) // shift iff higher hook is caught
            {
                isWorldShifting = true;
                StartCoroutine(ShiftWorldAfterDelay(0f)); // hooks spawned after the world is shifted
            }
            hookController.setHook(other.gameObject);
        }
        else if (other.gameObject.CompareTag("power"))
        {
            particleSystemScript.PlayParticleSystemAtPosition(transform.position);
            var w = other.gameObject.GetComponent<powerupPower>().power;
            animMethods.modifyImage(w);
            WeightManager.getInstance().modifyWeight(w);
            weightText.text = WeightManager.getInstance().playerWeight.ToString();
            Destroy(other.gameObject);
            soundMnaager.instance.PlaySound(SoundName.POWERUP);
        }
        else if (other.gameObject.CompareTag("deathTrigger"))
        {
            soundMnaager.instance.PlaySound(SoundName.DEATH);
            soundMnaager.instance.PlaySound(SoundName.ELEDEATH);
            deathRoutine.startDeathRoutine();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("wall") && isFlying)
        {
            soundMnaager.instance.PlaySound(SoundName.HIT);
            animMethods.hurtPig();
        }
    }

    void catchHook(GameObject hook)
    {
        Vector3 hookPosition = hook.transform.position;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.position = hookPosition;
        startPosition.x = hookPosition.x;
        isFlying = false;

        if (hook.GetComponent<Hook>().getWeight() < WeightManager.getInstance().playerWeight)
        {
            StartCoroutine(releaseHook());
        }
    }

    IEnumerator releaseHook()
    {
        yield return new WaitForSeconds(hookReleaseTime);
        isFlying = true;
        soundMnaager.instance.PlaySound(SoundName.CRACK);
        rb.gravityScale = 1;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        if (!isFlying && !isWorldShifting)
        {
            Debug.Log(isWorldShifting);

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
                soundMnaager.instance.PlaySound(SoundName.THROW);
                isDragging = false;
                lineRenderer.enabled = false;
                StartCoroutine(hookController.EnableHookDelay(0.3f));

                float pullRatio = dragDirection.magnitude / maxDistance;

                rb.AddForce(-dragDirection.normalized * forceMultiplier * pullRatio, ForceMode2D.Impulse);
                isFlying = true;
                rb.gravityScale = 1; // Enable gravity upon release
            }
        }
        else
        {
            float RotDirection = Mathf.Sign(rb.velocity.x);
            rb.AddTorque(RotDirection * torque);
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
        // isWorldShifting = false;
    }
}
