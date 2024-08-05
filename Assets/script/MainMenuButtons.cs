using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject BookletPanel, SettingsPanel, CreditsPanel, SoundManager;
    public Animator BookletAnimator, SettingsAnimator, CreditsAnimator;
    public Button soundToggleButton; // Reference to the button
    public AudioListener audioListener;
    public void OpenSettings(){SettingsPanel.SetActive(true);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void OpenCredits(){CreditsPanel.SetActive(true);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void OpenBooklet(){BookletPanel.SetActive(true);BookletAnimator.SetInteger("Page", 1);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void ExitCredits(){CreditsAnimator.SetTrigger("Close");Invoke("_exitCredits", 0.5f);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void ExitSettings(){SettingsAnimator.SetTrigger("Close");Invoke("_exitSettings", 0.5f);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void ExitBooklet(){BookletAnimator.SetInteger("Page", 0);Invoke("_exitBooklet", 0.5f);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void PlayGame(){GameObject.Destroy(SoundManager);SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void SoundToggle(){int x = PlayerPrefs.GetInt("Sound"); x=x==1?0:1;PlayerPrefs.SetInt("Sound", x);soundMnaager.instance.PlaySound(SoundName.CLICK);
    audioListener.enabled = x == 1;
    Color newColor = x == 1 ? Color.red : Color.white;
        ColorBlock cb = soundToggleButton.colors;
        cb.normalColor = newColor;
        cb.highlightedColor = newColor;
        cb.pressedColor = newColor;
        cb.selectedColor = newColor;
        soundToggleButton.colors = cb;}
    public void BookletNext(){int pg = BookletAnimator.GetInteger("Page")+1;soundMnaager.instance.PlaySound(SoundName.CLICK);if(pg==4)pg=3;BookletAnimator.SetInteger("Page", pg);}
    public void BookletPrevious(){int pf = BookletAnimator.GetInteger("Page")-1;if(pf==0){return;};soundMnaager.instance.PlaySound(SoundName.CLICK);BookletAnimator.SetInteger("Page", pf);}
    public void _exitCredits(){CreditsPanel.SetActive(false);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void _exitSettings(){SettingsPanel.SetActive(false);soundMnaager.instance.PlaySound(SoundName.CLICK);}
    public void _exitBooklet(){BookletPanel.SetActive(false);soundMnaager.instance.PlaySound(SoundName.CLICK);}
}
