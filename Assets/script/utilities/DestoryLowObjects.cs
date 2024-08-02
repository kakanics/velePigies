using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryLowObjects : MonoBehaviour
{
    public int y;

    void Update()
    {
        if(transform.position.y<y){
            Destroy(gameObject);
        }
    }
}
