using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //CompletedWaveCount.waveCount = 0; //resets the global wave count
        SaveManager.instance.waveCount = 0; //resets the save file wave count
        SaveManager.instance.SaveToFile();
    }
}
