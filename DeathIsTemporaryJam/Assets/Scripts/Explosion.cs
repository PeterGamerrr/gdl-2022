using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] public int damage;

    [SerializeField] bool canDamagePlayer;
    [SerializeField] bool canDamageEnemy;

    [SerializeField] AudioSource explosionSound;
    [SerializeField] GameObject explosionEffect;

    private Collider2D[] hitColliders;
    EnemyHealthManager healthManager;
    HealthController healthController;




    public void Explode()
    {
        Debug.Log("Boom_0");
        //if (damage <= 0) return;
        explosionSound.Play();
        Vector2 explosionPos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(explosionEffect, explosionPos, transform.rotation);
        hitColliders = Physics2D.OverlapCircleAll(explosionPos, range);
        Debug.Log("Boom_1");
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Enemy") && canDamageEnemy)
            {
                healthManager = collider.GetComponent<EnemyHealthManager>();
                healthManager.Damage(damage);
            }
            if (collider.gameObject.CompareTag("Player") && canDamagePlayer)
            {
                healthController = collider.gameObject.GetComponentInParent<HealthController>();
                healthController.Damage(damage);
            }
        }
        Destroy(gameObject);
        Debug.Log("Boom_2");
    }
}
