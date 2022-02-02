using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{
    public class Boss : MonoBehaviour, IHitable
    {
        [SerializeField] BossStageManager bossStageManager;
        public float health;
        [SerializeField] float currentHealth;

        [HideInInspector] public Player target;

        public Slider healthSlider;

        public Transform point1, point2;

        TeleportController teleportController;
        MoveController moveController;
        ShootController shootController;
        SpawnEnemyController spawnEnemyController;

        BossAnimations bossAnimation;

        public AudioClip gameStartClip;
        public AudioSource source;

        private void Start()
        {
            teleportController = GetComponentInChildren<TeleportController>();
            moveController = GetComponentInChildren<MoveController>();
            shootController = GetComponentInChildren<ShootController>();
            spawnEnemyController = GetComponentInChildren<SpawnEnemyController>();
            target = FindObjectOfType<Player>();
            currentHealth = health;
            bossAnimation = GetComponentInChildren<BossAnimations>();
            StartBossFight();
        }

        private void Update()
        {
            healthSlider.value = currentHealth / health;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Player>())
            {
                teleportController.Teleport();
            }
        }


        [ContextMenu("Start BOSS FIGHT")]
        void StartBossFight()
        {
            bossStageManager.currentStageIndex = 0;
            source.PlayOneShot(gameStartClip);
            StartCoroutine(BossFightCoro());
        }


        public HitFeedback hitFeedback;
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                healthSlider.value = currentHealth / health;
                Die();
                return;
            }
            hitFeedback.AnimateTakeDamage();
            Screenshake.instance.StartShake(0.04f);
        }

        void Die()
        {
            StopAllCoroutines();
            moveController.StopMoving();
            shootController.StopShooting();
            shootController.targetPlayer = false;
            spawnEnemyController.KillAllEnemies();
            Debug.LogError("BOSS DIED, GG");
            Destroy(this.gameObject);
            WinLoseScreen.instace.SetWin(true);
            WinLoseScreen.instace.EndGame();
        }



        BossStage currentStage => bossStageManager.stages[bossStageManager.currentStageIndex];
        IEnumerator BossFightCoro()
        {
            StartCoroutine(MovingCORO());
            StartCoroutine(ShootingCORO());
            StartCoroutine(TeleportCORO());
            StartCoroutine(SpawnCORO());
            ApplyStage();
            while (currentHealth > 0)
            {
                float percentage = currentHealth / health;
                if (percentage <= currentStage.percentageToMoveNext)
                {
                    if (bossStageManager.currentStageIndex < bossStageManager.stages.Length - 1)
                    {
                        bossStageManager.currentStageIndex++;
                        ApplyStage();
                    }
                }
                yield return null;
            }
            Die();
        }


        void ApplyStage()
        {
            //Teleport
            teleportController.timeBeforeTeleport = currentStage.timeBeforeTeleport;
            teleportController.timeAfterTeleport = currentStage.timeAfterTeleport;

            //Movement
            moveController.speed = currentStage.moveSpeed;
            moveController.distanceFromPlayer = currentStage.distanceFromPlayer;

            //Shooting
            shootController.cooldown = currentStage.shootingCooldown;
            shootController.bulletSpeed = currentStage.bulletSpeed;
            shootController.targetPlayer = currentStage.targetPlayer;
            shootController.trackingSpeed = currentStage.trackingSpeed;

            //Spawn
            spawnEnemyController.spawnCircleRadius = currentStage.spawnCircleRadius;
            spawnEnemyController.numberOfEnemiesToSpawn = currentStage.numberOfEnemiesToSpawn;
            spawnEnemyController.randomSpawnFactor = currentStage.randomSpawnFactor;
            spawnEnemyController.waitTimeAfterCirlceSpawn = currentStage.waitTimeAfterCirlceSpawn;
            spawnEnemyController.randomWaitFactor = currentStage.randomWaitFactor;
        }

        IEnumerator MovingCORO()
        {
            while (currentHealth > 0 && !target.controller.isDead)
            {
                if (currentStage.movementEnabled)
                {
                    yield return new WaitForSeconds(Random.Range(0, 2f));
                    if (Random.Range(0, 2) == 1)
                    {
                        moveController.StartMoving();
                        yield return new WaitForSeconds(Random.Range(0, currentStage.movingTime));
                    }
                    moveController.StopMoving();
                }
                yield return null;

            }
        }

        IEnumerator ShootingCORO()
        {
            while (currentHealth > 0 && !target.controller.isDead)
            {
                if (currentStage.shootingEnabled)
                {
                    yield return new WaitForSeconds(Random.Range(0, 2f));
                    if (Random.Range(0, 2) == 1)
                    {
                        shootController.StartShooting();
                        yield return new WaitForSeconds(Random.Range(0, currentStage.movingTime));
                    }
                    shootController.StopShooting();

                }
                yield return null;
            }
        }

        IEnumerator TeleportCORO()
        {
            while (currentHealth > 0 && !target.controller.isDead)
            {
                if (currentStage.TeleportEnabled)
                {
                    yield return new WaitForSeconds(Random.Range(0, 2f));
                    if (Random.Range(0, 2) == 1)
                    {
                        teleportController.Teleport();
                        yield return new WaitForSeconds(Random.Range(currentStage.teleportTime, currentStage.teleportTime + currentStage.timeAfterTeleport));
                    }
                }
                yield return null;
            }
        }

        IEnumerator SpawnCORO()
        {
            while (currentHealth > 0 && !target.controller.isDead)
            {
                if (currentStage.spawnEnemiesEnabled)
                {
                    yield return new WaitForSeconds(Random.Range(0, 2f));
                    if (Random.Range(0, 2) == 1)
                    {
                        bossAnimation.Summon();
                        spawnEnemyController.SpawnEnemies();
                        yield return new WaitForSeconds(Random.Range(currentStage.spawnEnemyTime, currentStage.spawnEnemyTime + 3));
                    }
                }
                yield return null;
            }
        }




        public Vector3 GetRandomPoint()
        {
            return new Vector3(
                UnityEngine.Random.Range(point1.position.x, point2.position.x),
                    0,
                UnityEngine.Random.Range(point1.position.z, point2.position.z));
        }


        private void OnDrawGizmosSelected()
        {
            if (!point1 || !point2) return;
            Gizmos.DrawLine(point1.position,
                            new Vector3(point1.position.x, point1.position.y, point2.position.z));
            Gizmos.DrawLine(new Vector3(point1.position.x, point1.position.y, point2.position.z),
                            point2.position);
            Gizmos.DrawLine(point2.position,
                            new Vector3(point2.position.x, point1.position.y, point1.position.z));
            Gizmos.DrawLine(new Vector3(point2.position.x, point1.position.y, point1.position.z),
                            point1.position);
        }

    }
}