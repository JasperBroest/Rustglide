using System.Collections;
using UnityEngine;

public abstract class AblilityAbstract : MonoBehaviour
{
    [Header("Testing button lmao")]
    public bool yes;



    [Header("Scriptable Object")]
    public AbilityObject SO;




    void Update()
    {

        if (SO.DoesItemHaveDuration)
        {
            StartCoroutine(EffectTimer());
            yes = false;
        }
        else
        {
            NoCooldown();
        }

    }


    public void ApplyToPlayer()
    {


        Debug.Log("Effect Apllied To player");
    }

    public void NoCooldown()
    {

    }



    private IEnumerator EffectTimer()
    {
        ApplyToPlayer();

        yield return new WaitForSeconds(SO.EffectDuration);

        Debug.Log("bruhh");
    }
}
