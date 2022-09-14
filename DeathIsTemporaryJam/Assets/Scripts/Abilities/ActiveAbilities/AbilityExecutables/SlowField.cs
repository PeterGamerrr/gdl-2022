using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowField : Field
{
    public float slowMultiplier;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AIPath enemyMovement = collision.gameObject.GetComponent<AIPath>();
            enemyMovement.maxSpeed *= slowMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AIPath enemyMovement = collision.gameObject.GetComponent<AIPath>();
            enemyMovement.maxSpeed /= slowMultiplier;
        }
    }


}
