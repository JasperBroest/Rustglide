using UnityEngine;

public class Door : MonoBehaviour
{
    private void Update()
    {
        if(Chosen.instance.hasChosen)
        {
            this.gameObject.SetActive(false);
        }
    }
}
