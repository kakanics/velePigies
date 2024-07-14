using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldShift : MonoBehaviour
{
    public GameObject[] objects;
    void Start(){
        if(objects[0].GetComponent("Slingshot")==null){
            Debug.Log("The first object must be the player");
        }
    }
    public void shiftWorld(){
        float yOffset=-3.15f-objects[0].transform.position.y;
        foreach(GameObject obj in objects){
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y+yOffset, obj.transform.position.z);
        }
    }
}
