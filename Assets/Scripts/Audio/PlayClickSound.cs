using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickSound : MonoBehaviour
{
    public void PlayClick()
    {
        SoundManager.instance.PlayButtonClick();
    }
}
