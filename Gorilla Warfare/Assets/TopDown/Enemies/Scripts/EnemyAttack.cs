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
        Collider colliderr;

        // Start is called before the first frame update

        public AudioSource source;
        public AudioClip monkeySound;



        float canAttackTimer = 0;
        public float attackRecoveryTime = 0.5f;


        protected override void Start()
        {
            base.Start();
            isAttacking = false;

            colliderr = GetComponent<Collider>();

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

            if (canAttackTimer > 0)
            {
                canAttackTimer -= Time.deltaTime;
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
            {
                base.FixedUpdate();
            }
            else
            {
                rb.velocity = dashDirection * currentDashSpeed;
            }
        }
        IEnumerator AttackCORO()
        {
            SetAttackParams();

            if (Random.Range(0, 10) < 2)
            {
                source.pitch = Random.Range(0.5f, 1.3f);
                source.PlayOneShot(monkeySound);
            }

            yield return new WaitForSeconds(timeBeforeDash);
            if (isAttacking)
                currentDashSpeed = dashSpeed;
            else
                EndAttackParams();


            yield return new WaitForSeconds(dashTime);
            isAttacking = false;
            EndAttackParams();
        }

        void SetAttackParams()
        {
            //colliderr.isTrigger = true;
            isAttacking = true;
            currentDashSpeed = 0;
            dashDirection = (target.transform.position - transform.position).normalized;
        }

        void EndAttackParams()
        {
            //colliderr.isTrigger = false;
            canAttackTimer = attackRecoveryTime;
            isAttacking = false;
        }

        public override void TakeDamage(int damage)
        {
            StopAllCoroutines();
            EndAttackParams();
            base.TakeDamage(damage);

        }

        public GameObject deathParticlePrefab;
        public override void Die()
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            base.Die();
        }
    }
}