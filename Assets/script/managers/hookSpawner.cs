using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookSpawner : MonoBehaviour
{
    public GameObject hookPrefab;  
    public GameObject hookHolder;
    public int hookCount = 6;
    private int weightOffset = 50;
    public Vector3[] spawnLocations = new Vector3[] {new Vector3(-1.5f, 5.5f, 0), new Vector3(0, 5.5f, 0), new Vector3(1.5f, 5.5f, 0), new Vector3(-1f, 5.5f, 0), new Vector3(1f, 5.5f, 0)};
    [HideInInspector] public List<GameObject> spawnedHooks;
    [HideInInspector] public scoreManager scoreManager;
    public void spawnHooks()
    {
        bool b = false;
        spawnedHooks = new List<GameObject>();
        foreach(Vector3 locationa in spawnLocations)
        {
            Vector3 location;
            if(IsObjectAtPosition(locationa)){location=new Vector3(locationa.x, locationa.y+1, locationa.z);}else{location=locationa;}
            float score = scoreManager.score;
            float chance;
            if (score <= 1000)
            {
                // Linearly interpolate from 33% to 5%
                chance = Mathf.Lerp(0.33f, 0.05f, score / 1000f);
            }
            else
            {
                // After 1000, the chance remains at 5%
                chance = 0.05f;
            }
            if(UnityEngine.Random.value<chance){
               GameObject hook = Instantiate(hookPrefab, location, Quaternion.identity, hookHolder.transform);
                setHookWeight(hook, calculateWeight());
               b=true;
                spawnedHooks.Add(hook);
                Debug.Log("Hook Created");
            }
        }
        while (impossibleCondition(spawnedHooks) && spawnedHooks.Count > 0)
        {
            spawnedHooks[0].GetComponent<Hook>().setWeight(calculateWeight());
        }
        if (!b){
            spawnHooks();
        }
    }

    private void setHookWeight(GameObject hook, int weight)
    {
        hook.GetComponent<Hook>().setWeight(weight);
    }

    private int calculateWeight()
    {
        int playerWeight = WeightManager.getInstance().playerWeight;
        int lowerBound = playerWeight - weightOffset;
        lowerBound = lowerBound < 0 ? 0 : lowerBound; //Prevent weight being less than zero
        int upperBound = playerWeight + weightOffset + 5;
        return UnityEngine.Random.Range(lowerBound, upperBound);
    }

    private bool impossibleCondition(List<GameObject> hooks)
    {
        foreach(GameObject hook in hooks)
        {
            if (hook.GetComponent<Hook>().getWeight() >= WeightManager.getInstance().playerWeight)
                return false;
        }
        return true;
    }
    bool IsObjectAtPosition(Vector2 position)
    {
        // Check if there are any colliders at the specified position
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider != null;
    }
}
