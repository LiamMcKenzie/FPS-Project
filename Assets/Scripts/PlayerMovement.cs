using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public struct MovementSettings
    {
        public float maxSpeed;
        public float acceleration;
        public float deceleration;
        public MovementSettings(float maxSpeed, float acceleration, float deceleration)
        {
            this.maxSpeed = maxSpeed;
            this.acceleration = acceleration;
            this.deceleration = deceleration;
        }
    }

    [SerializeField] public MovementSettings groundSettings = new MovementSettings(10, 10, 10);
    

    public float moveSpeed = 5f;
    private Vector3 moveInput;
    private Vector3 playerVelocity;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        GroundMove();

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundMove()
    {
        Vector3 wishDir = new Vector3(moveInput.x, 0, moveInput.z);
        wishDir = transform.TransformDirection(wishDir);
        wishDir.Normalize();
        
        float wishSpeed = wishDir.magnitude;
        wishSpeed *= groundSettings.maxSpeed;

        Accelerate(wishDir, wishSpeed, groundSettings.acceleration);



    }

    /// <summary>
    /// calculates acceleration based on desired speed and direction.
    /// </summary>
    /// <param name="targetDir"></param>
    /// <param name="targetSpeed"></param>
    /// <param name="accel"></param>
    private void Accelerate(Vector3 targetDir, float targetSpeed, float accel)
    {
        float currentSpeed = Vector3.Dot(playerVelocity, targetDir);
        float addSpeed = targetSpeed - currentSpeed;
        if (addSpeed <= 0)
        {
            return;
        }

        float accelSpeed = accel * Time.deltaTime * targetSpeed;
        if (accelSpeed > addSpeed)
        {
            accelSpeed = addSpeed;
        }
        
        playerVelocity.x += accelSpeed * targetDir.x;
        playerVelocity.z += accelSpeed * targetDir.z;
    }
}
