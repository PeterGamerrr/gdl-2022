using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] AbilityCooldown[] activeAbilies;
    [SerializeField] Button[] slots;
    [SerializeField] Button newAbilityButton;
    [SerializeField] GameObject player;


    public Ability newAbility; //TODO: change to set automatically after opening chest


    private void Start()
    {
        SetButtonImages();
    }

    void SetButtonImages()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].image.sprite = activeAbilies[i].ability.aSprite;
        }
        newAbilityButton.image.sprite = newAbility.aSprite;

    }

    
    public void SwitchAbility(int abilitySlot)
    {
        switch (abilitySlot)
        {
            case 1:
                activeAbilies[0].Initialize(newAbility, player);
                break;
            case 2:
                activeAbilies[1].Initialize(newAbility, player);
                break;
            case 3:
                activeAbilies[2].Initialize(newAbility, player);
                break;
        }
        SetButtonImages();
        gameObject.SetActive(false);
    }
}
