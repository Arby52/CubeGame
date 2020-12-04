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
    public Transform cameraHeightRotation;
    public Transform player;

    private void Start()
    {
        mouseX = 0;
        mouseY = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        //Camera Stuff
        mouseX += Input.GetAxis("Mouse X") * xSensitivity;

        if (invertYAxis)
            mouseY += Input.GetAxis("Mouse Y") * ySensitivity;
        else
            mouseY -= Input.GetAxis("Mouse Y") * ySensitivity;

        mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);

        transform.LookAt(cameraHeightRotation);

        cameraHeightRotation.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
