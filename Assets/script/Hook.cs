using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private int weight = 100;

    public void setWeight(int _weight)
    {
        weight = _weight;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = weight.ToString();
    }
    public int getWeight()
    {
        return weight;
    }
}
