using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour
{
    public void PlayTick()
    {
        SoundManager.instance.PlayButtonTick();
    }

    public void PlayClick()
    {
        SoundManager.instance.PlayButtonClick();
    }
}
