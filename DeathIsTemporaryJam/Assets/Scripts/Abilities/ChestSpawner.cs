using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int waveInterval;

    [Header("References")]
    [SerializeField] GameObject rewardCrate;
    [SerializeField] Transform spawnPosition;

    private GameObject activeChest;


    public void CheckWave(int wave)
    {
        if (wave % waveInterval == 0)
        {
            SpawnCrate();
        }
    }

    void SpawnCrate()
    {
        activeChest = Instantiate(rewardCrate, spawnPosition.position, spawnPosition.rotation, spawnPosition);
    }

    public void DestroyChest()
    {
        Destroy(activeChest);
    }

}
