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
    #region Upgrade Variables
    //TODO: the following variables need to be refactored to be modular for different weapon types
    public float damage = 20;
    public float fireRate = 0.5f; 
    public float randomSpread = 0.1f; 
    public bool bulletPiercing = false; 
    public bool fullyAutomatic = false;

    public int pelletsPerShot; //used for shotgun

    #endregion
    public Transform bulletSpawnPoint; //point on the gun where the particle should start from (barrel of gun)
    
    //Shot cooldown variables
    private float shotCooldown; //used for fire rate
    private bool isShooting = false;
    private bool isSwitching = false;
    private bool bufferedShot = false;
    private float bufferTimer = 0f;
    private float bufferAmount = 0.25f;

    public float switchSpeed = 0.5f;
    private float switchCooldown = 0;

    public WeaponType currentWeapon = WeaponType.Pistol; //defaults to the pistol

    private List<GameObject> activeParticles = new List<GameObject>();

    public GameObject bulletTrailPrefab;
    public GameObject muzzleFlashPrefab;

    public GameObject ShotgunObject; 
    public GameObject PistolObject;
    private Animator weaponAnimator;
    //public GameObject RocketLauncherObject;
    [HideInInspector] public LayerMask layerMask; //layer mask for raycasting


    void Start()
    {
        //SwitchWeapon(WeaponType.Pistol); //sets the default weapon
        PistolObject.SetActive(true);
        bulletSpawnPoint = PistolObject.transform.Find("Barrel Point");
        weaponAnimator = PistolObject.GetComponent<Animator>();
        layerMask = LayerMask.GetMask("Enemy", "World"); //gets the layer mask for the enemy layer and world layer.
    }

    void Update()
    {

        //TODO: assign upgradable values. (this could be a seperate function)
        AssignUpgradeValues();

        //only allow input when the player has control
        if (GameManager.instance.CanControlPlayer())
        {
            ShootInput();
            WeaponSwitchInput();

            HandleShotCooldown(); //the shot cooldown only decreases when the player has control
            SwitchWeaponCooldown();
            HandleBufferCooldown();
        }
    }

    /// <summary>
    /// Weapon stats are assigned based on the current weapon and upgrade level.
    /// </summary>
    void AssignUpgradeValues()
    {
        //I'm not sure what the best way of doing this is, I tried to think of a dynamic way of having variables for weapon stats.
        //shared values will be overwritten by the value for the current weapon.
        //values that aren't shared will always exist but won't be used when not in use. 
        switch (currentWeapon)
        {
            case WeaponType.Pistol:
                damage = GameManager.instance.GetUpgradeValue("Damage", UpgradeSection.Pistol);
                fireRate = GameManager.instance.GetUpgradeValue("Fire Rate", UpgradeSection.Pistol);
                randomSpread = GameManager.instance.GetUpgradeValue("Bullet Spread", UpgradeSection.Pistol);
                bulletPiercing = GameManager.instance.GetUpgradeValue("Piercing", UpgradeSection.Pistol) == 1;
                fullyAutomatic = GameManager.instance.GetUpgradeValue("Automatic", UpgradeSection.Pistol) == 1;
                break;

            case WeaponType.Shotgun:
                damage = GameManager.instance.GetUpgradeValue("Damage", UpgradeSection.Shotgun);
                fireRate = GameManager.instance.GetUpgradeValue("Fire Rate", UpgradeSection.Shotgun);
                randomSpread = GameManager.instance.GetUpgradeValue("Bullet Spread", UpgradeSection.Shotgun);
                pelletsPerShot = (int)GameManager.instance.GetUpgradeValue("Pellets Per Shot", UpgradeSection.Shotgun);
                break;

            // case WeaponType.RocketLauncher:
            //     damage = GameManager.instance.GetUpgradeValue("Damage", UpgradeSection.RocketLauncher);
            //     fireRate = GameManager.instance.GetUpgradeValue("Fire Rate", UpgradeSection.RocketLauncher);
            //     break;

            default:

                break;
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
        // else if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     SwitchWeapon(WeaponType.RocketLauncher);
        // }
    }

    public void SwitchWeapon(WeaponType weapon)
    {
        activeParticles.ForEach(particle => Destroy(particle)); //destroy all active particles. (Addresses a bug that causes muzzle flashes to repeat when switching weapons)

        if (currentWeapon == weapon) //if the player is trying to switch to the same weapon, return
        {
            return;
        }
        ResetShotCooldown();
        currentWeapon = weapon;

        PistolObject.SetActive(false);
        ShotgunObject.SetActive(false);
        //RocketLauncherObject.SetActive(false);
        switch (currentWeapon)
        {
            case WeaponType.Pistol:
                PistolObject.SetActive(true);
                bulletSpawnPoint = PistolObject.transform.Find("Barrel Point");
                weaponAnimator = PistolObject.GetComponent<Animator>();
                break;
            case WeaponType.Shotgun:
                ShotgunObject.SetActive(true);
                bulletSpawnPoint = ShotgunObject.transform.Find("Barrel Point");
                weaponAnimator = ShotgunObject.GetComponent<Animator>();
                break;
            case WeaponType.RocketLauncher:
                //RocketLauncherObject.SetActive(true);
                //bulletSpawnPoint = RocketLauncherObject.transform.Find("Barrel Point");
                break;
            default:
                break;
        }

        weaponAnimator.SetTrigger("Switch"); //play the switch animation
        switchCooldown = switchSpeed; //set the switch cooldown
        
    }

    //=====GENERIC SHOOTING FUNCTIONS=====

    /// <summary>
    /// Decreases the shot cooldown and updates isShooting
    /// </summary>
    void HandleShotCooldown()
    {
        if(shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        }else
        {
            isShooting = false;
            shotCooldown = 0; //just in case
        }
    }

    /// <summary>
    /// Weapon switch cooldown, used to prevent the player from switching weapons too quickly
    /// </summary>
    void SwitchWeaponCooldown()
    {
        if(switchCooldown > 0)
        {
            isSwitching = true;
            switchCooldown -= Time.deltaTime;
        }else
        {
            isSwitching = false;
            switchCooldown = 0; 
        }
    }


    /// <summary>
    /// Used when switching weapons so the shot cooldown is reset. 
    /// </summary>
    void ResetShotCooldown()
    {
        shotCooldown = 0;
        bufferedShot = false; //(shot buffer is reset, otherwise weapons would automatically shoot when switching)
    }

    /// <summary>
    /// Adds to the shot cooldown, takes in a float for cooldown amount
    /// </summary>
    /// <param name="amount"></param>
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
        
    
        if(bufferTimer > 0 && !isShooting && !isSwitching) //if a shot is buffered and the player is not currently shooting, shoot
        {
            Shoot();
            bufferedShot = false;
        }


        //if the weapon is fully automatic. 
        if(currentWeapon == WeaponType.Pistol && fullyAutomatic && Input.GetMouseButton(0) && !isShooting && !isSwitching) //NOTE: The "shotcooldown > 0 == false" is the same as checking IsShooting. I had some order of operation issues with isShooting.
        {
            Shoot();
        }
    }


    /// <summary>
    /// Calculates shot buffering (if the player tried to shoot while shooting, it will shoot as soon as possible)
    /// </summary>
    public void BufferShot()
    {
        if (!isShooting && !isSwitching) //if the player is not shooting or switching weapons, shoot. otherwise buffer the shot
        {
            Shoot();
        }
        else
        {
            bufferTimer = bufferAmount;
        }
    }

    void HandleBufferCooldown()
    {
        if(bufferTimer < 0)
        {
            bufferTimer = 0;
        }else
        {
            bufferTimer -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Set firerate and play animation
    /// </summary>
    public void Shoot()
    {
        AddShotCooldown(fireRate);
        isShooting = true;
        
        //TODO: play animation here
        weaponAnimator.SetTrigger("Shoot");
        GameObject muzzleFlashEffect = Instantiate(muzzleFlashPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation); //spawn muzzle flash

        muzzleFlashEffect.transform.SetParent(bulletSpawnPoint);

        activeParticles.Add(muzzleFlashEffect); 
        

        Destroy(muzzleFlashEffect, 2f); 

        switch (currentWeapon)
        {
            case WeaponType.Pistol: 
                ShootBullet();
                SoundManager.instance.PlayPistolShot();
                break;

            case WeaponType.Shotgun:
                for (int i = 0; i < pelletsPerShot; i++) //shotgun shoots 5 bullets. TODO: use a variable for the number of bullets
                {
                    ShootBullet();
                }

                SoundManager.instance.PlayShotgunShot();

                break;

            case WeaponType.RocketLauncher:
                //TODO: rocket launcher shoot function
                break;

            default:

                break;
        }
    }

    //=====BULLET SHOOTING FUNCTIONS===== (shared between pistol and shotgun)

    /// <summary>
    /// Sorts an array of raycast hits by distance from the camera. Used for piercing bullets.
    /// </summary>
    /// <remarks>
    /// This script was written using ChatGPT, since I've not used Array.Sort before.
    /// </remarks>
    /// <param name="hits"></param>
    /// <returns></returns>
    public RaycastHit[] SortRaycastHits(RaycastHit[] hits)
    {
        //Acording to this stackoverflow post, Array.sort uses an insertion sort for small arrays (our array is relatively small)
        //https://stackoverflow.com/questions/1854604/which-sorting-algorithm-is-used-by-nets-array-sort-method

        System.Array.Sort(hits, (hit1, hit2) =>
        {
            float distance1 = Vector3.Distance(Camera.main.transform.position, hit1.point);
            float distance2 = Vector3.Distance(Camera.main.transform.position, hit2.point);
            return distance1.CompareTo(distance2);
        });

        return hits;
    }
    
    /// <summary>
    /// Shoots a single raycast bullet and spawns a bullet trail.
    /// TODO: refactor to check variables for either pistol or shotgun
    /// </summary>
    public void ShootBullet()
    {
        //assign ray position and direction
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        //adds shot randomization, needs to be refactored to fetch the appropriate weapon's spread value
        rayDirection.x += Random.Range(-randomSpread, randomSpread);
        rayDirection.y += Random.Range(-randomSpread, randomSpread);

        //Sends out a raycastAll, which returns all objects hit by a raycast. The hits are unsorted by default.
        //Raycast all is used for shot piercing
        RaycastHit[] hits;
        hits = SortRaycastHits(Physics.RaycastAll(rayOrigin, rayDirection, Mathf.Infinity, layerMask)); //sorts the hits by distance\

        //Looping through all raycast hits, and either dealing damage or stopping loop
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.tag == "Enemy") //if the raycast has hit an enemy, deal damage
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }else //if the raycast didn't hit an enemy, it means it hit a wall. so break the loop
            {
                break;
            }

            if(bulletPiercing == false) //if player doesn't have bullet piercing stop the loop.
            {
                break;
            }
        }

        //Spawn bullet trail
        Vector3 hitPoint = rayOrigin + (rayDirection * 1000); //if nothing was hit, the bullet will travel 1000 units forward

        if(hits.Length > 0) //if the raycast hit something, set the hitpoint to the furthest hit.
        {
            hitPoint = hits[hits.Length - 1].point; //if 5 objects were hit, hits.length will be 5. but the highest index would be 4. 
        }

        //Spawn bullet trail, rotation is set to direction between bullet spawnpoint and hit point. 
        Instantiate(bulletTrailPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(hitPoint - bulletSpawnPoint.position));
    }
}
