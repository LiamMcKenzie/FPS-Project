using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// 
/// <remarks>
/// Bullet tracer code from: https://www.youtube.com/watch?v=cI3E7_f74MA
/// </remarks>
public class Pistol : MonoBehaviour
{
    public float damage = 10;
    public float fireRate = 0.5f;
    private bool isShooting = false;
    private bool bufferedShot = false;
    public GameObject pistolGameObject; //Holds the pistol game object. 

    public bool fullyAutomatic = false; 
    public bool bulletPiercing = false; 
    public float randomSpread = 0.1f; 
    public Transform bulletSpawnPoint; //point on the gun where the particle should start from (barrel of gun)

    [SerializeField] private TrailRenderer trailRenderer;

    public LayerMask layerMask;
    
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

        if(GameManager.instance.CanControlPlayer())
        {
            ShootInput();
        }
    }

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

    public void ShootBullet()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        rayDirection.x += Random.Range(-randomSpread, randomSpread);
        rayDirection.y += Random.Range(-randomSpread, randomSpread);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(rayOrigin, rayDirection, Mathf.Infinity, layerMask);


        //Bullet Raycast
        //NOTE: raycast all has a range from n to 1. n is the closest hit and 1 is the furthest hit. (n is number of hits)
        //TODO: piercing also allows shooting through walls, switch for loop to count down, and break if an enemy isn't hit.
        for (int i = 0; i < hits.Length; i++) //the highest value is the closest hit
        {
            // if(bulletPiercing == false)
            // {
            //     break;
            // }

            RaycastHit hit = hits[i];

            if(i + 1 == hits.Length || bulletPiercing == true) //get the closest hit, index starts at 0 but hits.length starts at 1. 
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
            }
            
            Debug.Log($"{hits.Length} - {i + 1}, {hits[i].transform.name}");

            
        }

        //Spawn Bullet Trail
        Vector3 hitPoint = rayOrigin + (rayDirection * 1000); //if nothing was hit, the bullet will travel 1000 units forward

        if(hits.Length > 0)
        {
            hitPoint = hits[0].point;
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
