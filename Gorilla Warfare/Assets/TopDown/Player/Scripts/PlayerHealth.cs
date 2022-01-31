using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth;
        public float currentHealth;

        Player player;

        private void Start()
        {
            currentHealth = maxHealth;
            player = GetComponent<Player>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                player.controller.TakeDamage(10);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                player.controller.TakeDamage(10);
            }
        }

        public void ApplyDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
                player.controller.Die();

        }
    }
}