using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class EnemyFollow : Enemy
    {
        public float enemySpeed;

        float enemyDamagedSpeed = -30f;
        [SerializeField] float currentSpeed;
        Rigidbody rb;

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
            currentSpeed = enemySpeed;
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            currentSpeed = enemyDamagedSpeed;
        }

        protected virtual void FixedUpdate()
        {
            if (currentSpeed != enemySpeed)
                currentSpeed = Mathf.Lerp(currentSpeed, enemySpeed, 4f * Time.fixedDeltaTime);
            rb.velocity = (target.transform.position - transform.position).normalized * currentSpeed;
            if (rb.velocity != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.4f);
            }
        }
    }
}
