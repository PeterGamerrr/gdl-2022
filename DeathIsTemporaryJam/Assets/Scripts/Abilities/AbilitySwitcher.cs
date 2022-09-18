using Assets.Scripts;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySwitcher : MonoBehaviour
{
    [Title("Values")]
    [SerializeField] int sellPointAmount;
    
    [Title("References")]
    [SerializeField] public AbilityCooldown[] activeAbilies;
    [SerializeField] Button[] slots;
    [SerializeField] Button newAbilityButton;
    [SerializeField] GameObject player;
    [SerializeField] ChestSpawner chestSpawner;


    public Ability newAbility;


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
        CloseMenu();
    }

    public void SellAbility()
    {
        StatManager.Instance.GiveUpgradePoint(sellPointAmount);
        CloseMenu();
    }

    void CloseMenu()
    {
        SetButtonImages();
        chestSpawner.DestroyChest();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
