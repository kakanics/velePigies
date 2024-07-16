using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationMethods : MonoBehaviour
{
    public Animator bkg;

    public void triggerBkgScroll(){
        bkg.SetTrigger("animStart");
    }
    public void pauseBkgScroll(){
        bkg.speed=0;
    }

}
