using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [CreateAssetMenu(fileName = "Stage", menuName = "TopDown/Stage")]
    public class BossStage : ScriptableObject
    {

        [Range(0, 1)]
        public float percentageToMoveNext;


        [Header("Teleport")]
        public bool TeleportEnabled;
        public float teleportTime;

        public float timeBeforeTeleport, timeAfterTeleport;


        [Header("Movement")]
        public bool movementEnabled;
        public float movingTime;
        public float distanceFromPlayer, moveSpeed;


        [Header("Shooting")]
        public bool shootingEnabled;
        public float shootingTime;
        public float shootingCooldown = 1f;
        public float bulletSpeed = 100f;
        public bool targetPlayer = true;
        [Range(0, 15)] public float trackingSpeed = 0;


        [Header("SpawnEnemies")]
        public bool spawnEnemiesEnabled;
        public float spawnEnemyTime;
        public float spawnCircleRadius = 1;
        public int numberOfEnemiesToSpawn = 2, randomSpawnFactor = 1;
        public float waitTimeAfterCirlceSpawn = 2, randomWaitFactor = 0;
    }
}
