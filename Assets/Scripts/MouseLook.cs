using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public float verticalLookLimit = 80f;

    private float rotX =0f;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        rotX -= mouseY * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -verticalLookLimit, verticalLookLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
    }
}
