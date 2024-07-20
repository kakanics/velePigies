using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject powerUpHolder;
    public GameObject hookManager;
    public GameObject player;

    public void spawnPowerUps()
    {
        List<Vector3> spawnLocations = calculateLocations();
        foreach (var location in spawnLocations)
        {
            if(Random.Range(0,2) == 1)
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
