using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivesController : MonoBehaviour
{
    public UnityEvent GameOverEvent = new();

    public int lives = 5;

    private void Start()
    {
        gameObject.GetComponent<HealthController>().DeathEvent.AddListener(DeathEventListener);
    }

    private void DeathEventListener()
    {
        lives--;
        if (lives <= 0) GameOver();
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        GameOverEvent.Invoke();
    }
}
