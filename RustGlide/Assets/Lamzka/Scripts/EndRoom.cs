using System.Collections;
using UnityEngine;

public class EndRoom : MonoBehaviour
{
    public HudManager HudManager;
    public GameObject EndRoomSpawn;




    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Player").transform.position = EndRoomSpawn.transform.position;

        StartCoroutine(finished());


    }



    public IEnumerator finished()
    {
        HudManager.StartEndSequence();
        yield return new WaitForSeconds(5f);
    }



}
