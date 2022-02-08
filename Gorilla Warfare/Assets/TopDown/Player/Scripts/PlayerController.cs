using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public abstract class PlayerController : MonoBehaviour, IHitable
    {
        //Movment
        protected Rigidbody rb;
        public Vector3 rbVelocity => rb.velocity;

        [Range(5, 30)]
        public float playerSpeed;

        protected Vector3 moveDirection;
        protected Vector3 lookAtPoint;


        //Attack
        public float attackCD;
        public float knockBackVal;

        public int damageValue = 10;
        public float damageFactor = 1f;

        public bool isDead = false;

        //Audio
        public AudioClip attackAudio;
        public AudioSource source;

        public PlayerAnimation playerAnimatoin;

        public bool canMoveWhileAttacking = true;


        public AudioSource walkingSource;


        private void Update()
        {

            playerAnimatoin.Animate(moveDirection, lookAtPoint);
            if (rb.velocity != Vector3.zero)
            {
                if (GamePause.gameIsPaused)
                {
                    if (walkingSource.isPlaying)
                        walkingSource.Pause();
                }
                else if (!walkingSource.isPlaying)
                {
                    walkingSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
                    walkingSource.Play();
                }
            }
            else
            {
                if (walkingSource.isPlaying)
                    walkingSource.Pause();
            }
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
            rb.velocity = moveDirection * playerSpeed;
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

        public virtual void EndPS()
        {

        }

        public bool isAttacking = false;

        public void PlayAttackAnim()
        {
            playerAnimatoin.PlayAttackAnim();
        }

        public abstract void Attack();

        public abstract void OtherAbility();

    }
}