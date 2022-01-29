using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class ShootController : MonoBehaviour
    {

        public Rigidbody bulletPrefab;
        public Transform bulletSpawnLocation;
        public float cooldown = 1f;
        public float bulletSpeed = 100f;

        bool isShooting = false;
        float lastShotTime;

        public bool targetPlayer = true;
        [Range(0, 15)] public float trackingSpeed = 0;

        Boss boss;
        private void Start()
        {
            boss = GetComponentInParent<Boss>();
        }

        private void Update()
        {
            if (targetPlayer)
            {
                Vector3 targetDir = boss.target.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, trackingSpeed * Time.deltaTime);
            }

            if (!isShooting) return;

            if (Time.time < lastShotTime + cooldown) return;
            Shoot();
        }

        [ContextMenu("Start Shooting")]
        public void StartShooting()
        {
            isShooting = true;
            lastShotTime = Time.time - cooldown;
        }

        [ContextMenu("Stop Shooting")]
        public void StopShooting()
        {
            isShooting = false;
        }

        [ContextMenu("Shoot")]
        public void Shoot()
        {
            lastShotTime = Time.time;
            var bulletRb = Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
            bulletRb.velocity = transform.forward * bulletSpeed;
            Destroy(bulletRb.gameObject, 5f);
        }

    }
}
