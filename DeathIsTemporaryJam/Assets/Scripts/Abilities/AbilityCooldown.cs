using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class AbilityCooldown : MonoBehaviour
{

    public Image darkMask;
    public TextMeshProUGUI coolDownTextDisplay;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject player;
    private Image buttonImage;
    private AudioSource abilityAudio;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;
    private bool coolDownComplete;

    void Start()
    {
        Initialize(ability, player);
    }

    public void Initialize(Ability selectedAbility, GameObject player)
    {
        ability = selectedAbility;
        buttonImage = GetComponent<Image>();
        abilityAudio = GetComponent<AudioSource>();
        buttonImage.sprite = ability.aSprite;
        darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.aBaseCoolDown;
        ability.Initialize(player);
        AbilityReady();
    }


    private void Update()
    {
        coolDownComplete = (Time.time > nextReadyTime);
        if (!coolDownComplete)
        {
            CoolDown();
        }
    }



    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }


    public void OnButtonPushed()
    {
        if (coolDownComplete)
        {
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            darkMask.enabled = true;

            abilityAudio.clip = ability.aSound;
            abilityAudio.Play();
            ability.TriggerAbility();
        }
    }


}
