using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DoorControlls : MonoBehaviour
{

    public GameObject linkedDoor;
    Animator animator;

    public bool onlyWorksWhileBig;
    public float doorCloseSpeed;

    public Material doorMat;
    public Material heavyDoorMat;

    private void Start()
    {
        try
        {
            animator = linkedDoor.GetComponent<Animator>();
            animator.SetFloat("DoorCloseSpeed", doorCloseSpeed);
        } catch (System.Exception)
        {
            Debug.LogError("Make sure 'Door(drag this one onto the variable)' is in the Linked Door variable in the door controlls script on DoorPad)");
        }
    }

    private void Update()
    {
        if (onlyWorksWhileBig)
        {
            GetComponent<MeshRenderer>().material = heavyDoorMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = doorMat;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            if (onlyWorksWhileBig)
            {
                if (other.GetComponent<CharacterMechanics>().cubeSize == CharacterMechanics.SizeStates.big)
                {
                    animator.SetBool("Open", true);
                    animator.SetBool("Close", false);
                }
            } else
            {
                animator.SetBool("Open", true);
                animator.SetBool("Close", false);
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Open", false);
            animator.SetBool("Close", true);
        }
    }
}
