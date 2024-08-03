using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundName
{
    POWERUP,
    DEATH,
    CATCH,
    THROW,
    HIT,
    RELEASE,
    POWERDOWN,
    ELEDEATH
}

public class soundMnaager : MonoBehaviour
{
    public static soundMnaager instance;
    public List<AudioClip> audioClips;
    private Dictionary<SoundName, AudioClip> audioClipDictionary;
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
        audioClipDictionary = new Dictionary<SoundName, AudioClip>();

        // Populate the dictionary with audio clips
        foreach (var clip in audioClips)
        {
            // Strip the file extension
            string clipNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(clip.name);

            // Attempt to parse the stripped name into the enum
            if (System.Enum.TryParse(clipNameWithoutExtension, out SoundName soundName))
            {
                audioClipDictionary[soundName] = clip;
            }
            else
            {
                Debug.LogWarning($"Audio clip name {clip.name} does not match any SoundName enum value.");
            }
        }
    }

    // Method to play a sound by enum
    public void PlaySound(SoundName soundName)
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