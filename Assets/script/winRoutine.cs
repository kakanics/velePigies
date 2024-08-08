using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winRoutine : MonoBehaviour
{
    public GameObject winUI;

    public void startWinRoutine()
    {
        Debug.Log("Game WOn!");
        winUI.SetActive(true);
    }

}