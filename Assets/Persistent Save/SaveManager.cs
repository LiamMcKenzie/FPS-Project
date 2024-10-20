using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public int waveCount;
    
    public int displayModeIndex;
    public bool fpsDisplay;
    public int fpsCap;
    public int resolutionIndex;
    public bool vsync;
    public float mouseSensitivity;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
        //SaveToFile();
        

        LoadFromFile();

    }

    public void DefaultSettings()
    {
        waveCount = 0;
        displayModeIndex = 0;
        fpsDisplay = false;
        fpsCap = 60;
        resolutionIndex = 0;
        vsync = false;
        mouseSensitivity = 2f;
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
        writer.WriteLine("Mouse Sensitivity: " + mouseSensitivity);
        writer.Close();

        Debug.Log("Settings saved to " + path);
    }

    public void LoadFromFile()
    {
        string path = Application.dataPath + "/settings.txt";

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);

            waveCount = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            displayModeIndex = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            fpsDisplay = bool.Parse(reader.ReadLine().Split(':')[1].Trim());
            fpsCap = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            resolutionIndex = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            vsync = bool.Parse(reader.ReadLine().Split(':')[1].Trim());
            mouseSensitivity = float.Parse(reader.ReadLine().Split(':')[1].Trim());

            reader.Close();
            Debug.Log("Settings loaded from " + path);
        }
        else
        {
            DefaultSettings();
            SaveToFile();
            Debug.LogWarning("No save file found, default settings loaded");
        }
    }

    
}