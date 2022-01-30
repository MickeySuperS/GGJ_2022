using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{





    public class PlayerAnimation : MonoBehaviour
    {

        public Animator anim;
        SpriteRenderer rend;
        [SerializeField] PlayerController pController;

        private void Start()
        {
            anim = GetComponent<Animator>();
            rend = GetComponent<SpriteRenderer>();
            rend.receiveShadows = true;
        }

        public void Animate(Vector3 moveDirection, Vector3 lookAtPoint)
        {
            if (pController.isAttacking) return;

            var diff = transform.position - lookAtPoint;

            rend.flipX = diff.x < 0;

            if (diff.z > 0)
            {
                if (moveDirection != Vector3.zero)
                    anim.Play("WalkFacingForward");
                else
                    anim.Play("Idle");
            }
            else
            {
                if (moveDirection != Vector3.zero)
                    anim.Play("WalkFacingBackward");
                else
                    anim.Play("Idle_Back");
            }

            // Idle
            // WalkFacingForward
            // Idle_Back
            // WalkFacingBackward

        }

        public void PlayAttackAnim()
        {
            pController.isAttacking = true;
            anim.Play("Attack");
        }

        public void Attack()
        {
            if (!pController.enabled) return;
            pController.Attack();
        }

        public void EndAttack()
        {
            pController.isAttacking = false;
        }

        public void Die()
        {
            anim.CrossFade("Die", 0.1f);
        }
    }
}