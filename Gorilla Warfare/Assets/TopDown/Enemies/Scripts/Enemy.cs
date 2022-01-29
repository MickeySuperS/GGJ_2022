using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public abstract class Enemy : MonoBehaviour
    {
        public int healthPoints;
        protected Player target;

        protected virtual void Start()
        {
            target = FindObjectOfType<Player>();
            
        }

        protected virtual void Update()
        {

        }

        public virtual void TakeDamage(int damage)
        {
            healthPoints -= damage ;
            if (healthPoints <= 0)
                Die();
        }

        protected virtual void Die()
        {
            Destroy(this.gameObject);
        }

     
    }
}