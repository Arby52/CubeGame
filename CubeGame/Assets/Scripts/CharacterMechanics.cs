using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public enum SizeStates
    {
        small,
        medium,
        big
    };

    Camera cam;
    Vector3 tempCamPos;

    [SerializeField]
    PowerStates powerState;

    public SizeStates cubeSize = SizeStates.medium;
    [SerializeField]
    Vector3 smallSize;
    [SerializeField]
    Vector3 mediumSize;
    [SerializeField]
    Vector3 bigSize;

    float lastTime = 0;
    [SerializeField]
    float inputDelay = 0.5f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        ChangeSizeState();
    }

    private void FixedUpdate()
    {
        switch (cubeSize)
        {
            case SizeStates.small:
                gameObject.transform.localScale = smallSize;
                break;

            case SizeStates.medium:
                gameObject.transform.localScale = mediumSize;
                break;

            case SizeStates.big:
                gameObject.transform.localScale = bigSize;
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerPodium")
        {
            GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            powerState = other.GetComponent<PodiumColour>().powerState;
        }
    }

    void ChangeSizeState()
    {
        //Getting Bigger
        if (Input.GetKey(KeyCode.E))
        {
            if (Time.time - lastTime > inputDelay)
            {
                lastTime = Time.time;
                switch (cubeSize)
                {
                    case SizeStates.small:
                        cubeSize = SizeStates.medium;
                        break;

                    case SizeStates.medium:
                        cubeSize = SizeStates.big;
                        break;

                    default:
                        break;
                }
            }
        }

        //Getting Smaller
        if (Input.GetKey(KeyCode.Q))
        {
            if (Time.time - lastTime > inputDelay)
            {
                lastTime = Time.time;
                switch (cubeSize)
                {
                    case SizeStates.big:
                        cubeSize = SizeStates.medium;
                        break;

                    case SizeStates.medium:
                        cubeSize = SizeStates.small;
                        break;

                    default:
                        break;
                }
            }
        }
    }

}
