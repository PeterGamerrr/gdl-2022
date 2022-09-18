using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public UnityEvent DeathEvent = new();

    [SerializeField] int MaxHealth = 100;
    [SerializeField] int Health = 100;

    [SerializeField] GameObject HealthBar;
    [SerializeField] AudioSource hurtSound;

    public void Start()
    {
        StatManager.Instance.UpGradeEvent.AddListener(UpgradeEvent); 
    }

    private void UpgradeEvent()
    {
        MaxHealth = StatManager.Instance.GetHealth();
    }

    public void Damage(int amount)
    {
        hurtSound.Play();
        Health -= amount;
        if (Health <= 0) Die();
    }
    
    public void Heal(int amount)
    {
        //TODO: ADD HEAL SOUND
        hurtSound.Play();
        
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void ResetHealth()
    {
        Health = MaxHealth;
    }

    private void Die()
    {
        DeathEvent.Invoke();
        ResetHealth();
    }

    private void UpdateHealthBar()
    {
        var rectTransform = (RectTransform) HealthBar.transform;
        var parentRectTransform = (RectTransform) rectTransform.parent.transform;

        rectTransform.sizeDelta = new Vector2(parentRectTransform.rect.width * Health / MaxHealth,parentRectTransform.rect.height);
    }

    private void Update()
    {
        UpdateHealthBar();
    }

}
