using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Piercing;
    public int Damage;

    EnemyHealthManager enemyHealthManager;

    
    void Update()
    {
        
    }

    void CheckPiercing()
    {
        if (Piercing > 0)
        {
            Piercing--;
        } else
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHealthManager = collision.GetComponent<EnemyHealthManager>();

            enemyHealthManager.Damage(Damage);
            //damage
            Debug.Log("Damaged Enemy");

        }
    }
}
