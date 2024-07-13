using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setReferences : MonoBehaviour
{
    public Slingshot slingshot;
    public cameraMovement cameraMovement;
    worldShift worldShift;
    void Start()
    {
        worldShift=GetComponent<worldShift>();
        
        slingshot.worldShift=worldShift;
        slingshot.cameraFollow=cameraMovement;
        
        cameraMovement.followObject=slingshot.gameObject;
    }

}
