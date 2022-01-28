using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<Enemy> enemiesPrefab;

        public List<Transform> spawnPoints;

        public float spawnCoolDown;

        float lastTimeSpawned;
        public int maxEnemiesToSpawn;
        int totalSpawnCount;

        private void Start()
        {
            lastTimeSpawned = -spawnCoolDown;
            totalSpawnCount = 0;
        }

        private void Update()
        {
            if (totalSpawnCount >= maxEnemiesToSpawn) return;
            if (Time.time < lastTimeSpawned + spawnCoolDown) return;
            lastTimeSpawned = Time.time;

            var enemy = Instantiate(enemiesPrefab[Random.Range(0, enemiesPrefab.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            totalSpawnCount++;

        }
    }
}
