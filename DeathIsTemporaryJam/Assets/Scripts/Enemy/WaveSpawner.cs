using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] enemyTypes;
    [SerializeField] GameObject player;
    [SerializeField] Transform enemyParent;

    [SerializeField] float spawnBuffer;

    [SerializeField] int[] enemyLevelWaves;
    [SerializeField] int currentEnemyLevel;
    [SerializeField] int startEnemyAmount;
    [SerializeField] float waveMultiplier;
    [SerializeField] int waveCooldownInSeconds;



    Camera cam;

    private Vector3 spawnPosition;
    private float spawnX;
    private float spawnY;
    private float outerSpawn;
    private int axisRandomiser;


    private int currentWave = 0;
    private int currentAmountOfEnemies;
    private float currentWaveMultiplier;
    private List<GameObject> waveEnemies = new List<GameObject>();
    private bool waveEnded = true;


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

        UpdateWaves();


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

        //TODO: check if position is safe

        return spawnPosition;
    }


    void IncreaseWave()
    {
        currentWave++;

        //give score?
    }


    void SpawnEnemies(int amountOfMobs, int enemyLVL)
    {
        int randomMobID;
        Vector3 spawnPos;
        for (int i = 0; i < amountOfMobs; i++)
        {
            randomMobID = Random.Range(0, enemyLVL);
            spawnPos = GetSpawnPosition();
            waveEnemies.Add(Instantiate(enemyTypes[randomMobID], spawnPos, transform.rotation, enemyParent));
        }
    }

    void SpawnWaves(int wave)
    {
        Debug.Log(currentWaveMultiplier);
        currentAmountOfEnemies = (int) (currentAmountOfEnemies  * waveMultiplier);

        SpawnEnemies(currentAmountOfEnemies, currentEnemyLevel);
    }

    IEnumerator SpawnNextWave()
    {

        IncreaseWave();
        Debug.Log("Spawning wave " + currentWave + " in " + waveCooldownInSeconds + " seconds.");
        yield return new WaitForSeconds(waveCooldownInSeconds);
        SpawnWaves(currentWave);
        Debug.Log("Spawned wave " + currentWave);
    }

    void UpdateWaves()
    {
        if (waveEnded)
        {
            waveEnded = false;
            StartCoroutine(SpawnNextWave());
        }
    }

}
