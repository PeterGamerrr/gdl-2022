using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{

    [Header("Config Values")]
    [SerializeField] float attackRange;
    [SerializeField] int attackCooldown;
    [SerializeField] int attackDamage;
    [SerializeField] HealthController healthController;

    AIDestinationSetter destinationSetter;
    Transform player;
    EnemyShooting shooting;

    private bool isAttacking = false;

    private void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        destinationSetter.target = player;

        shooting = GetComponent<EnemyShooting>();

        shooting.target = player;
    }


    void Update()
    {
        CheckAttackDistance();
    }


    float DistanceToPlayer()
    {
        Vector3 fixedPosition = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 distance = fixedPosition - player.transform.position;
        Debug.Log(distance.magnitude);
        return distance.magnitude;
    }

    void CheckAttackDistance()
    {
        if (DistanceToPlayer() <= attackRange && !isAttacking)
        {
            Debug.LogWarning("Within Attacking Range");
            StartCoroutine(AttackPlayer());
            isAttacking = true;
        }

        else if (DistanceToPlayer() >= attackRange)
        {
            isAttacking = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        Debug.LogWarning("Started Player Attack");
        shooting.Shoot();
        yield return new WaitForSeconds(attackCooldown);
        if (DistanceToPlayer() <= attackRange)
        {
            StartCoroutine(AttackPlayer());
        }
    }
}
