using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setReferences : MonoBehaviour
{
    public Slingshot slingshot;
    worldShift worldShift;
    public hookSpawner hookSpawner;
    public animationMethods animationMethods;
    public scoreManager scoreManager;
    public powerUpSpawner powerUpSpawner;
    public playParticleSystem particleSystemScript;
    public AudioSource[] sources;
    void Awake()
    {
        worldShift=GetComponent<worldShift>();
        worldShift.animScript=animationMethods;
        
        slingshot.worldShift=worldShift;
        slingshot.hookSpawner=hookSpawner;
        slingshot.hookController=hookSpawner.gameObject.GetComponent<hookController>();
        slingshot.scoreManager=scoreManager;
        slingshot.deathRoutine = GetComponent<deathRoutine>();
        slingshot.winRoutine = GetComponent<winRoutine>();
        slingshot.particleSystemScript = particleSystemScript;
        slingshot.animMethods = animationMethods;

        hookSpawner.scoreManager=scoreManager;
        scoreManager.GetComponent<comboManager>().animScript = animationMethods;
        int x = PlayerPrefs.GetInt("Sound", 0);
        PlayerPrefs.SetInt("Sound", x); 
        soundMnaager.instance.PlaySound(SoundName.CLICK);
        foreach(var i in sources)
        {
            i.enabled = x == 0;
        }

    }

}
