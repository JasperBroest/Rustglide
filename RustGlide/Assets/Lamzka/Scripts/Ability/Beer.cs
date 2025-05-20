using System.Collections;
using UnityEngine;


public class Beer : MonoBehaviour
{

    [Header("Testing button lmao")]
    public bool yes;

    [Header("Item Settings")]
    [SerializeField] private float EffectDuration;

    [SerializeField] private float SetSpeed;
    [SerializeField] private float SetDamage;

    [SerializeField] private float SetSlowSpeed;
    [SerializeField] private float SetSlowDamage;

    [Header("Additive")]
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip AudioClip;




    void Update()
    {

        if (yes)
        {
            StartCoroutine(EffectTimer());
            yes = false;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }





    private IEnumerator EffectTimer()
    {
        yield return new WaitForSeconds(EffectDuration);
        Debug.Log("bruhh");
    }




}
