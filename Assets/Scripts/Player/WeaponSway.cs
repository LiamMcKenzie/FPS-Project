using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float amount = 0.02f;
    public float maxAmount = 0.03f;
    public float smoothAmount = 6f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if(GameManager.instance.CanControlPlayer()) //only moves weapon if player has control
        {
            Sway();
        }
    }

    void Sway()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount * (SaveManager.instance.mouseSensitivity / 2);
        float movementY = -Input.GetAxis("Mouse Y") * amount * (SaveManager.instance.mouseSensitivity / 2);
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }
}
