using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotator : MonoBehaviour
{
    public float torqueRange = 100f; // Adjust this value to control the rotation speed

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Apply a random torque around each axis
            float torque = Random.Range(-torqueRange, torqueRange);
            rb.AddTorque(torque);
        }
    }
}
