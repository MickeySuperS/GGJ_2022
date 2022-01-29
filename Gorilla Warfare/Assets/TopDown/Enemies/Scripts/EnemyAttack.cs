using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown {
    public class EnemyAttack : EnemyFollow
    {
        bool isAttacking;
        Vector3 dashDirection;
        public float dashSpeed;
        float currentDashSpeed;
        public float distanceToAttack;
        public float timeBeforeDash;
        public float dashTime;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            isAttacking = false;
            
        }

        // Update is called once per frame

        protected override void Update()
        {
            if (isAttacking)
            {
                return;
            }

            base.Update();
            float distance = (target.transform.position - transform.position).magnitude;

            if (distance < distanceToAttack)
                StartCoroutine(AttackCORO());
        }
        protected override void FixedUpdate()
        {
            if(!isAttacking)
                base.FixedUpdate();

            else
            {
                rb.velocity = dashDirection * currentDashSpeed;
            }
        }
        IEnumerator AttackCORO()
        {
            isAttacking = true;
            currentDashSpeed = 0;
            dashDirection = (target.transform.position - transform.position).normalized;

            yield return new WaitForSeconds(timeBeforeDash);
            currentDashSpeed = dashSpeed;


            yield return new WaitForSeconds(dashTime);
            isAttacking = false;

        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            isAttacking = false;

        }
    }
}