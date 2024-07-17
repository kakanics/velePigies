using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This needs to be on the same object as the scoreManager Script
public class comboManager : MonoBehaviour
{
    public float timer = 2f; // this will reduce to 1 when combo reaches 10 and then remain there
    public int comboMultiplier = 5;
    int combo = 0;
    bool isInCombo = false;
    float l, r;
    [HideInInspector] public animationMethods animScript;
    public RectTransform comboBg;
    public TextMeshProUGUI comboText;

    private Coroutine comboTimerCoroutine = null;

    void Start()
    {
        // comboBox bg reset parameters value init
        l = comboBg.offsetMin.x;
        r = -comboBg.offsetMax.x;
    }
    public int getComboVal()=>combo;
    public void comboIncrement()
    {
        if (comboTimerCoroutine != null)
        {
            StopCoroutine(comboTimerCoroutine);
        }
        ResetComboBg();
        if (!isInCombo)
        {
            combo=1;
            animScript.ComboBoxAppearTrigger();
            isInCombo = true;
            comboTimerCoroutine = StartCoroutine(ComboTimer());
        } else
        {
            combo++;
            comboTimerCoroutine = StartCoroutine(ComboTimer());
        }
        updateComboText();
    }
    void updateComboText(){
        comboText.text = combo.ToString() + "X";
    }
    void ResetComboBg()
    {
        // Reset comboBg to its original left and right positions
        comboBg.offsetMin = new Vector2(l, comboBg.offsetMin.y);
        comboBg.offsetMax = new Vector2(r, comboBg.offsetMax.y);
    }
    IEnumerator ComboTimer()
    {
        float duration = timer - Mathf.Min(1, combo * 0.1f) - 1;
        float time = 0;
        float initialLeft = comboBg.offsetMin.x; // Left
        float initialRight = -comboBg.offsetMax.x; // Right
        
        RectTransform parentRect = comboBg.parent.GetComponent<RectTransform>();
        float parentWidth = parentRect.rect.width;
        float leftOffset = comboBg.offsetMin.x;
        float rightOffset = -comboBg.offsetMax.x;
        float rectWidth = parentWidth - leftOffset - rightOffset;
        float target = leftOffset + (rectWidth / 2);
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            comboBg.offsetMin = new Vector2(Mathf.Lerp(initialLeft, target, t), comboBg.offsetMin.y);
            comboBg.offsetMax = new Vector2(-Mathf.Lerp(initialRight, target, t), comboBg.offsetMax.y);
            yield return null;
        }
        resetComboParamsOnComboBreak();
    }
    void resetComboParamsOnComboBreak(){
        combo = 0;
        comboText.text = "1X";
        isInCombo=false;
        animScript.ComboBoxDisappearTrigger();
    }
    
}
