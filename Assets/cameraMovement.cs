using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [HideInInspector] public GameObject followObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;
    private bool move = false;
    private const float StopThreshold = 0.2f;

    public void MoveCamera()
    {
        move = true;
    }

    void LateUpdate()
    {
        if (!move) return;

        Vector3 desiredPosition = new Vector3(transform.position.x, followObject.transform.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (Mathf.Abs(transform.position.y - desiredPosition.y) < StopThreshold)
        {
            move = false;
        }
    }
}
