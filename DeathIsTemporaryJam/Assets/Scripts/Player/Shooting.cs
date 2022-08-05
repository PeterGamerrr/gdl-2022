using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletParent;
    [SerializeField] Transform shootingPoint;

    [SerializeField] float bulletSpeed;
    [SerializeField] float shootCooldown;
    [SerializeField] int amountOfBullets;

    private Vector2 direction;

    private Rigidbody2D bulletRB;
    private GameObject bulletObject;

    private float timeStamp = 0;


    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }*/

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    
    void Shoot()
    {
        if (timeStamp <= Time.time)
        {
            Fire();
            timeStamp = Time.time + shootCooldown;
        }
    }

    void Fire()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bulletObject = Instantiate(bullet, shootingPoint.position, transform.rotation, bulletParent);
        bulletRB = bulletObject.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(direction * bulletSpeed);

    }
}
