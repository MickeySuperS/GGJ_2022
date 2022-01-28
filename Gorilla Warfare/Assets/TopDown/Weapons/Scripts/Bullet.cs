using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed = 10f;
        Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * bulletSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage();
            }
            Destroy(this.gameObject);
        }
    }
}
