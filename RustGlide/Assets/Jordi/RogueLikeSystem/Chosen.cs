using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Chosen : MonoBehaviour
{
    public bool HasChosen;

    private void Update()
    {
        if(HasChosen)
        {
            GetChosenCard();
        }
    }

    private void GetChosenCard()
    {
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
