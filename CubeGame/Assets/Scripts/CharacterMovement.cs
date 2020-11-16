using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController controller;
    [SerializeField]
    Vector3 movement;
    bool isGrounded;
    [HideInInspector]
    public float speed;
    [HideInInspector]
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
        //Movement Stuff
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        Vector3 directionSide = transform.right * xMovement;
        Vector3 directionForward = transform.forward * zMovement;        

        Vector3 direction = (directionForward + directionSide).normalized;

        movement.x = direction.x * speed;
        movement.z = direction.z * speed;

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
    }
}
