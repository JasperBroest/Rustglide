using UnityEngine;

public class ForceField : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
       {
           // Check if the entering object is not the player
           if (!other.CompareTag("RocketPlayer") || !other.CompareTag("Gun"))
           {
               // Optional: Push the object back, destroy it, or do nothing
               Rigidbody rb = other.attachedRigidbody;
               if (rb != null)
               {
                   Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                   rb.AddForce(pushDirection * 5f, ForceMode.Impulse);
               }
   
               Debug.Log("Non-player tried to enter, blocked or pushed away.");
           }
           else
           {
               Debug.Log("Player entered successfully.");
           }
       }
}
