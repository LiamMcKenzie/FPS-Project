using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public int damage = 10;
    public GameObject pistolGameObject; //Holds the pistol game object. 
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //NEEDS REFACTORING. Use different input system. also needs to be modular for different weapon types
        {
            Shoot();
        }
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
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
