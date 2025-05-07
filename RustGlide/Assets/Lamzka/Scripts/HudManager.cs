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
}
