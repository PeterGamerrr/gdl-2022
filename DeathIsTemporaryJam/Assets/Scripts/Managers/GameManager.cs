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
            if (_instance is null) Debug.LogError("Gamemanager is null");
            return _instance;
        }
    }
    public GameState GameState = GameState.MAINMENU;

    private SceneManager _sceneManager = new();

    [SerializeField] HealthController HealthController;
    [SerializeField] LivesController LivesController;
    [SerializeField] WaveSpawner WaveSpawner;

    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject UpgradeMenu;
    [SerializeField] GameObject GameNumbers;
    [SerializeField] GameObject PauseMenu;


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

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (GameState == GameState.PLAYING)
            {
                Pause();
            }
            if (GameState == GameState.PAUSE)
            {
                UnPause();
            }
        }
    }

    public void TogglePause()
    {
        if (GameState == GameState.PLAYING)
        {
            Pause();
        }
        if (GameState == GameState.PAUSE)
        {
            UnPause();
        }
    }
    public void UnPause()
    {
        PauseMenu.SetActive(false);
        HealthBar.SetActive(true);
        GameNumbers.SetActive(true);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        HealthBar.SetActive(false);
        GameNumbers.SetActive(false);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        StartMenu.SetActive(false);
        HealthBar.SetActive(true);
        GameNumbers.SetActive(true);
        WaveSpawner.StartWaves();
        GameState = GameState.PLAYING;
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
        GameState = GameState.UPGRADE;
    }

    public void ContinueAfterDeath()
    {
        UpgradeMenu.SetActive(false);
        HealthBar.SetActive(true);
        GameNumbers.SetActive(true);
        HealthController.ResetHealth();
        WaveSpawner.StartWaves();
        Time.timeScale = 1;
        GameState = GameState.PLAYING;

    }

    public void OnGameOver()
    {
        UpgradeMenu.SetActive(false);
        GameNumbers.SetActive(false);
        GameOverMenu.SetActive(true);
        HealthBar.SetActive(false);
        GameState = GameState.GAMEOVER;

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
