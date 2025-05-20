using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RogueLikeManager : MonoBehaviour
{
    public List<AllUpgrades> Upgrades;
    public List<GameObject> ChosenUpgrades;
    public List<GameObject> localUpgrades;

    private int RandValue;

    [System.Serializable]
    public struct AllUpgrades
    {
        public GameObject Upgrade;
        [Range(0, 100)] public int DropChance;
    }

    private void Start()
    {
        if(Upgrades.Count > 3)
        {
            GenerateThree();
        }
    }

    private void SetList()
    {
        foreach(var upgrade in Upgrades)
        {
            localUpgrades.Add(upgrade.Upgrade);
        }
    }

    private void GenerateThree()
    {
        SetList();
        for (int i = 0; i < 3; i++)
        {
            GameObject droppingItem = null;
            int totalWeight = Upgrades.Sum(item => item.DropChance);
            RandValue = UnityEngine.Random.Range(1, totalWeight + 1);
            Debug.Log(RandValue);
            int cumulative = 0;
            for(int j = 0; j < localUpgrades.Count; j++)
            {
                cumulative += Upgrades[j].DropChance;
                if (RandValue <= cumulative)
                {
                    droppingItem = localUpgrades[j];
                    localUpgrades.RemoveAt(j);
                    break;
                }
            }
            if(droppingItem != null)
            {
                ChosenUpgrades.Add(droppingItem);
            }
        }
    }
}