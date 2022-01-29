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

            var hitable = other.gameObject.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.TakeDamage(10);
            }
            Destroy(this.gameObject);
        }
    }
}
