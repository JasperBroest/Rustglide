using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InstantiateAbility : MonoBehaviour
{
    public List<GameObject> SpawnAbility;

    private void Update()
    {
        if(GetComponentInParent<RogueLikeManager>().ChosenUpgradesFilled)
        {
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
            ability.transform.parent = this.transform;
        }
    }
}
