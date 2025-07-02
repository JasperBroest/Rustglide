using UnityEngine;

public class SoundOnLanding : MonoBehaviour
{
    public AudioClip[] landingSounds;         
              
    public float raycastDistance = 0.1f;      
    public float landingVelocityThreshold = 2f; 

    public AudioSource audioSource;
    private bool wasGrounded;
    private Vector3 lastVelocity;

    private Rigidbody rb;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * raycastDistance);
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        
        
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, raycastDistance);
        
        // Detect landing: was NOT grounded, now IS grounded
        if (!wasGrounded && isGrounded /*&& lastVelocity.y < -landingVelocityThreshold*/)
        {
            PlayRandomLandingSound();
        }

        wasGrounded = isGrounded;
        /*lastVelocity = rb.linearVelocity;*/
    }

    void PlayRandomLandingSound()
    {
        if (landingSounds.Length == 0) return;
        
        int index = Random.Range(0, landingSounds.Length);
        audioSource.PlayOneShot(landingSounds[index]);
    }
}
