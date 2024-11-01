using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAudioSourceVolume : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.volume *= SaveManager.instance.volume; //multiply the audio volume by the volume setting
        }
    }
}
