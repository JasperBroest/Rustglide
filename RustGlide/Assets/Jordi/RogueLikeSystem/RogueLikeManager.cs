using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RogueLikeManager : MonoBehaviour
{
    public List<AllUpgrades> Upgrades;
    [HideInInspector] public List<GameObject> localUpgrades;
    [HideInInspector] public List<int> localDropChance;
    [HideInInspector] public bool ChosenUpgradesFilled;

    private int RandValue;

    [System.Serializable]
    public struct AllUpgrades
    {
        public GameObject Upgrade;
        [Range(0, 100)] public int DropChance;
    }

    private void Start()
    {
        if(Upgrades.Count > 0)
        {
            GenerateThree();
        }
    }

    private void SetList()
    {
        foreach(var upgrade in Upgrades)
        {
            localUpgrades.Add(upgrade.Upgrade);
            localDropChance.Add(upgrade.DropChance);
        }
    }

    private void GenerateThree()
    {
        SetList();
        for (int i = 0; i < 3; i++)
        {
            GameObject droppingItem = null;
            int totalWeight = localDropChance.Sum(item => item);
            RandValue = UnityEngine.Random.Range(1, totalWeight + 1);
            Debug.Log(RandValue);
            int cumulative = 0;
            for(int j = 0; j < localUpgrades.Count; j++)
            {
                cumulative += localDropChance[j];
                if (RandValue <= cumulative)
                {
                    droppingItem = localUpgrades[j];
                    localUpgrades.RemoveAt(j);
                    localDropChance.RemoveAt(j);
                    break;
                }
            }
            if(droppingItem != null)
            {
                GetComponentInChildren<InstantiateAbility>().SpawnAbility.Add(droppingItem);
            }
        }
        ChosenUpgradesFilled = true;
    }
}