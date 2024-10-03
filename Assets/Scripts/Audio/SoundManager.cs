using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public AudioClip buttonClick;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        CheckMissingSounds();
    }

    public void CheckMissingSounds()
    {
        if(buttonClick == null)
        {
            Debug.LogError("Button Click sound is not assigned in the inspector");
        }
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
}
