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
    [SerializeField] Animator animator;

    AIDestinationSetter destinationSetter;
    Transform player;
    EnemyShooting shooting;

    private bool isAttacking = false;
    private Vector3 playerPos;
    private Vector3 scaleRight;
    private Vector3 scaleLeft;

    private void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        destinationSetter.target = player;

        shooting = GetComponent<EnemyShooting>();

        shooting.target = player;

        scaleLeft = transform.localScale;
        scaleRight = transform.localScale;
        scaleRight.x *= -1;
    }


    void Update()
    {
        CheckAttackDistance();
        FlipTowardsPlayer();
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
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.2f);
        shooting.Shoot();
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(attackCooldown);
        if (DistanceToPlayer() <= attackRange)
        {
            StartCoroutine(AttackPlayer());
        }
    }


    void FlipTowardsPlayer()
    {
        playerPos = player.transform.position;
        if (IsPlayerRight())
        {
            //newScale = gameObject.transform.localScale;
            gameObject.transform.localScale = scaleRight;
        }
        else if (!IsPlayerRight())
        {
            gameObject.transform.localScale = scaleLeft;
        }
    }

    bool IsPlayerRight()
    {
        if (playerPos.x > transform.position.x)
        {
            return true;
        }
        return false;
    }
}
