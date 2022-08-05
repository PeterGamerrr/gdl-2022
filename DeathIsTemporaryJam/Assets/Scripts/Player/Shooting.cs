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

    private Vector2 direction;

    private Rigidbody2D bulletRB;
    private GameObject bulletObject;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bulletObject = Instantiate(bullet, shootingPoint.position, transform.rotation, bulletParent);
        bulletRB = bulletObject.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(direction * bulletSpeed);
    }
}
