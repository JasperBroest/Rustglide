using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InstantiateAbility : MonoBehaviour
{
    private List<GameObject> SpawnAbility;

    private void Update()
    {
        if(GetComponentInParent<RogueLikeManager>().ChosenUpgradesFilled)
        {
            SpawnAbility = GetComponentInParent<RogueLikeManager>().ChosenUpgrades;
            SpawnAbilities();
            GetComponentInParent<RogueLikeManager>().ChosenUpgradesFilled = false;
        }
    }

    private void SpawnAbilities()
    {
        for(int i = 0; i < SpawnAbility.Count; i++)
        {
            GameObject ability = Instantiate(SpawnAbility[i], this.transform.GetChild(i));
            ability.transform.localPosition = Vector3.zero;
            ability.transform.localPosition += new Vector3(0, 1.5f, -1.5f);
        }
    }
}
