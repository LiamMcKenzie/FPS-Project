using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public AudioClip buttonClick;
    public AudioClip shotgunShot;
    public AudioClip pistolShot;
    public AudioClip hitsound;
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
        if(shotgunShot == null)
        {
            Debug.LogError("Shotgun Shot sound is not assigned in the inspector");
        }
        if(pistolShot == null)
        {
            Debug.LogError("Pistol Shot sound is not assigned in the inspector");
        }
        if(hitsound == null)
        {
            Debug.LogError("Hit sound is not assigned in the inspector");
        }
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick, SaveManager.instance.volume);
    }

    public void PlayShotgunShot()
    {
        audioSource.PlayOneShot(shotgunShot, SaveManager.instance.volume);
    }

    public void PlayPistolShot()
    {
        audioSource.PlayOneShot(pistolShot, SaveManager.instance.volume);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitsound, SaveManager.instance.volume);
    }
}
