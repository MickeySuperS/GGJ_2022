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


        public override void LookAt(Vector3 lookAtPoint)
        {
            base.Start();
        }

        public override void Attack()
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider enemycoll in hitEnemies)
            {
                var enemy = enemycoll.GetComponent<Enemy>();
                if (enemy)
                {
                    if (enemy is EnemyFollow)
                        (enemy as EnemyFollow).enemyDamagedSpeed = knockBackVal;
                    enemy.TakeDamage(25);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public override void TakeDamage(ref float health)
        {
            health -= 10;
        }
    }
}