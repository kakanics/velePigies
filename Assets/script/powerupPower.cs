using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupPower : MonoBehaviour
{
    public int power = 0;
    public Sprite powerUpSprite, powerDownSprite;
    public bool showPower = false;

    void Start()
    {
        while(power==0)
            power = Random.Range(-10,11);
        GetComponent<SpriteRenderer>().sprite=(power>0)?powerUpSprite:powerDownSprite;
        if(showPower){
            var x = gameObject.GetComponentInChildren<TextMesh>();
            x.gameObject.SetActive(true);
            x.text = power.ToString();
        }
    }

}
