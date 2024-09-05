using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Pistol,
    Shotgun,
    RocketLauncher
}

public class PlayerWeapon : MonoBehaviour
{
    public float fireRate = 0.5f; //NOTE: this needs to be refactored when I add other weapons, as each weapon will have its own fire rateq
    
    private float shotCooldown; //used for fire rate
    private bool isShooting = false;
    private bool bufferedShot = false;

    public WeaponType currentWeapon = WeaponType.Pistol; //defaults to the pistol

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: assign upgradable values. (this could be a seperate function)


        //only allow input when the player has control
        if (GameManager.instance.CanControlPlayer())
        {
            ShootInput();
            WeaponSwitchInput();
            
            HandleShotCooldown(); //the shot cooldown only decreases when the player has control
        }
    }

    /// <summary>
    /// This function handles the weapon switching input
    /// </summary>
    void WeaponSwitchInput()
    {
        //Alpha 1-3 refers to the number keys at the top of the keyboard (as opposed to the numpad) 
        //This is hard coded because I don't plan on supporting more than mouse and keyboard. I also only plan on having 3 weapons 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(WeaponType.Pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(WeaponType.Shotgun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(WeaponType.RocketLauncher);
        }
    }

    public void SwitchWeapon(WeaponType weapon)
    {
        currentWeapon = weapon;
        ResetShotCooldown();
    }


    
    //=====GENERIC SHOOTING FUNCTIONS=====

    /// <summary>
    /// Decreases the shot cooldown and updates isShooting
    /// </summary>
    void HandleShotCooldown()
    {
        if(shotCooldown > 0)
        {
            isShooting = true;
            shotCooldown -= Time.deltaTime;
        }else
        {
            isShooting = false;
            shotCooldown = 0; //just in case
        }
    }


    /// <summary>
    /// Used when switching weapons so the shot cooldown is reset.
    /// </summary>
    void ResetShotCooldown()
    {
        shotCooldown = 0;
    }

    
    void AddShotCooldown(float amount) //Code to calculate the shot cooldown per weapon could also be calculated here
    {
        shotCooldown += amount;
    }

    /// <summary>
    /// Handles the shooting input, buffering is handled here as well.
    /// </summary>
    void ShootInput()
    {
        if (Input.GetMouseButtonDown(0)) //buffers a shot when pressing the left mouse button
        {
            BufferShot();
        }

    
        if(bufferedShot && !isShooting) //if a shot is buffered and the player is not currently shooting, shoot
        {
            Shoot();
            bufferedShot = false;
        }
    }


    /// <summary>
    /// Calculates shot buffering (if the player tried to shoot while shooting, it will shoot as soon as possible)
    /// </summary>
    public void BufferShot()
    {
        if (!isShooting)
        {
            Shoot();
        }
        else
        {
            bufferedShot = true;
        }
    }


    /// <summary>
    /// Set firerate and play animation
    /// </summary>
    public void Shoot()
    {
        AddShotCooldown(fireRate);
        
        //TODO: play animation here

        //TODO: Switch case for different weapons and what function to shoot.
    }

}
