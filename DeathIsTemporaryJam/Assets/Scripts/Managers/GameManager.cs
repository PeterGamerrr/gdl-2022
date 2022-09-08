using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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
    
    public GameState GameState = GameState.MAINMENU;
    public List<GameObject> Bullets = new List<GameObject>();

    [SerializeField] HealthController HealthController;
    [SerializeField] LivesController LivesController;
    [SerializeField] WaveSpawner WaveSpawner;

    [Header("UI")]
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject StartMenu;
    [SerializeField] GameObject GameOverMenu;
    [SerializeField] GameObject UpgradeMenu;
    [SerializeField] GameObject GameNumbers;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject SettingsMenu;

    private DateTime _pauseCooldownTimeStamp;
    private const float _pauseCooldownTime = 1;
    
    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {
        HealthController.DeathEvent.AddListener(OnDeath);
        LivesController.GameOverEvent.AddListener(OnGameOver);
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (_pauseCooldownTimeStamp >= DateTime.Now) return;
        _pauseCooldownTimeStamp = DateTime.Now.AddSeconds(_pauseCooldownTime);
        
        if (GameState == GameState.PLAYING)
        {
            Pause();
        }
        else if (GameState == GameState.PAUSE)
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
        GameState = GameState.PLAYING;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        HealthBar.SetActive(false);
        GameNumbers.SetActive(false);
        Time.timeScale = 0;
        GameState = GameState.PAUSE;
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

    public void ShowSettings()
    {
        SettingsMenu.SetActive(true);
    }

    public void HideSettings()
    {
        SettingsMenu.SetActive(false);
    }

    private void RemoveBullets()
    {
        foreach (GameObject bullet in Bullets)
        {
            Destroy(bullet);
        }
    }
}
