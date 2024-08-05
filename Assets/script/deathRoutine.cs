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
        soundMnaager.instance.PlaySound(SoundName.CLICK);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void mainMenu(){
        soundMnaager.instance.PlaySound(SoundName.CLICK);
        SceneManager.LoadScene(0);
    }
    public void startDeathRoutine(){
        if(scoreManager.score>PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore",scoreManager.score);
        }
        scoreText.text = "Score: " + scoreManager.score.ToString()+"\nHighScore: "+PlayerPrefs.GetInt("HighScore").ToString();
        deathUI.SetActive(true);
        soundMnaager.instance.PlaySound(SoundName.WOOSH);
        hooks.SetActive(false);
        powerUps.SetActive(false);
        player.SetActive(false);
        scoreCard.SetActive(false);
        comboCard.SetActive(false);
    }
}
