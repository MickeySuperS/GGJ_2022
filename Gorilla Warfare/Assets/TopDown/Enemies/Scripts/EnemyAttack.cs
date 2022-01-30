using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
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

        public AudioSource source;
        public AudioClip monkeySound;
        protected override void Start()
        {
            base.Start();
            isAttacking = false;

            if (Random.Range(0, 10) < 3)
            {
                source.pitch = Random.Range(0.5f, 1.3f);
                source.PlayOneShot(monkeySound);
            }


        }

        // Update is called once per frame

        protected override void Update()
        {
            if (WinLoseScreen.instace.gameEnded) return;
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
            if (!isAttacking)
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


            if (Random.Range(0, 10) < 3)
            {
                source.pitch = Random.Range(0.5f, 1.3f);
                source.PlayOneShot(monkeySound);
            }

            yield return new WaitForSeconds(timeBeforeDash);
            if (isAttacking)
                currentDashSpeed = dashSpeed;


            yield return new WaitForSeconds(dashTime);
            isAttacking = false;

        }

        public override void TakeDamage(int damage)
        {
            rb.velocity = Vector3.zero;
            isAttacking = false;
            base.TakeDamage(damage);

        }
    }
}