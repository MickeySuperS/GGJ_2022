using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{
    public class PlayerHealth : MonoBehaviour
    {

        public float maxHealth;
        public float currentHealth;

        Player player;

        public RectTransform UIRect;

        public void SetMaxHealth(float newMaxHealth)
        {
            var sizeDelta = UIRect.sizeDelta;
            sizeDelta.x = newMaxHealth / 10;
            UIRect.sizeDelta = sizeDelta;

            float val = Mathf.InverseLerp(0, maxHealth, currentHealth);
            currentHealth = Mathf.Lerp(0, newMaxHealth, val);
            maxHealth = newMaxHealth;
        }

        private void Start()
        {
            currentHealth = maxHealth;
            SetMaxHealth(maxHealth);
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