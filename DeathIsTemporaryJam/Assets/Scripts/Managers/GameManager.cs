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
    [SerializeField] GameObject GameNumbers;


    public List<GameObject> Bullets = new List<GameObject>();

    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        HealthController.DeathEvent.AddListener(OnDeath);
        LivesController.GameOverEvent.AddListener(OnGameOver);

    }

    public void StartGame()
    {
        StartMenu.SetActive(false);
        HealthBar.SetActive(true);
        GameNumbers.SetActive(true);
        WaveSpawner.StartWaves();
    }

    public void OnDeath()
    {
        UpgradeMenu.SetActive(true);
        HealthBar.SetActive(false);
        GameNumbers.SetActive(false);
        WaveSpawner.ResetWaves();
        RemoveBullets(); 
        StatManager.Instance.GiveUpgradePoint(0);
        Time.timeScale = 0;
    }

    public void ContinueAfterDeath()
    {
        UpgradeMenu.SetActive(false);
        HealthBar.SetActive(true);
        GameNumbers.SetActive(true);
        HealthController.ResetHealth();
        WaveSpawner.StartWaves();
        Time.timeScale = 1;

    }

    public void OnGameOver()
    {
        UpgradeMenu.SetActive(false);
        GameNumbers.SetActive(false);
        GameOverMenu.SetActive(true);
        HealthBar.SetActive(false);

    }

    public void ContinueAfterGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


    private void RemoveBullets()
    {
        foreach (GameObject bullet in Bullets)
        {
            Destroy(bullet);
        }
    }
}
