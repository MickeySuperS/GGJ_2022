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
            base.LookAt(lookAtPoint);
        }

        public override void Attack()
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider enemycoll in hitEnemies)
            {
                var iHitable = enemycoll.GetComponent<IHitable>();
                if (iHitable != null)
                {
                    iHitable.TakeDamage(25);

                    var enemy = enemycoll.GetComponent<IHitable>();
                    if (enemy is EnemyFollow)
                        (enemy as EnemyFollow).enemyDamagedSpeed = knockBackVal;
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