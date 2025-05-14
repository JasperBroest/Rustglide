using System;
using UnityEngine;
using System.Collections.Generic;

public class RogueLikeManager : MonoBehaviour
{
    public List<GameObject> Upgrades;
    public List<GameObject> ChosenUpgrades;

    private int RandValue;

    private void Start()
    {
        if(Upgrades.Count > 0)
        {
            SelectThree();
        }
    }

    private void SelectThree()
    {
        for(int i = 0; i < 3; i++)
        {
            RandValue = UnityEngine.Random.Range(0, Upgrades.Count);
            ChosenUpgrades.Add(Upgrades[RandValue]);
            Upgrades.Remove(Upgrades[RandValue]);
        }
    }
}