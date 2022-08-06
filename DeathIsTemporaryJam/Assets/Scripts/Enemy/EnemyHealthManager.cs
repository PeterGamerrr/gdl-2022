using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    public UnityEvent EnemyDeathEvent = new();

    [SerializeField] int health;

    
    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        EnemyDeathEvent.Invoke();
        Destroy(this.gameObject);
    }
}
