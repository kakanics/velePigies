using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookSpawner : MonoBehaviour
{
    public GameObject hookPrefab;  
    public GameObject hookHolder;
    public Vector3[] spawnLocations = new Vector3[3] {new Vector3(-1.5f, 5.5f, 0), new Vector3(0, 5.5f, 0), new Vector3(1.5f, 5.5f, 0)};

    public void spawnHooks()
    {
        bool b = false;
        foreach(Vector3 location in spawnLocations)
        {
            if(UnityEngine.Random.Range(0,3)==0){
               GameObject hook = Instantiate(hookPrefab, location, Quaternion.identity, hookHolder.transform);
                setHookWeight(hook);
               b=true;
            }
        }
        if(!b){
            spawnHooks();
        }
    }

    private void setHookWeight(GameObject hook)
    {
        int playerWeight = WeightManager.getInstance().playerWeight;
        int offset = 50;
        int lowerBound = playerWeight - offset;
        lowerBound = lowerBound < 0 ? 0 : lowerBound; //Prevent weight being less than zero
        int upperBound = playerWeight + offset;
        hook.GetComponent<Hook>().setWeight(UnityEngine.Random.Range(lowerBound, upperBound));
    }
}
