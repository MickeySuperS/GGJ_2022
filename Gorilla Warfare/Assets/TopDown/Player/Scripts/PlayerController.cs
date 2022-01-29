using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public abstract class PlayerController : MonoBehaviour
    {
        //Movment
        Rigidbody rb;
        [Range(5, 30)]

        public float playerSpeed;

        Vector3 moveDirection;
        Vector3 lookAtPoint;

        //Dash
        bool isDashing = false;
        Vector3 dashDirection;
        public float dashSpeed, dashTime;

        //Attack
        public float attackCD;
        public float knockBackVal;


        //Animations
        public Animator anim;
        public bool isDead = false;

        //Audio
        public AudioClip attackAudio;
        protected AudioSource source;



        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            source = GetComponent<AudioSource>();
            isDead = false;
        }

        protected virtual void FixedUpdate()
        {
            if (isDead) return;

            if (isDashing)
            {
                rb.velocity = dashDirection * dashSpeed;
            }
            else
            {
                rb.velocity = moveDirection * playerSpeed;

                // if (moveDirection != Vector3.zero)
                // {
                //     var targetRotation = Quaternion.LookRotation(lookAtPoint - transform.position);
                //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.4f);
                // }
            }
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            this.moveDirection = moveDirection;
        }

        public virtual void LookAt(Vector3 lookAtPoint)
        {
            this.lookAtPoint = new Vector3(lookAtPoint.x, transform.position.y, lookAtPoint.z);
        }


        public abstract void TakeDamage(ref float health);

        public virtual void Die()
        {
            isDead = true;
            rb.isKinematic = true;
            moveDirection = Vector3.zero;
            rb.velocity = Vector3.zero;
            anim.CrossFade("Die", 0.1f);

        }

        public abstract void Attack();

        public void Dash()
        {
            StartCoroutine(DashCORO());
        }

        IEnumerator DashCORO()
        {
            isDashing = true;
            dashDirection = moveDirection == Vector3.zero ? (lookAtPoint - transform.position).normalized : moveDirection;
            yield return new WaitForSeconds(dashTime);

            isDashing = false;

        }


    }
}