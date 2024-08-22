using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script rotates the camera based on mouse input. 
/// </summary>
/// 
/// <remarks>
/// USE: attach this script to the main camera on the main menu.
/// </remarks>

public class MenuCamera : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    public float rotateAmount = 5f;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    Vector2 ScreenToNDC(Vector2 screenPos)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float ndcX = (screenPos.x / screenWidth) * 2 - 1;
        float ndcY = (screenPos.y / screenHeight) * 2 - 1;
        ndcX = Mathf.Clamp(ndcX, -1f, 1f);
        ndcY = Mathf.Clamp(ndcY, -1f, 1f);
        return new Vector2(ndcX, ndcY);
    }

    float GetAspectRatio()
    {
        return (float)Screen.width / Screen.height;
    }

    void Update()
    {
        float targetXRotation = 0;
        float targetYRotation = 0; 

        targetXRotation = ScreenToNDC(Input.mousePosition).x * rotateAmount;
        targetYRotation = ScreenToNDC(Input.mousePosition).y * rotateAmount / GetAspectRatio();

        Quaternion targetRotation = initialRotation * Quaternion.Euler(-targetYRotation, targetXRotation, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
