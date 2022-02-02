using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public abstract class Enemy : MonoBehaviour, IHitable
    {
        public int healthPoints;
        protected int currentHp;
        protected Player target;

        protected EnemyAnimation enemyAnimatoin;

        protected virtual void Start()
        {
            target = FindObjectOfType<Player>();
            enemyAnimatoin = GetComponentInChildren<EnemyAnimation>();
            currentHp = healthPoints;
        }

        protected virtual void Update()
        {
            enemyAnimatoin?.Animate(target.transform.position);
        }

        public virtual void TakeDamage(int damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
                Die();
        }

        public virtual void Die()
        {
            Destroy(this.gameObject);
        }


    }
}