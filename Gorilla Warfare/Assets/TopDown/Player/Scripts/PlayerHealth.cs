using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth;
        public float currentHealth;

        PlayerController controller;

        private void Start()
        {
            currentHealth = maxHealth;
            controller = GetComponent<PlayerController>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Destroy(other.gameObject);
                currentHealth--;
                if (currentHealth <= 0)
                    controller.Die();

            }
        }
    }
}
