using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationMethods : MonoBehaviour
{
    public Animator bkg;
    public Animator comboBox;
    [Header("Pig Animations")]
    public Animator smokeAnim;
    public GameObject pig1, pig2, pig3; // pig 3 = king, pig 2 = aba, pig 1 = small wala
    public Animator pigAnimator, abaAnimator, kingAnimator;
    public int thresholdKing = 100, thresholdBig = 60;
    public CircleCollider2D playerCol2D;
    private void Start() {
        int diff = PlayerPrefs.GetInt("Difficulty", 1);
        if (diff == 1){
            thresholdBig = 60;
            thresholdKing = 100;
        } else if (diff == 2){
            thresholdBig = 100;
            thresholdKing = 160;
        } else if (diff == 3){
            thresholdBig = 200;
            thresholdKing = 300;
        }
    }
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
    public void modifyImage(int weight)
    {
        int oldWeight = WeightManager.getInstance().playerWeight;
        int newWeight = oldWeight+weight;
        
        if (newWeight >= thresholdKing){
            pig2.SetActive(false);
            pig3.SetActive(true);
            smokeAnim.SetTrigger("puff");
        } 
        else if (newWeight >= thresholdBig){
            pig1.SetActive(false);
            pig2.SetActive(true);
            pig3.SetActive(false);
            smokeAnim.SetTrigger("puff");
        } else if (newWeight < thresholdBig && oldWeight>thresholdBig){
            pig2.SetActive(false);
            pig1.SetActive(true);
            smokeAnim.SetTrigger("puff");
        }
        if (newWeight<thresholdBig){
            //0.4 to 0.7 scale and 0.28 to 0.5 radius for weight 40 to 100
            newWeight = newWeight<30?30:newWeight;

            // Calculate scale and radius using linear interpolation
            float scale = Mathf.Lerp(0.4f, 0.7f, (newWeight - 30) / 30f);
            float radius = Mathf.Lerp(0.28f, 0.5f, (newWeight - 30) / 30f);

            // Apply the calculated scale and radius
            pig1.transform.localScale = new Vector3(scale, scale, scale);
            playerCol2D.radius = radius;
        }
    }
    public void hurtPig() {
        pigAnimator.SetTrigger("hurt");
        abaAnimator.SetTrigger("hurt");
        kingAnimator.SetTrigger("hurt");
    }
}
