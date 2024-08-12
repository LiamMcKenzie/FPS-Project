using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public int damage = 10;
    public float fireRate = 0.5f;
    private bool isShooting = false;
    private bool bufferedShot = false;
    public GameObject pistolGameObject; //Holds the pistol game object. 

    public bool fullyAutomatic = false; 
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //NEEDS REFACTORING. Use different input system. also needs to be modular for different weapon types
        {
            BufferShot();
        }

        if(fullyAutomatic && Input.GetMouseButton(0)) //this might be a bad way of doing this.
        {
            BufferShot();
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

        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            Debug.DrawRay(rayOrigin, rayDirection, Color.red, duration: 10f);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
