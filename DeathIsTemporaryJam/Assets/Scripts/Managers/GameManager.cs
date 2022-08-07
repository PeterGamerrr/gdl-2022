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
    [SerializeField] WaveSpawner WaveSpawner;

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
        WaveSpawner.StartWaves();
    }

    public void OnDeath()
    {
        UpgradeMenu.SetActive(true);
        HealthBar.SetActive(false);
        WaveSpawner.ResetWaves();
        StatManager.Instance.GiveUpgradePoint(100);
        Time.timeScale = 0;
    }

    public void ContinueAfterDeath()
    {
        UpgradeMenu.SetActive(false);
        HealthBar.SetActive(true);
        HealthController.ResetHealth();
        WaveSpawner.StartWaves();
        Time.timeScale = 1;

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
