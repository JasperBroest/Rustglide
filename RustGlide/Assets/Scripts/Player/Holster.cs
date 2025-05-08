using UnityEngine;

public class Holster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Debug.Log("holster");   
            other.transform.parent = transform.parent;
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            //other.GetComponent<Rigidbody>().useGravity = true;
            //other.transform.SetParent(null);
        }
    }
}
