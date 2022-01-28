using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TopDown
{
    public class PlayerUI : MonoBehaviour
    {

        public Slider slider;

        PlayerHealth health;

        private void Start()
        {
            health = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if (slider)
                slider.value = health.currentHealth / health.maxHealth;
        }

    }
}
