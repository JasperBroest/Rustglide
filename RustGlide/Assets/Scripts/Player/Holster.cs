using UnityEngine;

public class Holster : MonoBehaviour
{
    public void SnapToHolster(Collider other)
    {
        other.transform.parent = transform.parent;
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Rigidbody>().useGravity = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            Debug.Log("holster");   
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().useGravity = true;
            other.transform.SetParent(null);
        }
    }
}
