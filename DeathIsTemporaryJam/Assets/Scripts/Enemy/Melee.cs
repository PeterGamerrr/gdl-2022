using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [Header("Config Values")]
    [SerializeField] public float attackRange;
    [SerializeField] float attackCooldown;
    [SerializeField] int attackDamage;
    [SerializeField] HealthController healthController;
    [SerializeField] Animator animator;


    AIDestinationSetter destinationSetter;
    Transform player;


    private bool isAttacking = false;

    private void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        healthController = player.GetComponent<HealthController>();
        destinationSetter.target = player;
    }


    void Update()
    {
        CheckAttackDistance();
    }


    float DistanceToPlayer()
    {
        Vector3 fixedPosition = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 distance = fixedPosition - player.transform.position;
        return distance.magnitude;
    }

    void CheckAttackDistance()
    {
        if (DistanceToPlayer() <= attackRange && !isAttacking)
        {
            destinationSetter.target = transform;

            StartCoroutine(AttackPlayer());
            isAttacking = true;
        }

        else if (DistanceToPlayer() >= attackRange)
        {
            destinationSetter.target = player;
            isAttacking = false;
        }
    }

    IEnumerator AttackPlayer()
    {
        Debug.LogWarning("Started Attacking Player");
        //animator.SetBool("IsAttacking", true);
        animator.SetTrigger("AttackTrigger");
        healthController.Damage(attackDamage);
        Debug.LogWarning("Damaged Player");
        yield return new WaitForSeconds(0.2f);
        //animator.SetBool("IsAttacking", false);
        animator.ResetTrigger("AttackTrigger");
        yield return new WaitForSeconds(attackCooldown);


        if (DistanceToPlayer() < attackRange)
        {
            Debug.LogWarning("Restart Cycle");
            StartCoroutine(AttackPlayer());
        }
        Debug.LogWarning("Cycle Finished");
    }

}
