using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Abilities/FreezeAbility")]
public class FreezeAbility : Ability
{
    [Title("Ability Information")]
    public int freezeTime;
    public float range;

    private Freeze freeze;

    private GameObject player;

    private Collider2D[] hitColliders;

    public override void Initialize(GameObject obj)
    {
        player = obj;
        freeze = obj.GetComponent<Freeze>();
        freeze.freezeTime = freezeTime;
        freeze.range = range;
    }

    public override void TriggerAbility()
    {
        hitColliders = Physics2D.OverlapCircleAll(new Vector2(player.transform.position.x, player.transform.position.y), range);
        freeze.collidersInRange = hitColliders;
        freeze.StartCoroutine(freeze.FreezeEnemies());
    }
}
