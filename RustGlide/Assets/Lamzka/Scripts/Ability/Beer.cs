using UnityEngine;

public class Beer : AblilityAbstract
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            ApplyToPlayer();
        }
    }

}
