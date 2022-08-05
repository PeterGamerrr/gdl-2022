using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public int MaxHealth = 100;
    public int Health = 100;

    public void Start()
    {
        StatManager.Instance.UpGradeEvent.AddListener(UpgradeEvent);
    }
    void Update()
    {
        
    }

    private void UpgradeEvent()
    {
        MaxHealth = StatManager.Instance.GetHealth();
        Health = MaxHealth;
    }
}
