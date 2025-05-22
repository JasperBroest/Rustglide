using UnityEngine;

public class Beer : AblilityAbstract
{
    public string TagOfCollider;

    //if Beer touches player collider, apply Item to player
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == TagOfCollider)
        {

        }
    }

}
