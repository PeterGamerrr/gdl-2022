using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthManager : MonoBehaviour
{
    public UnityEvent<GameObject> EnemyDeathEvent = new();

    [SerializeField] int health;
    [SerializeField] float damageColorTime;

    [HideInInspector] public bool isPoisoned;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }



    public void Damage(int amount)
    {
        health -= amount;
        StartCoroutine(DamageColor());
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


    IEnumerator DamageColor()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageColorTime);
        spriteRenderer.color = Color.white;
    }

}
