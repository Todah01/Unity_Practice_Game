using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource mainEngine_as;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainEngine_as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(!mainEngine_as.isPlaying)
                mainEngine_as.PlayOneShot(mainEngine);

            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        }
        else{
            mainEngine_as.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    void ApplyRotation(Vector3 direction)
    {
        rb.freezeRotation = true; // freeaing rotation so we can manually rotate
        transform.Rotate(direction * rotateSpeed * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
