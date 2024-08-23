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
    public int damage = 10;
    public float fireRate = 0.5f;
    private bool isShooting = false;
    private bool bufferedShot = false;
    public GameObject pistolGameObject; //Holds the pistol game object. 

    public bool fullyAutomatic = false; 
    public float randomSpread = 0.1f; 
    public Transform bulletSpawnPoint; //point on the gun where the particle should start from (barrel of gun)

    [SerializeField] private TrailRenderer trailRenderer;
    
    void Update()
    {
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
        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.transform.position;


        Vector3 rayDirection = Camera.main.transform.forward;

        rayDirection.x += Random.Range(-randomSpread, randomSpread);
        rayDirection.y += Random.Range(-randomSpread, randomSpread);

        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            TrailRenderer trail = Instantiate(trailRenderer, bulletSpawnPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit.point));
            Debug.Log(hit.transform.name);

            Debug.DrawRay(rayOrigin, rayDirection, Color.red, duration: 10f);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }else{ //if nothing was hit
            TrailRenderer trail = Instantiate(trailRenderer, bulletSpawnPoint.position, Quaternion.identity);
            Vector3 hitPoint = rayOrigin + (rayDirection * 1000); //if nothing was hit, the bullet will travel 1000 units forward
            StartCoroutine(SpawnTrail(trail, hitPoint));
        }

        
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint)
    {
        Vector3 startPos = trail.transform.position;
        float distance = Vector3.Distance(startPos, hitPoint);
        float remaingDistance = distance;

        float bulletSpeed = 100f;

        while (remaingDistance >= 0) //while bullet is stil moving towards target
        {
            trail.transform.position = Vector3.MoveTowards(trail.transform.position, hitPoint, bulletSpeed * Time.deltaTime);

            remaingDistance = Vector3.Distance(trail.transform.position, hitPoint); //updates the distance to the target
             
            yield return null;
        }

        Destroy(trail.gameObject,trail.time); //trail.time is how long it takes the trail to fade out. So it will destroy the trail after its fully faded out.
    }
}
