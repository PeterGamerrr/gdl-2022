using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    public UnityEvent<GameObject> EnemyDeathEvent = new();

    [SerializeField] int health;

    
    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        EnemyDeathEvent.Invoke(gameObject);
        Destroy(this.gameObject);
    }
}
