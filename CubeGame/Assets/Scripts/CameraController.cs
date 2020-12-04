using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xSensitivity;
    public float ySensitivity;
    public float minYAngle;
    public float maxYAngle;

    public bool invertYAxis;

    float mouseX;
    float mouseY;
    public Transform cameraYMovement;
    public Transform player;
    public Transform originalPos;
    CharacterMechanics mechanics;
    Camera cam;
    float baseDist;

    Vector3 giz1;
    Vector3 giz2;
    Vector3 giz3;


    private void Start()
    {
        mouseX = 0;
        mouseY = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mechanics = player.gameObject.GetComponent<CharacterMechanics>();
        cam = GetComponent<Camera>();
        Vector3 baseOffset = cam.transform.position - cameraYMovement.position;
        baseDist = baseOffset.magnitude;
    }

    void LateUpdate()
    {
        if (!mechanics.isPaused)
        {           

            //Camera Stuff
            mouseX += Input.GetAxis("Mouse X") * xSensitivity;

            if (invertYAxis)
                mouseY += Input.GetAxis("Mouse Y") * ySensitivity;
            else
                mouseY -= Input.GetAxis("Mouse Y") * ySensitivity;

            mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);

            transform.LookAt(cameraYMovement);

            cameraYMovement.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);

            //Move Camera Closer if it would intersect with a wall.
            Vector3 playerToCam = cam.transform.position - cameraYMovement.position;
            Vector3 behindCam = cam.transform.position + playerToCam.normalized;

            if (Physics.Linecast(cameraYMovement.position, cam.transform.position, LayerMask.NameToLayer("Player")))
            {
                if (playerToCam.magnitude >= 0.1)
                {
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, cameraYMovement.transform.position, 0.2f);
                }
            }
            else if (!(Physics.Linecast(cam.transform.position, behindCam, LayerMask.NameToLayer("Player"))))
            {
                if (playerToCam.magnitude <= baseDist)
                {
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, originalPos.position, 0.1f);
                }
            }

        }
    }

}
