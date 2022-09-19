using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/ExplosionAbility")]
public class ExplosionAbility : Ability
{
    GameObject player;
    Shooting shooting;

    [SerializeField] int damage;

    public override void Initialize(GameObject obj)
    {
        player = obj;
        shooting = player.GetComponent<Shooting>();
    }

    public override void TriggerAbility()
    {
        shooting.explosionDamage = damage;
        shooting.hasExplosion = true;
    }

    


}
