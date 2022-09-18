using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewardChest : MonoBehaviour
{
    [SerializeField] GameObject abilitySwitchMenu;
    [SerializeField] Ability[] abilities;

    private AbilitySwitcher abilitySwitcher;
    private bool isOpen;
    private Animator animator;
    private Ability reward;


    private void Start()
    {
        abilitySwitcher = abilitySwitchMenu.GetComponent<AbilitySwitcher>();
        animator = GetComponent<Animator>();
    }



    void OpenChest()
    {
        Time.timeScale = 0;
        abilitySwitchMenu.SetActive(true);
        abilitySwitcher.newAbility = RandomAbility();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {                                             
            animator.SetBool("isOpen", true);         
            OpenChest();                              
        }                                             
    }                         
    
    Ability RandomAbility()
    {
        reward = abilities[Random.Range(0, abilities.Length)];
        foreach (AbilityCooldown slot in abilitySwitcher.activeAbilies)
        {
            if (slot.ability == reward)
            {
                reward = RandomAbility();
            }
        }
        return reward;
        
    }
}                                                     
                                                      