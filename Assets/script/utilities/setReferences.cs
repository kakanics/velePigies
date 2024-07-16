using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setReferences : MonoBehaviour
{
    public Slingshot slingshot;
    worldShift worldShift;
    public hookSpawner hookSpawner;
    public animationMethods animationMethods;
    void Start()
    {
        worldShift=GetComponent<worldShift>();
        worldShift.animScript=animationMethods;
        
        slingshot.worldShift=worldShift;
        slingshot.hookSpawner=hookSpawner;
        slingshot.hookController=hookSpawner.gameObject.GetComponent<hookController>();

    }

}
