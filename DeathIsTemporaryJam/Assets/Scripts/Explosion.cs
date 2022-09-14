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

    private Collider2D[] hitColliders;
    EnemyHealthManager healthManager;
    HealthController healthController;




    public void Explode()
    {
        if (damage <= 0) return;
        explosionSound.Play();
        hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range);

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
 
    }
}
