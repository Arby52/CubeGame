using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Vector3 movement;
    bool isGrounded;
    public float speed;
    public float jumpHeight;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = Vector3.zero;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        movement.x = xAxis * speed;
        movement.z = zAxis * speed;

        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            movement.y = jumpHeight;
        }        

        movement.y -= gravity * Time.deltaTime;

        controller.Move(movement *Time.fixedDeltaTime);
    }

}
