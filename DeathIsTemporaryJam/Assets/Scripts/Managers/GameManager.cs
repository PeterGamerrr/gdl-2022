using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("Gamemanager is null");
            return _instance;
        }
    }

    private SceneManager _sceneManager = new();

    [SerializeField] HealthController HealthController;
    [SerializeField] LivesController LivesController;

    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject UpgradeMenu;


    void Start()
    {
        HealthController.DeathEvent.AddListener(OnDeath);
        LivesController.GameOverEvent.AddListener(OnGameOver);
    }

    public void StartGame()
    {
        StartMenu.SetActive(false);
        HealthBar.SetActive(true);
    }

    public void OnDeath()
    {
        UpgradeMenu.SetActive(true);
        HealthBar.SetActive(false);
        StatManager.Instance.GiveUpgradePoint(100);
    }

    public void ContinueAfterDeath()
    {
        UpgradeMenu.SetActive(false);
        HealthBar.SetActive(true);
        HealthController.ResetHealth();
    }

    public void OnGameOver()
    {
        UpgradeMenu.SetActive(false);
        GameOverMenu.SetActive(true);
        HealthBar.SetActive(false);

    }

    public void ContinueAfterGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
