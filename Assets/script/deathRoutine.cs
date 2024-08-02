using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class deathRoutine : MonoBehaviour
{
    public GameObject deathUI, hooks, powerUps, player, scoreCard, comboCard;
    public TextMeshProUGUI scoreText;
    public scoreManager scoreManager;
    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void startDeathRoutine(){
        if(scoreManager.score>PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore",scoreManager.score);
        }
        scoreText.text = "You Lost!\nScore: " + scoreManager.score.ToString()+"\nHighScore: "+PlayerPrefs.GetInt("HighScore").ToString();
        deathUI.SetActive(true);
        hooks.SetActive(false);
        powerUps.SetActive(false);
        player.SetActive(false);
        scoreCard.SetActive(false);
        comboCard.SetActive(false);
    }
}
