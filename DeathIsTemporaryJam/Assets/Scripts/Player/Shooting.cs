using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParent;

    [SerializeField] float bulletSpeed;
    [SerializeField] float shootCooldown;

    private Vector2 direction;



    void Update()
    {
        
    }


    void Shoot()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
/*        Instantiate(bullet, );*/

    }
}
