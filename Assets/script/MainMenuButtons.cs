using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject BookletPanel, SettingsPanel, CreditsPanel, SoundManager;
    public Animator BookletAnimator, SettingsAnimator, CreditsAnimator;
    public Button soundToggleButton; // Reference to the button
    public AudioSource[] sources;
    public float lastPress = 0, minTimeBetweenPressBooklet = 0.5f;
    public Slider difficultySlider;
    public TextMeshProUGUI difficultyText, difficultyShadow;
    void Start()
    {
        int x = PlayerPrefs.GetInt("Sound", 0);
        foreach(var i in sources)
        {
            i.enabled = x == 0;
        }
        Color newColor = x == 1 ? Color.red : Color.white;
        ColorBlock cb = soundToggleButton.colors;
        cb.normalColor = newColor;
        cb.highlightedColor = newColor;
        cb.pressedColor = newColor;
        cb.selectedColor = newColor;
        soundToggleButton.colors = cb;
    }
    public void OpenSettings()
    {
        int x = PlayerPrefs.GetInt("Difficulty", 1);
        difficultySlider.value = x; 
        SettingsPanel.SetActive(true);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void OpenBooklet()
    {
        BookletPanel.SetActive(true);
        BookletAnimator.SetInteger("Page", 1);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void ExitCredits()
    {
        CreditsAnimator.SetTrigger("Close");
        Invoke("_exitCredits", 0.5f);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void ExitSettings()
    {
        SettingsAnimator.SetTrigger("Close");
        Invoke("_exitSettings", 0.5f);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void ExitBooklet()
    {
        BookletAnimator.SetInteger("Page", 0);
        Invoke("_exitBooklet", 0.5f);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void PlayGame()
    {
        GameObject.Destroy(SoundManager);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        soundMnaager.instance.PlaySound(SoundName.CLICK);
    }
    public void SoundToggle()
    {
        int x = PlayerPrefs.GetInt("Sound", 1);
        x = x == 1 ? 0 : 1;
        PlayerPrefs.SetInt("Sound", x); soundMnaager.instance.PlaySound(SoundName.CLICK);
                foreach(var i in sources)
        {
            i.enabled = x == 0;
        }
        Color newColor = x == 1 ? Color.red : Color.white;
        ColorBlock cb = soundToggleButton.colors;
        cb.normalColor = newColor;
        cb.highlightedColor = newColor;
        cb.pressedColor = newColor;
        cb.selectedColor = newColor;
        soundToggleButton.colors = cb;
    }
    public void BookletNext()
    {
        if (Time.time - lastPress < minTimeBetweenPressBooklet) { return; }
        int pg = BookletAnimator.GetInteger("Page") + 1;
        soundMnaager.instance.PlaySound(SoundName.CLICK);
        lastPress = Time.time;
        if (pg == 4) pg = 3;
        BookletAnimator.SetInteger("Page", pg);
    }
    public void BookletPrevious()
    {
        if (Time.time - lastPress < minTimeBetweenPressBooklet) { return; }
        int pf = BookletAnimator.GetInteger("Page") - 1;
        if (pf == 0) { return; }
        lastPress = Time.time;
        soundMnaager.instance.PlaySound(SoundName.CLICK);
        BookletAnimator.SetInteger("Page", pf);
    }
    public void _exitCredits()
    {
        CreditsPanel.SetActive(false);
    }
    public void _exitSettings()
    {
        SettingsPanel.SetActive(false);
    }
    public void _exitBooklet()
    {
        BookletPanel.SetActive(false);
    }
    public void changeSliderSetting(){
        soundMnaager.instance.PlaySound(SoundName.CLICK);
        int x = Mathf.RoundToInt(difficultySlider.value);
        difficultyText.text = "Difficulty: " + (x == 1 ? "Easy" : x == 2 ? "Normal" : "Hard");
        difficultyShadow.text = "Difficulty: " + (x == 1 ? "Easy" : x == 2 ? "Normal" : "Hard");
        PlayerPrefs.SetInt("Difficulty", x);
    }
}
