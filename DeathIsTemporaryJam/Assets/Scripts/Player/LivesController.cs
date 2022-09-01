using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivesController : MonoBehaviour
{
    public UnityEvent GameOverEvent = new();
    [SerializeField] List<GameObject> LivesList = new();

    public int Lives = 5;


    private void Start()
    {
        gameObject.GetComponent<HealthController>().DeathEvent.AddListener(DeathEventListener);
    }

    private void DeathEventListener()
    {
        if (Lives <= 0)
        {
            GameOver();
            return;
        } 
        Lives--;
        if (LivesList != null && LivesList.Count > 0)
        {
            for (int i = 0; i < LivesList.Count; i++)
            {
                LivesList[i].gameObject.SetActive(i <= Lives);
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        GameOverEvent.Invoke();
    }
}
