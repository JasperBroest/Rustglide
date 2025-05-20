using UnityEngine;


[CreateAssetMenu]
public class AbilityObject : ScriptableObject
{
    [Header("Item Settings")]
    public float EffectDuration;

    public float SetSpeed;
    public float SetDamage;

    public float SetSlowSpeed;
    public float SetSlowDamage;

    [Header("Does Object Have a Duration")]
    public bool DoesItemHaveDuration;
}
