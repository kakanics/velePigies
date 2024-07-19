using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManager
{
    public static WeightManager weightManager;
    public int playerWeight = 200;

    private WeightManager() { 
    }
    public static WeightManager getInstance()
    {
        if (weightManager == null)
        {
            WeightManager instance = new WeightManager();
            weightManager = instance;
        }
        return weightManager;
    }

    public void increaseWeight(int weight)
    {
        playerWeight += weight;
    }

    public void decreaseWeight(int weight)
    {
        playerWeight -= weight;
        if (playerWeight < 0)
        {
            playerWeight = 0;
        }
    }
}
