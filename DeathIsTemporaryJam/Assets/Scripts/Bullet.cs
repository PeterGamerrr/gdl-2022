using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Piercing;
    public int Damage;
    public int ExplosionDamage;

    EnemyHealthManager enemyHealthManager;
    HealthController healthController;
    Explosion Explosion;

    [SerializeField] bool canDamagePlayer;
    [SerializeField] bool canDamageEnemy;
    [SerializeField] bool hasExplosion;

    [SerializeField] AudioSource hitSound;
    [SerializeField] List<string> nonHittableTags;

    private void Start()
    {
        if (hasExplosion)
        {
            Explosion = GetComponent<Explosion>();
            Explosion.damage = ExplosionDamage;
        }

    }

    void CheckPiercing()
    {
        if (Piercing > 0)
        {
            Piercing--;
        } else if (Piercing <=0)
        {
            if (hasExplosion)
            {
                Explosion.Explode();
            }
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogWarning(collision.name);
        if (collision.gameObject.CompareTag("Enemy") && canDamageEnemy)
        {
            enemyHealthManager = collision.GetComponent<EnemyHealthManager>();
            enemyHealthManager.Damage(Damage);
        } 
        if (collision.gameObject.CompareTag("Player") && canDamagePlayer)
        {
            healthController = collision.gameObject.GetComponentInParent<HealthController>();
            healthController.Damage(Damage);
            hitSound.Play();
        }
        if (!nonHittableTags.Contains(collision.gameObject.tag))
        {
            hitSound.Play();
            CheckPiercing();
        }
    }
}
