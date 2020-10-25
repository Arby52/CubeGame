using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField]
    Vector3 movement;
    bool isGrounded;
    public float speed;
    //[HideInInspector]
    public float jumpHeight;
    public float gravity;

    Camera cam;
    Vector3 camOffset;
    public float cameraSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        movement = Vector3.zero;
        isGrounded = false;
        camOffset = transform.position - cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {       

        //Movement Stuff
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        movement.x = xMovement * speed;
        movement.z = zMovement * speed;

        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            movement.y = jumpHeight;
        } else if (controller.isGrounded) //If on the ground, y velocity is 0.
        {
            movement.y = 0;
        } else //If not on ground, get affected by gravity.
        {
            movement.y -= gravity * Time.deltaTime;
        }

        
        controller.Move(movement *Time.fixedDeltaTime);

        //Camera Stuff
        float xRotation = Input.GetAxis("Mouse X") * cameraSensitivity;
        float yRotation = Input.GetAxis("Mouse Y") * cameraSensitivity;
        
        //transform.Rotate(0, xRotation, 0);

        Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        cam.transform.RotateAround(transform.position, -Vector3.up, xRotation);
        //cam.transform.position = transform.position - camOffset;
        //cam.transform.LookAt(transform);
    }
}
