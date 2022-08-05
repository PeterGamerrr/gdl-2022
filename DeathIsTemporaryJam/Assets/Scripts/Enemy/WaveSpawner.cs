using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] enemyTypes;
    [SerializeField] GameObject player;
    [SerializeField] Transform enemyParent;

    [SerializeField] float spawnBuffer;

    Camera cam;

    private Vector3 spawnPosition;
    private float spawnX;
    private float spawnY;
    private float outerSpawn;

    private int axisRandomiser;
    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetSpawnPositions();
        }
    }


    void GetSpawnPositions()
    {
        outerSpawn = Random.Range(-1, 1);
        if (outerSpawn >= 0)
        {
            outerSpawn += 1.1f;
        }

        axisRandomiser = Random.Range(0, 2);
        if (axisRandomiser == 0)
        {
            spawnX = outerSpawn;
            spawnY = Random.Range(-1, 3);
        }
        else
        {
            spawnY = outerSpawn;
            spawnX = Random.Range(-1, 3);
        }

        spawnPosition = cam.ViewportToWorldPoint(new Vector3(spawnX, spawnY, 1));


        Instantiate(enemyTypes[0], spawnPosition, transform.rotation, enemyParent);
    }

}
