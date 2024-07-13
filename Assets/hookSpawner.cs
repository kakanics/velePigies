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
            if(Random.RandomRange(0,3)==0){
               Instantiate(hookPrefab, location, Quaternion.identity, hookHolder.transform);
               b=true;
            }
        }
        if(!b){
            spawnHooks();
        }
    }
}
