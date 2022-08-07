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
        Debug.Log("Trigger Enter");
        //Debug.LogWarning(collision.name);
        Debug.Log(collision.gameObject.tag == "Player");
        if (collision.gameObject.CompareTag("Enemy") && canDamageEnemy)
        {
            enemyHealthManager = collision.GetComponent<EnemyHealthManager>();

            enemyHealthManager.Damage(Damage);

            Debug.Log("Damaged Enemy");

        } 
        if (collision.gameObject.CompareTag("Player") && canDamagePlayer)
        {
            Debug.Log("Damaged Player before");
            healthController = collision.gameObject.GetComponentInParent<HealthController>();
            healthController.Damage(Damage);
            Debug.Log("Damaged Player" + Damage);
        }
        hitSound.Play();

        CheckPiercing();


    }
}
