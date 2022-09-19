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
    [SerializeField] GameObject abilitySwitchMenu;

    public RewardChest activeChest;


    public void CheckWave(int wave)
    {
        if (wave % waveInterval == 0)
        {
            SpawnCrate();
        }
    }

    void SpawnCrate()
    {
        activeChest = Instantiate(rewardCrate, spawnPosition.position, spawnPosition.rotation, spawnPosition).GetComponent<RewardChest>();
        activeChest.abilitySwitchMenu = abilitySwitchMenu;
    }

    public void DestroyChest()
    {
        Debug.Log("destroying chest_2");
        activeChest.DestroyChest();
    }

}
