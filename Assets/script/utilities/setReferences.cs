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
    public AudioListener listener;
    void Awake()
    {
        worldShift=GetComponent<worldShift>();
        worldShift.animScript=animationMethods;
        
        slingshot.worldShift=worldShift;
        slingshot.hookSpawner=hookSpawner;
        slingshot.hookController=hookSpawner.gameObject.GetComponent<hookController>();
        slingshot.scoreManager=scoreManager;
        slingshot.deathRoutine = GetComponent<deathRoutine>();
        slingshot.particleSystemScript = particleSystemScript;
        slingshot.animMethods = animationMethods;

        hookSpawner.scoreManager=scoreManager;
        scoreManager.GetComponent<comboManager>().animScript = animationMethods;
        listener.enabled = PlayerPrefs.GetInt("Sound") == 0;
    }

}
