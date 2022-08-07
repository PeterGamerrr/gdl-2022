using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{

    [Header("Config Values")]
    [SerializeField] float attackRange;
    [SerializeField] float attackFuse;
    [SerializeField] HealthController healthController;

    AIDestinationSetter destinationSetter;
    Transform player;
    Explosion explosion;

    private bool isAttacking = false;

    private void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        destinationSetter.target = player;

        explosion = GetComponent<Explosion>();
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
        yield return new WaitForSeconds(attackFuse);
        if (DistanceToPlayer() <= attackRange)
        {
            explosion.Explode();
            Destroy(gameObject);
        }
    }
}
