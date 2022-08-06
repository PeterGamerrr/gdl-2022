using Assets.Scripts;
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
    [SerializeField] int damage;
    [SerializeField] int piercing;

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
            timeStamp = Time.time + shootCooldown - Mathf.Min(Mathf.Sqrt(StatManager.Instance.GunRpm),(float) (shootCooldown * 0.8));
        }
    }

    void Fire()
    {
        Bullet bulletScript;
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bulletObject = Instantiate(bullet, shootingPoint.position, transform.rotation, bulletParent);
        bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.Damage = damage;
        bulletScript.Piercing = piercing;
        bulletRB = bulletObject.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(direction * bulletSpeed);

    }
}
