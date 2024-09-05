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
    public float fireRate = 0.5f; //NOTE: this needs to be refactored when I add other weapons, as each weapon will have its own fire rate
    public float randomSpread = 0.1f; //NOTE: same as above, needs to be refactored
    
    private float shotCooldown; //used for fire rate
    private bool isShooting = false;
    private bool bufferedShot = false;

    public WeaponType currentWeapon = WeaponType.Pistol; //defaults to the pistol

    [HideInInspector]public LayerMask layerMask; //layer mask for raycasting

    void Start()
    {
        layerMask = LayerMask.GetMask("Enemy", "World"); //gets the layer mask for the enemy layer and world layer.
    }

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
        switch (currentWeapon)
        {
            case WeaponType.Pistol: 
                ShootBullet();
                break;

            case WeaponType.Shotgun:
                for (int i = 0; i < 5; i++) //shotgun shoots 5 bullets. TODO: use a variable for the number of bullets
                {
                    ShootBullet();
                }

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
        Vector3 rayOrigin = Camera.main,transform.position;
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

        StartCoroutine(SpawnTrail(trail, hitPoint)); 
    }

    /// <summary>
    /// Spawns a bullet trail that moves towards the hitpoint
    /// </summary>
    /// <param name="hitPoint"></param>
    /// <returns></returns>
    private IEnumerator SpawnTrail(Vector3 hitPoint)
    {
        TrailRenderer trail = Instantiate(trailRenderer, bulletSpawnPoint.position, Quaternion.identity);
        Destroy(trail.gameObject, 1f); //destroys the trail after 1 second

        Vector3 startPos = trail.transform.position;
        float distance = Vector3.Distance(startPos, hitPoint);
        float remaingDistance = distance;

        float bulletSpeed = 300f;
        

        while (remaingDistance >= 0 && trail != null) //while bullet is stil moving towards target
        {
            trail.transform.position = Vector3.MoveTowards(trail.transform.position, hitPoint, bulletSpeed * Time.deltaTime);

            remaingDistance = Vector3.Distance(trail.transform.position, hitPoint); //updates the distance to the target
            
            yield return null;
        }
    }
}
