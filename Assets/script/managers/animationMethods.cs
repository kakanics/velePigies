using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationMethods : MonoBehaviour
{
    public Animator bkg;
    public Animator comboBox;

    public void triggerBkgScroll(){
        bkg.SetTrigger("animStart");
    }
    public void pauseBkgScroll(){
        bkg.speed=0;
    }
    public void ComboBoxAppearTrigger(){
        comboBox.SetTrigger("appear");
    }
    public void ComboBoxDisappearTrigger(){
        comboBox.SetTrigger("disappear");
    }

}
