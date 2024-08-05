using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject powerUpHolder;
    public GameObject hookManager;
    public GameObject player;
    public Vector3[] spawnLocations = new Vector3[4] {new Vector3(-.75f,5.5f,0), new Vector3(.75f,5.5f,0), new Vector3(-1.5f,5.5f,0), new Vector3(1.5f,5.5f,0)};
    public float spawnInterval = 3;
    float timer = 3;
    public bool enablePin = true;
    
    void Start(){
        this.enabled = enablePin;
    }
    void Update(){
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            spawnPowerUps();
            timer = spawnInterval;
        }
    }
    public void spawnPowerUps()
    {
        // List<Vector3> spawnLocations = calculateLocations();
        foreach (var location in spawnLocations)
        {
            if(Random.Range(0,3) == 1)
            {
                Instantiate(powerUpPrefab, location, Quaternion.identity, powerUpHolder.transform);
            }
        }
    }

    public List<Vector3> calculateLocations()
    {
        List<Vector3> spawnLocations = new List<Vector3>();
        List<GameObject> spawnedHooks = hookManager.GetComponent<hookSpawner>().spawnedHooks;
        foreach (var hook in spawnedHooks)
        {
            spawnLocations.Add((player.transform.position + hook.transform.position)/2.0f);
        }
        return spawnLocations;
    }
}
