using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionDestroyAllEnemies : GameCondition 
{
    private EnemySpawner[] enemySpawners;
    private int enemySpawnersCompletedCount;

    private void Start()
    {
        this.enemySpawners = FindObjectsOfType<EnemySpawner>();
        for (int i = 0; i < this.enemySpawners.Length; ++i)
        {
            this.enemySpawners[i].OnAllWavesCompleted += HandleAllWavesCompleted;
        }
    }

    private void HandleAllWavesCompleted()
    {
        ++this.enemySpawnersCompletedCount;
        if (this.enemySpawnersCompletedCount == this.enemySpawners.Length)
        {
            IsFulfilled = true;
            RaiseConditionChangedEvent();
        }
    }
}
