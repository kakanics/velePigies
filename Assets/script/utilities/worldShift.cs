using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldShift : MonoBehaviour
{
    public GameObject[] objects;
    public float shiftDuration = 0.1f;
    public float peakbkgScrollSpeed;
    [HideInInspector] public animationMethods animScript;
    void Start(){
        if(objects[0].GetComponent("Slingshot")==null){
            Debug.Log("The first object must be the player");
        }
    }
    public void shiftWorld()
    {
        float yOffset = -3.15f - objects[0].transform.position.y;
        StartCoroutine(ShiftWorldCoroutine(yOffset));
    }
    private IEnumerator ShiftWorldCoroutine(float yOffset)
    {
        animScript.triggerBkgScroll();
        Vector3[] startPositions = new Vector3[objects.Length];
        Vector3[] endPositions = new Vector3[objects.Length];
    
        // Record start and end positions
        for (int i = 0; i < objects.Length; i++)
        {
            startPositions[i] = objects[i].transform.position;
            endPositions[i] = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y + yOffset, objects[i].transform.position.z);
        }
    
        float elapsedTime = 0;
        float peakSpeed = peakbkgScrollSpeed; // Based on combo, this will be decreased
        float midpoint = shiftDuration / 2;
    
        while (elapsedTime < shiftDuration)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                float t = Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / shiftDuration);
                objects[i].transform.position = Vector3.Lerp(startPositions[i], endPositions[i], t);
            }
    
            // Adjust animation speed
            if (elapsedTime <= midpoint)
            {
                // Increase speed up to the midpoint
                animScript.bkg.speed = Mathf.Lerp(0f, peakSpeed, elapsedTime / midpoint);
            }
            else
            {
                // Decrease speed after the midpoint
                animScript.bkg.speed = Mathf.Lerp(peakSpeed, 0f, (elapsedTime - midpoint) / midpoint);
            }
    
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    
        // Ensure all objects are exactly at their end positions and reset animation speed
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.position = endPositions[i];
        }
    
        animScript.pauseBkgScroll();
    }
}
