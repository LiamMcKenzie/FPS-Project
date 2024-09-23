using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    public int waveCount;
    
    public int displayModeIndex;
    public bool fpsDisplay;
    public int fpsCap;
    public int resolutionIndex;
    public bool vsync;

    void Start()
    {
        SaveToFile();
    }

    public void SaveToFile()
    {
        string path = Application.dataPath + "/settings.txt";

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("Wave Count: " + waveCount);
        writer.WriteLine("Display Mode Index: " + displayModeIndex);
        writer.WriteLine("FPS Display: " + fpsDisplay);
        writer.WriteLine("FPS Cap: " + fpsCap);
        writer.WriteLine("Resolution Index: " + resolutionIndex);
        writer.WriteLine("VSync: " + vsync);
        writer.Close();

        Debug.Log("Settings saved to " + path);
    }
}