using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivesController : MonoBehaviour
{
    public UnityEvent GameOverEvent = new();

    
    [Header("hearts")] //Made it 5 separate lists because list of list wouldn't work in unity inspector. 
    [SerializeField] private List<GameObject> hearts1 = new();
    [SerializeField] private List<GameObject> hearts2 = new();
    [SerializeField] private List<GameObject> hearts3 = new();
    [SerializeField] private List<GameObject> hearts4 = new();
    [SerializeField] private List<GameObject> hearts5 = new();

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
        hearts1.ForEach(e => e.SetActive(Lives >= 1));
        hearts2.ForEach(e => e.SetActive(Lives >= 2));
        hearts3.ForEach(e => e.SetActive(Lives >= 3));
        hearts4.ForEach(e => e.SetActive(Lives >= 4));
        hearts5.ForEach(e => e.SetActive(Lives >= 5));
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        GameOverEvent.Invoke();
    }
}
