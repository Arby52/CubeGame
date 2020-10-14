using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMechanics : MonoBehaviour
{
    public enum sizeState
    {
        small,
        medium,
        big
    };

    public sizeState cubeSize = sizeState.medium;
    [SerializeField]
    Vector3 smallSize;
    [SerializeField]
    Vector3 mediumSize;
    [SerializeField]
    Vector3 bigSize;

    float lastTime = 0;
    [SerializeField]
    float inputDelay = 0.5f;

    void Update()
    {
        ChangeSizeState();
    }

    private void FixedUpdate()
    {
        switch (cubeSize)
        {
            case sizeState.small:
                gameObject.transform.localScale = smallSize;
                break;

            case sizeState.medium:
                gameObject.transform.localScale = mediumSize;
                break;

            case sizeState.big:
                gameObject.transform.localScale = bigSize;
                break;

            default:
                break;
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
                    case sizeState.small:
                        cubeSize = sizeState.medium;
                        break;

                    case sizeState.medium:
                        cubeSize = sizeState.big;
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
                    case sizeState.big:
                        cubeSize = sizeState.medium;
                        break;

                    case sizeState.medium:
                        cubeSize = sizeState.small;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
