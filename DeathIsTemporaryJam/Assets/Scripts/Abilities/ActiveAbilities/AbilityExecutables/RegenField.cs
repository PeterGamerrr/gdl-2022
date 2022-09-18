using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenField : Field
{

    public int regenAmount;
    public int regenCooldown;

    GameObject player;
    HealthController playerHealth;
    private bool hasRegen;


    IEnumerator RegenHealth()
    {
        if (hasRegen)
        {
            playerHealth.Heal(regenAmount);
            hasRegen = false;
            yield return new WaitForSeconds(regenCooldown);
            hasRegen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerHealth = player.GetComponentInParent<HealthController>();
            hasRegen = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hasRegen)
        {
            StartCoroutine(RegenHealth());
        }
    }

}
