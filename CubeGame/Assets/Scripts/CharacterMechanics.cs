using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterMechanics : MonoBehaviour
{
    public enum SizeStates
    {
        small,
        medium,
        big
    };

    CharacterMovement charMovement;

    [SerializeField]
    PowerStates powerState;

    public SizeStates cubeSize = SizeStates.medium;

    [SerializeField]
    Vector3 smallSize;
    [SerializeField]
    Vector3 mediumSize;
    [SerializeField]
    Vector3 bigSize;

    [SerializeField]
    float smallJumpHeight;
    [SerializeField]
    float mediumJumpHeight;
    [SerializeField]
    float bigJumpHeight;

    float lastTime = 0;
    [SerializeField]
    float inputDelay = 0.5f;

    //Sound Effects
    [SerializeField]
    AudioClip getBiggerAudio;
    [SerializeField]
    AudioClip getSmallerAudio;
    [SerializeField]
    AudioClip colourChangeAudio;

    void Start()
    {
        charMovement = GetComponent<CharacterMovement>();
        charMovement.jumpHeight = mediumJumpHeight;
    }

    void Update()
    {
        ChangeSizeState();
    }

    private void FixedUpdate()
    {
        /*
        switch (cubeSize)
        {
            case SizeStates.small:
                gameObject.transform.localScale = smallSize;
                charMovement.jumpHeight = smallJumpHeight;
                break;

            case SizeStates.medium:
                gameObject.transform.localScale = mediumSize;
                charMovement.jumpHeight = mediumJumpHeight;
                break;

            case SizeStates.big:
                gameObject.transform.localScale = bigSize;
                charMovement.jumpHeight = bigJumpHeight;
                break;

            default:
                break;
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerPodium")
        {
            GetComponent<MeshRenderer>().material = other.GetComponent<MeshRenderer>().material;
            powerState = other.GetComponent<PodiumColour>().powerState;
            AudioManager.Instance.Play(colourChangeAudio);
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
                        gameObject.transform.localScale = mediumSize;
                        charMovement.jumpHeight = mediumJumpHeight;
                        AudioManager.Instance.Play(getBiggerAudio);
                        break;

                    case SizeStates.medium:
                        cubeSize = SizeStates.big;
                        gameObject.transform.localScale = bigSize;
                        charMovement.jumpHeight = bigJumpHeight;
                        AudioManager.Instance.Play(getBiggerAudio);
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
                        gameObject.transform.localScale = mediumSize;
                        charMovement.jumpHeight = mediumJumpHeight;
                        AudioManager.Instance.Play(getSmallerAudio);
                        break;

                    case SizeStates.medium:
                        cubeSize = SizeStates.small;
                        gameObject.transform.localScale = smallSize;
                        charMovement.jumpHeight = smallJumpHeight;
                        AudioManager.Instance.Play(getSmallerAudio);
                        break;

                    default:
                        break;
                }
            }
        }
    }

}
