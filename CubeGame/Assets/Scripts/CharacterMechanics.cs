using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class CharacterMechanics : MonoBehaviour
{
    public enum SizeStates
    {
        small,
        medium,
        big
    };

    MeshRenderer meshRenderer;
    CharacterMovement charMovement;

    public int coinAmount;

    public PowerStates powerState;

    public SizeStates cubeSize = SizeStates.medium;

    //Sizes
    public Vector3 smallSize;
    public Vector3 mediumSize;
    public Vector3 bigSize;

    //Jump Heights
    public float baseSmallJumpHeight;
    public float baseMediumJumpHeight;
    public float baseBigJumpHeight;

    public float upgradedSmallJumpHeight;
    public float upgradedMediumJumpHeight;
    public float upgradedBigJumpHeight;

    float smallJumpHeight;
    float mediumJumpHeight;
    float bigJumpHeight;

    //Speed
    public float baseSpeed;
    public float upgradedSpeed;

    [HideInInspector]
    public Vector3 checkpointPosition;

    //Sound Effects
    public AudioClip getBiggerAudio;
    public AudioClip getSmallerAudio;
    public AudioClip colourChangeAudio;
    public AudioClip levelStartAudio;


    public GameObject pauseMenu;
    [HideInInspector]
    public bool isPaused = false;

    public static float seconds = 0;

    public TMP_Text coinText;
    public TMP_Text timerText;


    void Start()
    {
        AudioManager.Instance.Play(levelStartAudio);

        meshRenderer = GetComponent<MeshRenderer>();

        smallJumpHeight = baseSmallJumpHeight;
        mediumJumpHeight = baseMediumJumpHeight;
        bigJumpHeight = baseBigJumpHeight;

        charMovement = GetComponent<CharacterMovement>();
        charMovement.jumpHeight = mediumJumpHeight;
        charMovement.speed = baseSpeed;
        powerState = PowerStates.none;

        //Original checkpoint position is the first spawn of the cube.
        checkpointPosition = transform.position;
        coinText.text = CoinRotation.currentScore.ToString() + " - " + CoinRotation.totalCoins.ToString();
    }

    private void Update()
    {
        seconds += Time.deltaTime;

        System.TimeSpan timeInnit = System.TimeSpan.FromSeconds(seconds);

        timerText.text = timeInnit.ToString(@"mm\-ss");


        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isPaused = true;
            } else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                isPaused = false;
            }

        }
    }

    public void Die()
    {
        GoToCheckpoint();
    }

    public void GoToCheckpoint()
    {
        charMovement.controller.enabled = false;
        gameObject.transform.position = checkpointPosition;
        charMovement.controller.enabled = true;
    }

    public void SetCheckpoint(Vector3 _position)
    {
        checkpointPosition = _position;
    }

    public void ChangeColour(Material _material, PowerStates _state)
    {
        PowerStates prevState = powerState;
        meshRenderer.material = _material;
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

            case PowerStates.sticky:
                charMovement.isSticky = false;
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

            case PowerStates.sticky:
                charMovement.isSticky = true;
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
