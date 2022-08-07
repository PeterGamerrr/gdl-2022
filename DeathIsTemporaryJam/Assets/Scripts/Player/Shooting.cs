using Assets.Scripts;
using System;
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

    private int cooldownMul = 0;
    private int piercingMul = 0;
    private int damageMul = 0;

    private void Start()
    {
        StatManager.Instance.UpGradeEvent.AddListener(OnUpgradeEvent);
    }

    private void OnUpgradeEvent()
    {
        cooldownMul = StatManager.Instance.GunRpm;
        piercingMul = StatManager.Instance.Piercing;
        damageMul = StatManager.Instance.Damage;
    }

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
            var x = StatManager.Instance.GunRpm;
            timeStamp = Time.time + shootCooldown - 0.5f*(0.3f* cooldownMul) /(0.3f* cooldownMul + 2);
        }
    }

    void Fire()
    {
        Bullet bulletScript;
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bulletObject = Instantiate(bullet, shootingPoint.position, transform.rotation, bulletParent);
        bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.Damage = damage + (int) (damage * 0.2 * damageMul);
        bulletScript.Piercing = piercing + piercingMul;
        bulletRB = bulletObject.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(direction.normalized * bulletSpeed);
    }
}
