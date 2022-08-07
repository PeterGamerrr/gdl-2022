using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;

    [SerializeField] float bulletSpeed;
    [SerializeField] float shootCooldown;
    [SerializeField] int amountOfBullets;
    [SerializeField] int damage;

    private Vector2 direction;

    private Rigidbody2D bulletRB;
    private GameObject bulletObject;

    private float timeStamp = 0;

    public Transform target;


    void Update()
    {

    }


    public void Shoot()
    {
        Debug.LogWarning("Shoot Method Call");
        if (timeStamp <= Time.time)
        {
            Fire();
            timeStamp = Time.time + shootCooldown;
        }
    }

    void Fire()
    {
        Debug.LogWarning("Fired Bullet");
        Bullet bulletScript;
        direction = target.position - transform.position;
        bulletObject = Instantiate(bullet, shootingPoint.position, transform.rotation);
        bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.Damage = damage;
        bulletRB = bulletObject.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(direction.normalized * bulletSpeed);
    }
}
