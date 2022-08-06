using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [Header("Config Values")]
    [SerializeField] float minAttackDistance;
    [SerializeField] float maxAttackDistance;
    [SerializeField] int attackCooldown;
    [SerializeField] int attackDamage;
    [SerializeField] HealthController healthController;

    AIDestinationSetter destinationSetter;
    Transform player;


    private bool isAttacking = false;

    private void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        destinationSetter.target = player;
    }


    void Update()
    {
        CheckAttackDistance();
    }


    float DistanceToPlayer()
    {
        Vector3 distance = transform.position - player.transform.position;
        return distance.magnitude;
    }

    void CheckAttackDistance()
    {
        if (DistanceToPlayer() <= minAttackDistance && !isAttacking)
        {
            destinationSetter.target = transform;
            StartCoroutine(AttackPlayer());
            isAttacking = true;
        }

        else if (DistanceToPlayer() >= maxAttackDistance)
        {
            destinationSetter.target = player;
            isAttacking = false;
        }
    }

    IEnumerator AttackPlayer()
    {

        healthController.Damage(attackDamage);
        yield return new WaitForSeconds(attackCooldown);
        if (DistanceToPlayer() < maxAttackDistance)
        {
            StartCoroutine(AttackPlayer());
        }
    }

}
