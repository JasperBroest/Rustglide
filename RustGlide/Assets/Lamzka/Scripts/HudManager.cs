using System.Collections;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{

    public GameObject TextObject;
    public TextMeshPro Text;

    public float WaitBeforeBlink = 0.5f;



    void Start()
    {
        TextObject.SetActive(false);
        StartCoroutine(TextStartSequence());
    }

    public void StartEndSequence()
    {
        StartCoroutine(EndSequence());
    }

    public void StartDeathSequence()
    {
        StartCoroutine(DeathSequence());
    }

    public IEnumerator TextStartSequence()
    {
        Text.SetText("KEEP MOVING");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("KILL ALL");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
    }

    public IEnumerator EndSequence()
    {
        Text.SetText("GOOD JOB");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("PREPARE MORE CLEANUP");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("PREPARE MORE CLEANUP.");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("PREPARE MORE CLEANUP..");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("PREPARE MORE CLEANUP...");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
    }

    public IEnumerator DeathSequence()
    {
        Text.SetText("YOU");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("YOU DIED");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("TRY AGAIN.");
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(true);
        yield return new WaitForSeconds(WaitBeforeBlink);
        TextObject.SetActive(false);
        yield return new WaitForSeconds(WaitBeforeBlink);
        Text.SetText("KEEP MOVING");

    }
}
