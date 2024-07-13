using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public GameObject followObject;
    [SerializeField] private float offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, followObject.transform.position.y+offset, transform.position.z);        
    }
}
