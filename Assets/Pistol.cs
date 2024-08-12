using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //NEEDS REFACTORING. Use different input system. also needs to be modular for different weapon types
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        ShootBullet();
    }
    public void ShootBullet()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            Debug.Log("Hit " + hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit Enemy");
            }
        }
    }
}
