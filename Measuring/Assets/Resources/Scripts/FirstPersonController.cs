using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    Rigidbody rb;
    public float force = 5.0f;
    public float rotationSpeed = 100f;

    public float upForce = 2f;
    
    //mouse sensitivity:
    public float sensX = 1f;
    public float sensY = 1f;

    public float xRotation;
    public float yRotation;

    public Transform orientation;

    public float mouseX;
    public float mouseY;

    public AudioClip extingusherSound;
    public AudioSource soundEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        //rotate camera and orientation:
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
        //controller using input to get directions
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        
        //Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        movementDirection.Normalize();


        rb.AddForce(movementDirection * force * 2f, ForceMode.Force);
        //transform.Translate(movementDirection * force * Time.deltaTime, Space.World);


        /*
        //making sure that the player transform is smoothly following the direction of movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion
                toRotation =
                    Quaternion.LookRotation(movementDirection,
                        Vector3.up); //using quaternions to smooth out the rotation of directions. Type specifically to store rotations "looking" in a desired direction vector3.up is the y axis
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation,
                    rotationSpeed * Time.deltaTime); //rotate towards is to rotate towards the desired direction

        }
        */

    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * upForce);
        soundEffect.PlayOneShot(extingusherSound);
    }
}
