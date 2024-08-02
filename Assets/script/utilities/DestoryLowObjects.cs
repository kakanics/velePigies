using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryLowObjects : MonoBehaviour
{
    public int y;

    public float checkInterval = 2f; // Interval in seconds to check the position

    void Start()
    {
        StartCoroutine(CheckPositionPeriodically());
    }

    // Update m position hr frame pe check hogi, is script m time ka msla ni h late ho 
    //skta h lekin performance honi chahiye is lie hr kch seconds baad check kreinge position
    IEnumerator CheckPositionPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (transform.position.y < y)
            {
                Destroy(gameObject);
                yield break; // Exit the coroutine if the object is destroyed
            }
        }
    }
}
