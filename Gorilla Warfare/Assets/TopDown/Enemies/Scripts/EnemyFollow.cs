using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class EnemyFollow : Enemy
    {
        public float enemySpeed;

        [HideInInspector] public float enemyDamagedSpeed;
        float defaultEnemyDamageSpeed = -20;
        float currentSpeed;
        protected Rigidbody rb;

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody>();
            currentSpeed = enemySpeed;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            currentSpeed = enemyDamagedSpeed;
            enemyDamagedSpeed = defaultEnemyDamageSpeed;
        }

        protected virtual void FixedUpdate()
        {
            if (currentSpeed != enemySpeed)
                currentSpeed = Mathf.Lerp(currentSpeed, enemySpeed, 4f * Time.fixedDeltaTime);
            rb.velocity = (target.transform.position - transform.position).normalized * currentSpeed;

        }
    }
}
