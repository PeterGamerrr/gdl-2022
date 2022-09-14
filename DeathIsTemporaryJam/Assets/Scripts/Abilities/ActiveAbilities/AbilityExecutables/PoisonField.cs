using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonField : Field
{
    public int damage;
    public int poisonCooldown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealthManager = collision.GetComponent<EnemyHealthManager>();
            enemyHealthManager.isPoisoned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealthManager enemyHealthManager = collision.GetComponent<EnemyHealthManager>();
            if (enemyHealthManager.isPoisoned)
            {
                enemyHealthManager.StartCoroutine(enemyHealthManager.PoisonDamage(damage, poisonCooldown));
            }
        }
    }
}
