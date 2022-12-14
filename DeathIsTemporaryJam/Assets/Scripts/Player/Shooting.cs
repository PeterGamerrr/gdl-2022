using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletParent;
    [SerializeField] Transform shootingPoint;
    [SerializeField] PlayerController playerController;
    [SerializeField] AudioSource castSound;

    [SerializeField] float bulletSpeed;
    [SerializeField] float shootCooldown;
    [SerializeField] int amountOfBullets;
    [SerializeField] int damage;
    [SerializeField] int piercing;
    [SerializeField] public bool hasExplosion;

    private Vector2 direction;

    private Rigidbody2D bulletRB;
    private GameObject bulletObject;

    private float timeStamp = 0;

    private int cooldownMul = 0;
    private int piercingMul = 0;
    private int damageMul = 0;
    private int explosionMul = 0;
    Quaternion shootRotation;

    public int explosionDamage;

    private bool toggleShooting = false;

/*    PlayerControls playerControls;
    PlayerControls.PlayerActions playerActions;*/

    private void Start()
    {
        StatManager.Instance.UpGradeEvent.AddListener(OnUpgradeEvent);
        /*playerControls = new PlayerControls();
        playerActions = playerControls.Player;*/
    }

    private void OnUpgradeEvent()
    {
        cooldownMul = StatManager.Instance.GunRpm;
        piercingMul = StatManager.Instance.Piercing;
        damageMul = StatManager.Instance.Damage;
        explosionMul = StatManager.Instance.Explosion;
    }

    void Update()
    {
        if (toggleShooting)
        {
            Shoot();
        }
    }



    public void ShootOnInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            toggleShooting = true;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            toggleShooting = false;
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
        castSound.Play();
        direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - shootingPoint.transform.position;
        shootRotation = RotateTowardsMouse();
        bulletObject = Instantiate(bullet, shootingPoint.position, shootRotation, bulletParent); 
        bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.hasExplosion = hasExplosion;
        bulletScript.Damage = damage + (int) (damage * 0.2 * damageMul);
        bulletScript.Piercing = piercing + piercingMul;
        bulletScript.ExplosionDamage = explosionDamage;
        bulletRB = bulletObject.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(direction.normalized * bulletSpeed + playerController.movementVel);
        GameManager.Instance.Bullets.Add(bulletObject);
        hasExplosion = false;
    }

    Quaternion RotateTowardsMouse()
    {
        direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - shootingPoint.transform.position;
        direction.Normalize();
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotation_z);
    }
}
