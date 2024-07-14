using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setReferences : MonoBehaviour
{
    public Slingshot slingshot;
    public cameraMovement cameraMovement; 
    worldShift worldShift;
    public hookSpawner hookSpawner;
    public animationMethods animationMethods;
    void Start()
    {
        worldShift=GetComponent<worldShift>();
        
        slingshot.worldShift=worldShift;
        slingshot.cameraFollow=cameraMovement;
        slingshot.hookSpawner=hookSpawner;

        cameraMovement.followObject=slingshot.gameObject;
        cameraMovement.animScript=animationMethods;
    }

}
