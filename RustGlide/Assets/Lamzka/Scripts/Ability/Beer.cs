using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Beer : AblilityAbstract
{
    public string TagOfCollider;

    //if Beer touches player collider, apply Item to player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagOfCollider))
        {
            ApplyAbility();
            Destroy(this.gameObject.GetComponent<SphereCollider>());
            Destroy(gameObject.GetNamedChild("biertje"));
            StartCoroutine(DestroyObject());


        }
    }

    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

}
