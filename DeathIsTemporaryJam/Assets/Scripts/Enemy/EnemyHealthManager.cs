using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    public UnityEvent<GameObject> EnemyDeathEvent = new();

    [SerializeField] int health;

    [HideInInspector] public bool isPoisoned;



    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    public IEnumerator PoisonDamage(int damage, int poisonCooldown)
    {
        if (isPoisoned)
        {
            Damage(damage);
            isPoisoned = false;
            yield return new WaitForSeconds(poisonCooldown);
            isPoisoned = true;
        }
    }


    public void Die()
    {
        EnemyDeathEvent.Invoke(gameObject);
        Destroy(this.gameObject);
    }

}
