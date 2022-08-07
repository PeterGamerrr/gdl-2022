using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] float range;
    [SerializeField] public int damage;

    [SerializeField] bool canDamagePlayer;
    [SerializeField] bool canDamageEnemy;


    private Collider2D[] hitColliders;
    EnemyHealthManager healthManager;
    HealthController healthController;




    public void Explode()
    {
        if (damage <= 0) return;
        Debug.LogWarning("Boom");
        hitColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), range);
        Debug.LogWarning("Boom, but after the colliders");
        Debug.LogWarning(hitColliders.Length);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Enemy") && canDamageEnemy)
            {
                healthManager = collider.GetComponent<EnemyHealthManager>();
                healthManager.Damage(damage);
                Debug.LogWarning("EnemyExplode");
            }
            if (collider.gameObject.CompareTag("Player") && canDamagePlayer)
            {
                healthController = collider.gameObject.GetComponentInParent<HealthController>();
                healthController.Damage(damage);
                Debug.LogWarning("PlayerExplode");
            }
            Debug.LogWarning(collider.gameObject.tag);
        }
 
    }
}
