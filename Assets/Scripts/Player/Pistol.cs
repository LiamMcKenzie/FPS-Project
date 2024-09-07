using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the shooting functionality for the pistol. 
/// TODO: refactor this script to allow different weapon types. The code is already designed with future weapons in mind, but it will need to be moved to new scripts.
/// </summary>
/// 
/// <remarks>
/// Bullet tracer code from: https://www.youtube.com/watch?v=cI3E7_f74MA
/// </remarks>
public class Pistol : MonoBehaviour
{
    public float damage = 20;
    public float fireRate = 0.05f;
    private bool isShooting = false;
    private bool bufferedShot = false;
    public GameObject pistolGameObject; //Holds the pistol game object. 

    public bool fullyAutomatic = false; 
    public bool bulletPiercing = false; 
    public float randomSpread = 0.1f; 
    public Transform bulletSpawnPoint; //point on the gun where the particle should start from (barrel of gun)

    [SerializeField] private TrailRenderer trailRenderer; //the trail prefab

    [HideInInspector]public LayerMask layerMask;
    
    void Start()
    {
        layerMask = LayerMask.GetMask("Enemy", "World"); //gets the layer mask for the enemy layer and world layer. Layermasks are index values
    }

    void Update()
    {
        //Assigning Upgrade Values
        damage = GameManager.instance.GetUpgradeValue("Damage", UpgradeSection.Pistol); 
        fireRate = GameManager.instance.GetUpgradeValue("Fire Rate", UpgradeSection.Pistol); 
        randomSpread = GameManager.instance.GetUpgradeValue("Bullet Spread", UpgradeSection.Pistol); 
        fullyAutomatic = GameManager.instance.GetUpgradeValue("Automatic", UpgradeSection.Pistol) == 1; 
        bulletPiercing = GameManager.instance.GetUpgradeValue("Piercing", UpgradeSection.Pistol) == 1;

        //Only allows shooting input while the player has control.
        if(GameManager.instance.CanControlPlayer())
        {
            ShootInput();
        }
    }

    /// <summary>
    /// Handles shooting input, buffering is handled here as well.
    /// </summary>
    void ShootInput()
    {
        if (Input.GetMouseButtonDown(0)) //NEEDS REFACTORING. Use different input system. also needs to be modular for different weapon types
        {
            BufferShot();
        }

        if(fullyAutomatic && Input.GetMouseButton(0) && !isShooting) //this might be a bad way of doing this.
        {
            StartCoroutine(NewShoot()); //with automatic firing, it will always try to buffer a shot. So it skips the buffering
        }

        if(bufferedShot && !isShooting)
        {
            StartCoroutine(NewShoot());
            bufferedShot = false;
        }
    }

    public void BufferShot()
    {
        if(!isShooting)
        {
            StartCoroutine(NewShoot());
        }
        else
        {
            bufferedShot = true;
        }
    }

    IEnumerator NewShoot()
    {
        isShooting = true;
        Shoot();
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }

    /// <summary>
    /// Plays shoot animation and calls ShootBullet();
    /// NOTE: A shotgun can be added by calling ShootBullet() in a for loop.
    /// </summary>
    public void Shoot()
    {
        try //This code is prone to errors so I used a try catch to be safe.
        {
            pistolGameObject.GetComponent<Animator>().SetTrigger("Shoot"); 
        }
        catch
        {
            Debug.LogError("Pistol GameObject not found, animation can't play");
        }
        ShootBullet();
    }

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
    /// Shoots a single bullet raycast, applies damage, and spawns a bullet trail
    /// </summary>
    public void ShootBullet()
    {
        //Ray Position and Direction
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        rayDirection.x += Random.Range(-randomSpread, randomSpread);
        rayDirection.y += Random.Range(-randomSpread, randomSpread);


        //Sends out a raycastAll, which returns all objects hit by a raycast. The hits are unsorted by default.
        RaycastHit[] hits;
        hits = SortRaycastHits(Physics.RaycastAll(rayOrigin, rayDirection, Mathf.Infinity, layerMask)); //sorts the hits by distance


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


        //Spawn Bullet Trail
        Vector3 hitPoint = rayOrigin + (rayDirection * 1000); //if nothing was hit, the bullet will travel 1000 units forward

        if(hits.Length > 0) //if the raycast hit something, set the hitpoint to the furthest hit.
        {
            hitPoint = hits[hits.Length - 1].point; //if 5 objects were hit, hits.length will be 5. but the highest index would be 4. 
        }

        TrailRenderer trail = Instantiate(trailRenderer, bulletSpawnPoint.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trail, hitPoint));
    }   

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
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
