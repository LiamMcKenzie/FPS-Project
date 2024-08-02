using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the player's movement
/// </summary>
/// <remarks>
/// This script is based on the Quake 3 movement system translated by Isaiah Kelly.:
/// https://github.com/IsaiahKelly/quake3-movement-for-unity
/// </remarks>
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

    [SerializeField] private float friction = 6;
    [SerializeField] private float gravity = 20;
    [SerializeField] private float jumpForce = 8;

    [SerializeField] public MovementSettings groundSettings = new MovementSettings(10, 10, 10);
    [SerializeField] public MovementSettings airSettings = new MovementSettings(10, 2, 2);
    [SerializeField] public MovementSettings strafeSettings = new MovementSettings(1, 25, 25);
    

    private Vector3 moveInput;
    private Vector3 playerVelocity;
    private Vector3 moveDirectionNormal;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if(characterController.isGrounded)
        {
            GroundMove();
            if(Input.GetButtonDown("Jump"))
            {
                playerVelocity.y = jumpForce;
            }
        }
        else
        {
            AirMove();
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundMove()
    {
        ApplyFriction(1f);

        Vector3 wishDir = new Vector3(moveInput.x, 0, moveInput.z);
        
        wishDir = transform.TransformDirection(wishDir);
        wishDir.Normalize();
        moveDirectionNormal = wishDir;
        
        float wishSpeed = wishDir.magnitude;
        wishSpeed *= groundSettings.maxSpeed;

        Accelerate(wishDir, wishSpeed, groundSettings.acceleration);
    }

    void AirMove()
    {
        float accel;
        
        Vector3 wishDir = new Vector3(moveInput.x, 0, moveInput.z);
        wishDir = transform.TransformDirection(wishDir);
        
        float wishSpeed = wishDir.magnitude;
        wishSpeed *= airSettings.maxSpeed;

        wishDir.Normalize();
        moveDirectionNormal = wishDir;

        float wishSpeed2 = wishSpeed;
        if(Vector3.Dot(playerVelocity, wishDir) < 0)
        {
            accel = airSettings.deceleration;
        }
        else
        {
            accel = airSettings.acceleration;
        }

        if(moveInput.z == 0 && moveInput.x == 0)
        {
            if(wishSpeed > strafeSettings.maxSpeed)
            {
                wishSpeed = strafeSettings.maxSpeed;
            }

            accel = strafeSettings.acceleration;
        }

        Accelerate(wishDir, wishSpeed, accel);

        playerVelocity.y -= gravity * Time.deltaTime;
    }

    private void ApplyFriction(float t)
    {
        Vector3 vec = playerVelocity;
        vec.y = 0;
        float speed = vec.magnitude;
        float drop = 0;

        if(characterController.isGrounded)
        {
            float control = (speed < groundSettings.deceleration) ? groundSettings.deceleration : speed;
            drop = control * friction * Time.deltaTime * t;
        }

        float newSpeed = speed - drop;
        friction = newSpeed;
        if (newSpeed < 0)
        {
            newSpeed = 0;
        }

        if (speed > 0)
        {
            newSpeed /= speed;
        }

        playerVelocity.x *= newSpeed;
        playerVelocity.z *= newSpeed;
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
