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
    public Vector3[] spawnLocations = new Vector3[] {new Vector3(-1.5f, 5.5f, 0), new Vector3(0, 5.5f, 0), new Vector3(1.5f, 5.5f, 0), new Vector3(-1.25f, 10.0f, 0), new Vector3(0, 10.0f, 0), new Vector3(1.5f, 12.5f, 0)};

    public void spawnHooks()
    {
        bool b = false;
        List<GameObject> spawned = new List<GameObject>();
        foreach(Vector3 location in spawnLocations)
        {
            if(UnityEngine.Random.Range(0,3)==0){
               GameObject hook = Instantiate(hookPrefab, location, Quaternion.identity, hookHolder.transform);
                setHookWeight(hook, calculateWeight());
               b=true;
                spawned.Add(hook);
                Debug.Log("Hook Created");
            }
        }
        while (impossibleCondition(spawned) && spawned.Count > 0)
        {
            spawned[0].GetComponent<Hook>().setWeight(calculateWeight());
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
        int offset = 50;
        int lowerBound = playerWeight - offset;
        lowerBound = lowerBound < 0 ? 0 : lowerBound; //Prevent weight being less than zero
        int upperBound = playerWeight + offset;
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
}
