using UnityEngine;
using UnityEngine.InputSystem;

public class DashToDirection : MonoBehaviour
{
    public GameObject CubeRight;
    public GameObject CubeLeft;

    public Rigidbody PlayerRigidbody;

    public InputAction RightTrigger;
    public InputAction LeftTrigger;

    public AudioClip ThrusterSound;
    public AudioSource AudioSource;

    public float ThrustPower;

    private bool HasRightBeenPressed;
    private bool HasLeftBeenPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        RightTrigger.Enable();
        LeftTrigger.Enable();
    }

    private void Start()
    {
        AudioSource.clip = ThrusterSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (RightTrigger.ReadValue<float>() > 0.1f)
        {
            Debug.Log("RightHandTrigger");
            AddForceToCubeRightDirection();
            HasRightBeenPressed = true;
        }
        else if (HasRightBeenPressed && AudioSource.isPlaying)
        {
            StopNoise();
            HasRightBeenPressed = false;
        }

        if (LeftTrigger.ReadValue<float>() > 0.1f)
        {
            Debug.Log("LeftHandTrigger");
            AddForceToCubeLeftDirection();
            HasLeftBeenPressed = true;
        }
        else if (HasLeftBeenPressed && AudioSource.isPlaying)
        {
            StopNoise();
            HasLeftBeenPressed = false;
        }
    }

    void AddForceToCubeRightDirection()
    {
        if (!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);
        PlayerRigidbody.AddForce(CubeRight.transform.forward * ThrustPower);
    }
    void AddForceToCubeLeftDirection()
    {
        if (!AudioSource.isPlaying)
            AudioSource.PlayOneShot(ThrusterSound);
        PlayerRigidbody.AddForce(CubeLeft.transform.forward * ThrustPower);
    }

    void StopNoise()
    {
        AudioSource.Stop();
    }
}
