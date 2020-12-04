using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController controller;
    [SerializeField]
    Vector3 movement;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float jumpHeight;
    public float gravity;

    [HideInInspector]
    public bool isSticky;


    //Ground
    public GameObject groundDetectionParent;
    private Transform[] groundDetectors = new Transform[4];


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        movement = Vector3.zero;     
        
        //Get all grounded raycasts
        for(int i = 0; i < groundDetectors.Length; i++)
        {
            groundDetectors[i] = groundDetectionParent.transform.GetChild(i);
        }
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

        //If on the ground and hit the jump button
        if (GroundedCheck() && Input.GetButton("Jump"))
        {
            movement.y = jumpHeight;
        } else if (GroundedCheck()) //If on the ground, and not hitting jump button
        {
            movement.y = 0;
        } else //If not on ground, get affected by gravity.
        {
            movement.y -= gravity * Time.deltaTime;
        }
        controller.Move(movement *Time.fixedDeltaTime);

        
        Debug.Log("speed is " + speed);
        Debug.Log("jump is " + jumpHeight);
        Debug.Log("movement is " + movement);
    }


    bool GroundedCheck()
    {
        bool grounded = false;
        if (isSticky) //Is Sticky
        {
            //Ground            

            if(Physics.CheckBox(transform.position, transform.localScale / 2, transform.rotation, LayerMask.NameToLayer("Player"))) { 
                grounded = true;
            }

            if (controller.isGrounded)
            {
                grounded = true;
            }

            /*
            foreach (Transform t in groundDetectors)
            {
                RaycastHit hit;
                float distance = 0.1f;

                if (Physics.Raycast(t.position, -Vector3.up, out hit, distance))
                {
                    if (hit.transform.tag != "Player")
                    {
                        grounded = true;
                    }
                }
            }
            */

            return grounded;

        }
        else //Not Sticky
        {
            if (controller.isGrounded)
            {
                grounded = true;
            }

            /*
            //Ground
            foreach (Transform t in groundDetectors)
            {
                RaycastHit hit;
                float distance = 0.1f;

                if (Physics.Raycast(t.position, -Vector3.up, out hit, distance))
                {
                    if (hit.transform.tag != "Player")
                    {
                        grounded = true;
                    }
                }
            }
            */

            return grounded;
        }
    }
   
}
