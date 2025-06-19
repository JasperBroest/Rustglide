using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasOpened;

    private void Update()
    {
        if (AbilityManager.Instance.HasChosen && !hasOpened)
        {
            hasOpened = true;
            this.gameObject.SetActive(false);
        }
    }
}
