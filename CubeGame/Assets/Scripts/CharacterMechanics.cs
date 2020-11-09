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
    float lastTime = 0;

    [SerializeField]
    PowerStates powerState;

    public SizeStates cubeSize = SizeStates.medium;

    //Sizes
    [SerializeField]
    Vector3 smallSize;
    [SerializeField]
    Vector3 mediumSize;
    [SerializeField]
    Vector3 bigSize;

    //Jump Heights
    [SerializeField]
    float baseSmallJumpHeight;
    [SerializeField]
    float baseMediumJumpHeight;
    [SerializeField]
    float baseBigJumpHeight;

    [SerializeField]
    float upgradedSmallJumpHeight;
    [SerializeField]
    float upgradedMediumJumpHeight;
    [SerializeField]
    float upgradedBigJumpHeight;

    float smallJumpHeight;
    float mediumJumpHeight;
    float bigJumpHeight;

    //Speed
    [SerializeField]
    float baseSpeed;
    [SerializeField]
    float upgradedSpeed;

    //Sound Effects
    [SerializeField]
    AudioClip getBiggerAudio;
    [SerializeField]
    AudioClip getSmallerAudio;
    [SerializeField]
    AudioClip colourChangeAudio;

    void Start()
    {
        smallJumpHeight = baseSmallJumpHeight;
        mediumJumpHeight = baseMediumJumpHeight;
        bigJumpHeight = baseBigJumpHeight;

        charMovement = GetComponent<CharacterMovement>();
        charMovement.jumpHeight = mediumJumpHeight;
        charMovement.speed = baseSpeed;
        powerState = PowerStates.none;

    }

    public void ChangeColour(Material _material, PowerStates _state)
    {
        PowerStates prevState = powerState;
        GetComponent<MeshRenderer>().material = _material;
        powerState = _state;
        AudioManager.Instance.Play(colourChangeAudio);

        //Remove the effects of the previous upgrade
        switch (prevState)
        {
            case PowerStates.speed:
                charMovement.speed = baseSpeed;
                break;

            case PowerStates.jumpHeight:
                smallJumpHeight = baseSmallJumpHeight;
                mediumJumpHeight = baseMediumJumpHeight;
                bigJumpHeight = baseBigJumpHeight;
                switch (cubeSize)
                {
                    case SizeStates.big:
                        charMovement.jumpHeight = bigJumpHeight;
                        break;
                    case SizeStates.medium:
                        charMovement.jumpHeight = mediumJumpHeight;
                        break;
                    case SizeStates.small:
                        charMovement.jumpHeight = smallJumpHeight;
                        break;
                }                
                break;

            default:
                break;
        }

        //Add the effects of the new upgrade
        switch (_state)
        {
            case PowerStates.speed:
                charMovement.speed = upgradedSpeed;
                break;

            case PowerStates.jumpHeight:
                smallJumpHeight = upgradedSmallJumpHeight;
                mediumJumpHeight = upgradedMediumJumpHeight;
                bigJumpHeight = upgradedBigJumpHeight;
                switch (cubeSize)
                {
                    case SizeStates.big:
                        charMovement.jumpHeight = bigJumpHeight;
                        break;
                    case SizeStates.medium:
                        charMovement.jumpHeight = mediumJumpHeight;
                        break;
                    case SizeStates.small:
                        charMovement.jumpHeight = smallJumpHeight;
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void MediumBigFlip()
    {       
        switch (cubeSize)
        {                
            case SizeStates.big:
                cubeSize = SizeStates.medium;
                gameObject.transform.localScale = mediumSize;
                charMovement.jumpHeight = mediumJumpHeight;
                AudioManager.Instance.Play(getSmallerAudio);
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

    public void MediumSmallFlip()
    {        
        switch (cubeSize)
        {
            case SizeStates.small:
                cubeSize = SizeStates.medium;
                gameObject.transform.localScale = mediumSize;
                charMovement.jumpHeight = mediumJumpHeight;
                AudioManager.Instance.Play(getBiggerAudio);
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
