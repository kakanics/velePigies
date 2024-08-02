using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundMnaager : MonoBehaviour
{
    public static soundMnaager instance;

    public List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> audioClipDictionary;
    private AudioSource audioSource;

    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize the audio source and dictionary
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClipDictionary = new Dictionary<string, AudioClip>();

        // Populate the dictionary with audio clips
        foreach (var clip in audioClips)
        {
            audioClipDictionary[clip.name] = clip;
        }
    }

    // Method to play a sound by name
    public void PlaySound(string soundName)
    {
        if (audioClipDictionary.TryGetValue(soundName, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound {soundName} not found!");
        }
    }
}