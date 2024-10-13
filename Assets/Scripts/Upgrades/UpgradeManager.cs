using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeSection
{
    Player,
    Pistol,
    Shotgun
}

public class UpgradeManager : MonoBehaviour
{
   [HideInInspector] public int upgradePoints = 0;
   public int pointsPerWave = 3;

    [System.Serializable]
    public struct Upgrade
    {
        public string name;
        public List<float> upgradeValues; //example fire rate = [0.5, 0.25, 0.1, 0.05]. Boolean upgrades will need to use 0 and 1
        public UpgradeSection upgradeSection;
        public Upgrade(string name, List<float> upgradeValues, UpgradeSection upgradeSection)
        {
            this.name = name;
            this.upgradeValues = upgradeValues;
            this.upgradeSection = upgradeSection;
        }
    }

    public List<Upgrade> Upgrades = new List<Upgrade>();
    public List<int> upgradeProgress = new List<int>();

    void Start()
    {
        ClearUpgradeProgress();

        ResetUpgradePoints();
    }

    public void ClearUpgradeProgress()
    {
        upgradeProgress.Clear();
        for (int i = 0; i < ReturnListSize(); i++)
        {
            upgradeProgress.Add(0);
        }
    }

    public void ResetUpgradePoints()
    {
        upgradePoints = pointsPerWave;
    }

    public void RefundUpgradePoints(int waveNumber)
    {
        upgradePoints = waveNumber * pointsPerWave;
        ClearUpgradeProgress();
    }

    public int GetUpgradePoints()
    {
        return upgradePoints;
    }

    

    /// <summary>
    /// This function checks the index value to make sure its within range, if its not in range it picks the nearest value.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetIndexWithinRange(int index)
    {
        if(index > ReturnListSize()) //if desired index is over range, set to highest value
        {
            Debug.LogWarning("Trying to get upgrade index that is over range, setting to nearest value");
            return ReturnListSize();
        }else if (index < 0){ //if desired index is below 0, set to 0
            Debug.LogWarning("Trying to get upgrade index that is less than 0, setting to 0");
            return 0;
        }else
        {
            return index; //this value is within range
        }
    }

    public int ReturnListSize()
    {
        return Upgrades.Count;
    }

    public int ReturnUpgradeLevels(int index) //gets the amount of upgrade levels, used for displaying upgrade ticks. subtracts 1 because the first value is the default.
    {
        return Upgrades[GetIndexWithinRange(index)].upgradeValues.Count - 1;
    }

    //I can't pass the entire upgrade struct so I'm passing through each value.
    public string ReturnUpgradeName(int index) 
    {
        return Upgrades[GetIndexWithinRange(index)].name;
    }

    public UpgradeSection ReturnUpgradeSection(int index) 
    {
        return Upgrades[GetIndexWithinRange(index)].upgradeSection;
    }

    public int ReturnUpgradeProgress(int index) 
    {
        return upgradeProgress[GetIndexWithinRange(index)];
    }

    public List<float> ReturnUpgradeValues(int index) 
    {
        return Upgrades[GetIndexWithinRange(index)].upgradeValues;
    }

    public void IncreaseProgress(int index)
    {
        upgradeProgress[index]++;
    }
    
    //

    public int FindUpgradeByName(string name, UpgradeSection section) //finds the index of an upgrade by name and section
    {
        for (int i = 0; i < Upgrades.Count; i++)
        {
            if (Upgrades[i].name == name && Upgrades[i].upgradeSection == section)
            {
                return i;
            }
        }
        return -1;
    }

    public float ReturnUpgradeValue(string name, UpgradeSection upgradeType) //gets the current value for a specific upgrade. Upgrade stats use this function
    {
        int index = FindUpgradeByName(name, upgradeType);
        if(index == -1)
        {
            Debug.LogWarning($"Upgrade not found, returning 0. Upgrade name: {name}, Upgrade type: {upgradeType}");
            return 0;
        }
        return ReturnUpgradeValues(index)[ReturnUpgradeProgress(index)];
    }
}
