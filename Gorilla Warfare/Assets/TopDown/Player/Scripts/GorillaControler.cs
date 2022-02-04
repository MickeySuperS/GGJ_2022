using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class GorillaControler : PlayerController
    {
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayer;



        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

        }

        public AudioClip gorillaSmash;


        public override void LookAt(Vector3 lookAtPoint)
        {
            base.LookAt(lookAtPoint);
        }

        public ParticleSystem gorillaSmashPS;
        public override void Attack()
        {
            if (gorillaSmashPS)
                gorillaSmashPS.Play();
            source.PlayOneShot(gorillaSmash);
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider enemycoll in hitEnemies)
            {
                var iHitable = enemycoll.GetComponentInParent<IHitable>();
                if (iHitable != null)
                {
                    var enemy = enemycoll.GetComponent<Enemy>();
                    if (enemy is EnemyFollow)
                        (enemy as EnemyFollow).enemyDamagedSpeed = knockBackVal;

                    iHitable.TakeDamage(25);
                }
            }
            Screenshake.instance.StartShake();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public override void TakeDamage(int damageAmount)
        {
            base.TakeDamage(damageAmount);
            health.ApplyDamage(10);
            playerAnimatoin.hitFeedback.AnimateTakeDamage();
        }
    }
}