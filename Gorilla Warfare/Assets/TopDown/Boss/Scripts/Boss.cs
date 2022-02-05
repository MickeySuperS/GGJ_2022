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
        public AudioSource bgmSource;

        public AudioClip endStageClip, winClip, loseClip;

        public float timeToSwitchBetweenStages = 3f;
        bool invunrable = false;

        private void Start()
        {
            teleportController = GetComponentInChildren<TeleportController>();
            moveController = GetComponentInChildren<MoveController>();
            shootController = GetComponentInChildren<ShootController>();
            spawnEnemyController = GetComponentInChildren<SpawnEnemyController>();
            target = FindObjectOfType<Player>();
            currentHealth = health;
            bossAnimation = GetComponentInChildren<BossAnimations>();
            playerDied = false;
            StartBossFight();
        }


        bool playerDied = false;
        private void Update()
        {
            healthSlider.value = currentHealth / health;
            if (!playerDied && target.controller.isDead)
            {
                playerDied = true;
                PlayerLost();
            }
        }

        void PlayerLost()
        {
            bgmSource.loop = false;
            bgmSource.clip = loseClip;
            bgmSource.Play();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (invunrable) return;
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
            if (invunrable) return;
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
            bgmSource.loop = false;
            bgmSource.clip = winClip;
            bgmSource.Play();
            source.PlayOneShot(endStageClip);
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
            bgmSource.Play();
            while (currentHealth > 0)
            {
                float percentage = currentHealth / health;
                if (percentage <= currentStage.percentageToMoveNext)
                {
                    if (bossStageManager.currentStageIndex < bossStageManager.stages.Length - 1)
                    {
                        bossStageManager.currentStageIndex++;
                        ApplyStage();
                        StartCoroutine(SwitchBetweenStages());
                    }
                }
                yield return null;
            }
            Die();
        }

        IEnumerator SwitchBetweenStages()
        {
            invunrable = true;
            bgmSource.Stop();
            source.PlayOneShot(endStageClip);
            hitFeedback.rend.color = Color.red;
            shootController.StopShooting();
            moveController.StopMoving();
            yield return new WaitForSeconds(timeToSwitchBetweenStages);
            invunrable = false;
            hitFeedback.rend.color = Color.white;
            bgmSource.Play();
        }


        void ApplyStage()
        {
            //Audio
            bgmSource.clip = currentStage.bgmClip;

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
                        if (!invunrable)
                        {
                            moveController.StartMoving();
                            yield return new WaitForSeconds(Random.Range(0, currentStage.movingTime));
                        }
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
                        if (!invunrable)
                        {
                            shootController.StartShooting();
                            yield return new WaitForSeconds(Random.Range(0, currentStage.movingTime));
                        }
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
                        if (!invunrable)
                        {
                            teleportController.Teleport();
                            yield return new WaitForSeconds(Random.Range(currentStage.teleportTime, currentStage.teleportTime + currentStage.timeAfterTeleport));
                        }
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
                        if (!invunrable)
                        {
                            bossAnimation.Summon();
                            spawnEnemyController.SpawnEnemies();
                            yield return new WaitForSeconds(Random.Range(currentStage.spawnEnemyTime, currentStage.spawnEnemyTime + 3));
                        }
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