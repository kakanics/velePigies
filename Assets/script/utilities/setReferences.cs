using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setReferences : MonoBehaviour
{
    public Slingshot slingshot;
    worldShift worldShift;
    public hookSpawner hookSpawner;
    public animationMethods animationMethods;
    public scoreManager scoreManager;
    public powerUpSpawner powerUpSpawner;
    void Start()
    {
        worldShift=GetComponent<worldShift>();
        worldShift.animScript=animationMethods;
        
        slingshot.worldShift=worldShift;
        slingshot.hookSpawner=hookSpawner;
        slingshot.hookController=hookSpawner.gameObject.GetComponent<hookController>();
        slingshot.scoreManager=scoreManager;
        slingshot.powerUpSpawner=powerUpSpawner;

        scoreManager.GetComponent<comboManager>().animScript = animationMethods;
    }

}
