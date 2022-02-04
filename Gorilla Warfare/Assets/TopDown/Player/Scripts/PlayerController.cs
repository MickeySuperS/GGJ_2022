using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public abstract class PlayerController : MonoBehaviour, IHitable
    {
        //Movment
        Rigidbody rb;
        public Vector3 rbVelocity => rb.velocity;

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


        public bool isDead = false;

        //Audio
        public AudioClip attackAudio;
        public AudioSource source;

        public PlayerAnimation playerAnimatoin;

        public bool canMoveWhileAttacking = true;

        public ParticleSystem ps;


        private void Update()
        {
            playerAnimatoin.Animate(moveDirection, lookAtPoint);
        }

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            source = GetComponent<AudioSource>();
            health = GetComponentInChildren<PlayerHealth>();
            isDead = false;
        }

        protected virtual void FixedUpdate()
        {
            if (isDead) return;
            if (isAttacking && !canMoveWhileAttacking)
            {
                rb.velocity = Vector3.zero;
                return;
            }


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


        protected PlayerHealth health;
        public virtual void TakeDamage(int health)
        {
            // playerAnimatoin.AnimateTakeDamage();
        }

        public virtual void Die()
        {

            isDead = true;
            rb.isKinematic = true;
            moveDirection = Vector3.zero;
            rb.velocity = Vector3.zero;
            playerAnimatoin.Die();
            WinLoseScreen.instace.SetWin(false);
            WinLoseScreen.instace.EndGame();
        }

        public void EndPS()
        {
            if (ps)
                ps.Stop();
        }

        public bool isAttacking = false;

        public void PlayAttackAnim()
        {
            playerAnimatoin.PlayAttackAnim();
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
            if (ps)
                ps.Play();
            yield return new WaitForSeconds(dashTime);

            isDashing = false;
            if (ps)
                ps.Stop();

        }


    }
}