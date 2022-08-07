using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] enemyTypes;
    [SerializeField] GameObject player;
    [SerializeField] Transform enemyParent;

    [SerializeField] float spawnBuffer;

    [SerializeField] List<int> enemyLevelWaves;
    [SerializeField] int currentEnemyLevel;
    [SerializeField] int startEnemyAmount;
    [SerializeField] float waveMultiplier;
    [SerializeField] int waveCooldownInSeconds;
    [SerializeField] int pointChanceOnKill;
    [SerializeField] int amountOfKillPoints;
    [SerializeField] int amountOfWavePoints;

    [SerializeField] TextMeshProUGUI waveVisual;
    [SerializeField] TextMeshProUGUI enemyCountVisual;


    Camera cam;

    private Vector3 spawnPosition;
    private float spawnX;
    private float spawnY;
    private float outerSpawn;
    private int axisRandomiser;


    private int currentWave = 0;
    private int currentAmountOfEnemies;
    private float currentWaveMultiplier;
    public List<GameObject> waveEnemies = new List<GameObject>();


    void Start()
    {
        cam = Camera.main;
        currentAmountOfEnemies = startEnemyAmount;


    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SpawnNextWave());
        }


    }


    public void StartWaves()
    {
        StartCoroutine(SpawnNextWave());
    }

    public void ResetWaves()
    {
        currentWave = 0;
        currentEnemyLevel = 1;
        foreach (GameObject enemy in waveEnemies)
        {
            Destroy(enemy);
        }
        waveEnemies.Clear();
        enemyCountVisual.text = "0";
        waveVisual.text = "0";

    }


    Vector3 GetSpawnPosition()
    {
        outerSpawn = Random.Range(-1f, 1f);
        if (outerSpawn >= 0)
        {
            outerSpawn += 1.1f;
        }

        axisRandomiser = Random.Range(0, 2);
        if (axisRandomiser == 0)
        {
            spawnX = outerSpawn;
            spawnY = Random.Range(-1f, 3f);
        }
        else
        {
            spawnY = outerSpawn;
            spawnX = Random.Range(-1f, 3f);
        }


        spawnPosition = cam.ViewportToWorldPoint(new Vector3(spawnX, spawnY, 1));

        //check if is out of range
        if (spawnPosition.x < -72 || spawnPosition.y < -72 || spawnPosition.x > 72 || spawnPosition.y > 72) return GetSpawnPosition();

        //check if is colliding

        var colliders = Physics2D.OverlapCircleAll(spawnPosition, 2);

        if (colliders.Length > 0) return GetSpawnPosition();


        return spawnPosition;
    }


    void IncreaseWave()
    {
        StatManager.Instance.GiveUpgradePoint(amountOfWavePoints);

        currentWave++;
    }


    void SpawnEnemies(int amountOfMobs, int enemyLVL)
    {
        int randomMobID;
        Vector3 spawnPos;
        GameObject enemy;
        EnemyHealthManager enemyHealth;
        for (int i = 0; i < amountOfMobs; i++)
        {
            randomMobID = Random.Range(0, enemyLVL);
            spawnPos = GetSpawnPosition();
            enemy = Instantiate(enemyTypes[randomMobID], spawnPos, transform.rotation, enemyParent);
            waveEnemies.Add(enemy);
            enemyHealth = enemy.GetComponent<EnemyHealthManager>();
            enemyHealth.EnemyDeathEvent.AddListener(EnemyDeathListener);
        }   
    }

    void SpawnWaves(int wave)
    {
        Debug.Log(currentWaveMultiplier);

        currentAmountOfEnemies = (int) (startEnemyAmount * Mathf.Pow(waveMultiplier,wave) );

        SpawnEnemies(currentAmountOfEnemies, currentEnemyLevel);
    }

    IEnumerator SpawnNextWave()
    {

        IncreaseWave();
        CheckEnemyLVL();
        Debug.Log("Spawning wave " + currentWave + " in " + waveCooldownInSeconds + " seconds.");
        yield return new WaitForSeconds(waveCooldownInSeconds);
        SpawnWaves(currentWave);
        Debug.Log("Spawned wave " + currentWave);
        waveVisual.text = "" + currentWave;
        enemyCountVisual.text = "" + waveEnemies.Count;
    }


    void EnemyDeathListener(GameObject enemy)
    {
        int pointChance = Random.Range(1, 101);
        if (pointChance <= pointChanceOnKill)
        {
            StatManager.Instance.GiveUpgradePoint(amountOfKillPoints);
        }

        if (waveEnemies.Contains(enemy))
        {
            waveEnemies.Remove(enemy);
            Debug.Log("Killed Enemy");
        }

        enemyCountVisual.text = "" + waveEnemies.Count;

        if (waveEnemies.Count <= 0)
        {
            StartCoroutine(SpawnNextWave());
        }


    }





    void CheckEnemyLVL()
    {
        if (enemyLevelWaves.Contains(currentWave))
        {
            currentEnemyLevel++;
        }
    }
    

}
