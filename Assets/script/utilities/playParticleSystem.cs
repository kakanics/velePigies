using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playParticleSystem : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;
    public void PlayParticleSystemAtPosition(Vector3 position)
    {
        if (particleSystemPrefab != null)
        {
            ParticleSystem particleSystemInstance = Instantiate(particleSystemPrefab, position, Quaternion.identity);
            particleSystemInstance.Play();
            Destroy(particleSystemInstance.gameObject, 4);
        }
        else
        {
            Debug.LogWarning("Particle system prefab is not assigned.");
        }
    }
}
