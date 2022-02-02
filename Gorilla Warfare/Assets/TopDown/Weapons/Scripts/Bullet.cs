using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed = 10f;
        Transform childComp;
        float defaultScale = 1;
        Rigidbody rb;
        float localTimer = 0;
        public GameObject particlePrefab;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * bulletSpeed;
            childComp = transform.GetChild(0);
            defaultScale = childComp.localScale.x;
        }

        private void OnTriggerEnter(Collider other)
        {

            var hitable = other.gameObject.GetComponentInParent<IHitable>();
            if (hitable != null)
            {
                hitable.TakeDamage(10);
            }
            Instantiate(particlePrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        private void Update()
        {
            localTimer += Time.deltaTime * bulletSpeed;
            Vector3 scale = childComp.transform.localScale;
            scale.y = defaultScale * Mathf.Sin(localTimer);
            childComp.transform.localScale = scale;
        }
    }
}
