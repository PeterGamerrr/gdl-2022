using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public UnityEvent DeathEvent = new();

    public int MaxHealth = 100;
    public int Health = 100;

    public GameObject HealthBar;

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
        Health -= amount;
        if (Health < 0) Die();
    }

    public void ResetHealth()
    {
        Health = MaxHealth;
    }

    private void Die()
    {
        Debug.Log("Die");
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
