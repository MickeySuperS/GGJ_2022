using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class SpawnEnemyController : MonoBehaviour
    {

        public GameObject spawnCirclePrefab;
        public GameObject enemyToSpawnPrefab;


        public float spawnCircleRadius = 1;
        public int numberOfEnemiesToSpawn = 2;
        public int randomSpawnFactor = 1;

        public float waitTimeAfterCirlceSpawn = 2;
        public float randomWaitFactor = 0;

        List<GameObject> enemiesSpawned = new List<GameObject>();


        Boss boss;

        private void Start()
        {
            boss = GetComponentInParent<Boss>();
        }


        [ContextMenu("Spawn Enemies")]
        public void SpawnEnemies()
        {
            StartCoroutine(SpawnEnemyCoro());
        }

        IEnumerator SpawnEnemyCoro()
        {
            yield return null;
            Vector3 randomPoint = boss.GetRandomPoint();
            var circle = Instantiate(spawnCirclePrefab, randomPoint, Quaternion.identity);
            circle.transform.localScale = Vector3.one * spawnCircleRadius;

            yield return new WaitForSeconds(waitTimeAfterCirlceSpawn + Random.Range(0, randomWaitFactor));

            Destroy(circle);
            int enemyToSpawnCount = Random.Range(numberOfEnemiesToSpawn, numberOfEnemiesToSpawn + randomSpawnFactor);
            for (int i = 0; i < enemyToSpawnCount; i++)
            {
                Vector2 randomCirlce = Random.insideUnitCircle * spawnCircleRadius;
                Vector3 spawnPoint = randomPoint + new Vector3(randomCirlce.x, 0f, randomCirlce.y);
                var enemy = Instantiate(enemyToSpawnPrefab, spawnPoint, Quaternion.identity);
                enemiesSpawned.Add(enemy);
            }

        }

        public void KillAllEnemies()
        {
            for (int i = enemiesSpawned.Count - 1; i > 0; i--)
            {
                if (!enemiesSpawned[i])
                {
                    enemiesSpawned.RemoveAt(i);
                    continue;
                }

                enemiesSpawned[i].GetComponent<Enemy>()?.Die();
                enemiesSpawned.RemoveAt(i);
            }
        }
    }
}