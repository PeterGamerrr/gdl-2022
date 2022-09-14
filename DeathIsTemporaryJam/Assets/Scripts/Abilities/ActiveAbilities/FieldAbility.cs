using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FieldAbility")]
public class FieldAbility : Ability
{
    [InlineEditor]
    public Field field;

    private GameObject player;

    public override void Initialize(GameObject obj)
    {
        player = obj;
    }

    public override void TriggerAbility()
    {
        Instantiate(field.gameObject, player.transform.position, player.transform.rotation);
    }
}
