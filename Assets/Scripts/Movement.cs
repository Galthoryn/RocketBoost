using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustspeed=100f;
    [SerializeField] float rotationstrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem rightThrustparticle;
    [SerializeField] ParticleSystem leftThrustparticle;
    [SerializeField] ParticleSystem engineThrustparticle;


    Rigidbody rb;
    AudioSource a;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        a = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        RotationProcess();

    }

    void ProcessThrust()
    {

        Thrusting();
    }

    void Thrusting()
    {
        if (thrust.IsPressed())
        {

            rb.AddRelativeForce(Vector3.up * thrustspeed * Time.fixedDeltaTime);


            if (!a.isPlaying)
            {
                a.PlayOneShot(mainEngine);
            }
            if (!engineThrustparticle.isPlaying)
            {
                engineThrustparticle.Play();
            }
        }
        else
        {
            engineThrustparticle.Stop();
            a.Stop();

        }
    }

    void RotationProcess()
    {
        Rotating();
    }

    void Rotating()
    {
        float rotate = rotation.ReadValue<float>();
        if (rotate < 0)
        {

            ApplyRotation(rotationstrength);
            if (!rightThrustparticle.isPlaying)
            {
                rightThrustparticle.Play();
            }
            else
            {
                rightThrustparticle.Stop();
            }

        }
        else if (rotate > 0)

        {
            ApplyRotation(-rotationstrength);
            if (!leftThrustparticle.isPlaying)
            {
                leftThrustparticle.Play();
            }
            else
            {
                leftThrustparticle.Stop();
            }

        }
    }

    void ApplyRotation(float rotateThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisFrame* Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
   
}
