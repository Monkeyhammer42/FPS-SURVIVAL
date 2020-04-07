using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] axeAirSounds;
        

    void PlayAxeAirSound()
    {
        audioSource.clip = axeAirSounds[Random.Range(0, axeAirSounds.Length)];
        audioSource.Play();
        
    }
}
