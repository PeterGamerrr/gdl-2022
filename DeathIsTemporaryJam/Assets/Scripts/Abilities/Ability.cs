using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [HorizontalGroup("Split", LabelWidth = 60)]
    [VerticalGroup("Split/Left")]
    [HideLabel]
    [PreviewField(50, ObjectFieldAlignment.Left)] public Sprite aSprite;

    [LabelText("Name")]
    [VerticalGroup("Split/Right")]
    public string aName = "New Ability";
    [LabelText("Sound")]
    [VerticalGroup("Split/Right")]
    public AudioClip aSound;
    [LabelText("Cooldown")]
    [VerticalGroup("Split/Right")]
    public float aBaseCoolDown = 1f;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
