using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static float xSensitivity = 5;
    public static float ySensitivity = 5;
    public static bool invertYAxis = false;

    public float minYAngle;
    public float maxYAngle;    

    float mouseX;
    float mouseY;
    public Transform cameraYMovement;
    public Transform player;
    public Transform originalPos;
    CharacterMechanics mechanics;
    Camera cam;
    float baseDist;

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
            //Rotating the camera with the axis inputs
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

            //Move Camera towards the player instantly the distance it needs to not be inside a wall
            if (Physics.Linecast(cameraYMovement.position, cam.transform.position, out RaycastHit hit, LayerMask.NameToLayer("Player")))
            {
                if (playerToCam.magnitude >= 0.1)
                {
                    float toMoveBy = Vector3.Distance(hit.point, cam.transform.position);
                    cam.transform.position = Vector3.MoveTowards(cam.transform.position, cameraYMovement.transform.position, toMoveBy);
                }
            } //If the camera isn't behind an object, and there isnt anything 1 unit behind the camera, smoothly move back by 0.1 units every frame.            
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
