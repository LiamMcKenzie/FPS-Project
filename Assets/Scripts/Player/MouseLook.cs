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
    }

    void Update()
    {
        if(GameManager.instance.CanControlPlayer()) //only able to look if player has control.
        {
            Look();
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);

        rotX -= mouseY * mouseSensitivity;
        rotX = Mathf.Clamp(rotX, -verticalLookLimit, verticalLookLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
    }
}
