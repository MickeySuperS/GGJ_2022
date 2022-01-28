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

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        public override void LookAt(Vector3 lookAtPoint)
        {
            base.Start();
        }

        public void Attack()
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("hit enemy");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        public override void TakeDamage(ref float health)
        {
            
        }
    }
}