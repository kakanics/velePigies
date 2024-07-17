using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This needs to be on the same object as the comboManager Script
public class scoreManager : MonoBehaviour
{

    public TextMeshProUGUI ScoreText;
    public int score;
    comboManager comboScript;
    void Start(){
        comboScript = GetComponent<comboManager>();
    }
    public void updateScore()
    {
        comboScript.comboIncrement();
        score += 1+comboScript.getComboVal()*comboScript.comboMultiplier;
        ScoreText.text = "Score: " + score.ToString();
    }
}
