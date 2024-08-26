using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public enum UpgradeSection
    {
        Player,
        Pistol,
        Shotgun
    }

    [System.Serializable]
    public struct Upgrade
    {
        public string name;
        public List<float> upgradeValues; //example fire rate = [0.5, 0.25, 0.1, 0.05]. Boolean upgrades will need to use 0 and 1
        public int upgradeProgress;
        public Upgrade(string name, List<float> upgradeValues, int upgradeProgress)
        {
            this.name = name;
            this.upgradeValues = upgradeValues;
            this.upgradeProgress = upgradeProgress;
        }
    }

    public List<Upgrade> Upgrades = new List<Upgrade>();



    public List<Upgrade> ReturnUpgrades(){
        return Upgrades;
    }
}
