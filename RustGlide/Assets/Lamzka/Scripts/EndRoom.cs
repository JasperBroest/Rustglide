using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRoom : MonoBehaviour
{
    public HudManager HudManager;
    public GameObject EndRoomSpawn;

    public GameObject Lights;
    public bool IsPlayerTeleported;



    private void Start()
    {
        StartCoroutine(TurnLightsOnAndOff());
        IsPlayerTeleported = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsPlayerTeleported)
            {
                other.gameObject.transform.position = EndRoomSpawn.transform.position;
                IsPlayerTeleported = true;
                StartCoroutine(finished());
            }
        }



    }

    public IEnumerator TurnLightsOnAndOff()
    {
        while (this.gameObject.activeInHierarchy)
        {
            Lights.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Lights.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator finished()
    {
        HudManager.StartEndSequence();
        yield return new WaitForSeconds(7f);
        StoreStamina.instance.OnSceneLoaded();
        SceneManager.LoadScene(0);
    }
}
