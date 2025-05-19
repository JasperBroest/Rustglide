using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RogueLikeManager : MonoBehaviour
{
    public AllUpgrades[] Upgrades;
    public List<GameObject> ChosenUpgrades;

    private int RandValue;

    [System.Serializable]
    public struct AllUpgrades
    {
        public GameObject Upgrade;
        [Range(0, 100)] public int DropChance;
    }

    private void Start()
    {
        if(Upgrades.Length > 3)
        {
            GenerateThree();
        }
    }

    private void GenerateThree()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject droppingItem = null;
            int totalWeight = Upgrades.Sum(item => item.DropChance);
            RandValue = UnityEngine.Random.Range(1, totalWeight + 1);
            Debug.Log(RandValue);
            int cumulative = 0;
            foreach (var item in Upgrades)
            {
                cumulative += item.DropChance;
                if (RandValue <= cumulative)
                {
                    droppingItem = item.Upgrade;
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