using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueLikeManager : MonoBehaviour
{
    public List<AllUpgrades> Upgrades;
    [HideInInspector] public List<GameObject> localUpgrades;
    [HideInInspector] public List<int> localDropChance;
    [HideInInspector] public bool ChosenUpgradesFilled;

    private GameObject Stamina;

    private int RandValue;

    [System.Serializable]
    public struct AllUpgrades
    {
        public GameObject Upgrade;
        [Range(0, 100)] public int DropChance;
    }

    private void Start()
    {
        GetStaminaComponent();
        if (Upgrades.Count > 0)
        {
            GenerateThree();
        }
    }

    private void SetList()
    {
        foreach (var upgrade in Upgrades)
        {
            localUpgrades.Add(upgrade.Upgrade);
            localDropChance.Add(upgrade.DropChance);
        }
    }

    private void GenerateThree()
    {
        Stamina.gameObject.SetActive(false);
        SetList();
        for (int i = 0; i < 3; i++)
        {
            GameObject droppingItem = null;
            int totalWeight = localDropChance.Sum(item => item);
            RandValue = UnityEngine.Random.Range(1, totalWeight + 1);
            Debug.Log(RandValue);
            int cumulative = 0;
            for (int j = 0; j < localUpgrades.Count; j++)
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
            if (droppingItem != null)
            {
                GetComponentInChildren<InstantiateAbility>().SpawnAbility.Add(droppingItem);
            }
        }
        ChosenUpgradesFilled = true;
    }

    public void OnGrab()
    {
        Stamina.gameObject.SetActive(true);
        /*this.transform.parent.gameObject.SetActive(false);*/
        this.gameObject.SetActive(false);
    }

    public void GetStaminaComponent()
    {
        Stamina = GameObject.FindWithTag("Stamina");
    }
}