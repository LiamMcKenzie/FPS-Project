using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int playerCurrency;
    public int pistolSkinIndex;
    public int shotgunSkinIndex;

    public List<bool> unlockedPistolSkins = new List<bool>();
    public List<bool> unlockedShotgunSkins = new List<bool>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
        
        LoadFromFile();

        DontDestroyOnLoad(gameObject);
    }

    public void DefaultSettings()
    {
        playerCurrency = 0;
        pistolSkinIndex = 0;
        shotgunSkinIndex = 0;
        unlockedPistolSkins = new List<bool>() { true, false, false, false, false, false, false, false}; //the first skin is always unlocked
        unlockedShotgunSkins = new List<bool>() { true, false, false, false, false, false, false, false}; //there are 8 skins for both weapons
    }

    public void SaveToFile()
    {
        string path = Application.dataPath + "/playerdata.txt";

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("Player Currency: " + playerCurrency);
        writer.WriteLine("Pistol Skin: " + pistolSkinIndex);
        writer.WriteLine("Shotgun Skin: " + shotgunSkinIndex);
        writer.WriteLine("Unlocked Pistol Skins: " + string.Join(",", unlockedPistolSkins));
        writer.WriteLine("Unlocked Shotgun Skins: " + string.Join(",", unlockedShotgunSkins));
        writer.Close();

        Debug.Log("Player Data saved to " + path);
    }

    public void LoadFromFile()
    {
        string path = Application.dataPath + "/playerdata.txt";

        if (File.Exists(path))
        {
            //stream reader will continously read the file.
            //the reader will be split and trimed to get the value of the variables we want.
            StreamReader reader = new StreamReader(path);

            playerCurrency = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            pistolSkinIndex = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            shotgunSkinIndex = int.Parse(reader.ReadLine().Split(':')[1].Trim());
            
            unlockedPistolSkins = ParseBoolListString(reader);
            unlockedShotgunSkins = ParseBoolListString(reader);


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

    public List<bool> ParseBoolListString(StreamReader reader)
    {
        string boolListString = reader.ReadLine().Split(':')[1].Trim();
        string[] boolList = boolListString.Split(',');

        List<bool> boolListParsed = new List<bool>();

        for (int i = 0; i < boolList.Length; i++)
        {
            boolListParsed.Add(bool.Parse(boolList[i]));
        }

        return boolListParsed;
    }
}
