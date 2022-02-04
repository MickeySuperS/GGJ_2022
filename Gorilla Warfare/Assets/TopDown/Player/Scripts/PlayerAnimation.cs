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

        //public SpriteRenderer[] ghostEffect;

        private void Start()
        {
            anim = GetComponent<Animator>();
            rend = GetComponent<SpriteRenderer>();
            rend.receiveShadows = true;
        }

        // public void AnimateGhost()
        // {
        //     for (int i = 0; i < ghostEffect.Length; i++)
        //     {
        //         ghostEffect[i].gameObject.SetActive(true);
        //         var localPos = -pController.rbVelocity * i * 0.05f;
        //         ghostEffect[i].transform.position = transform.position + localPos;
        //     }
        // }

        // public void EndAnimateGhost()
        // {
        //     for (int i = 0; i < ghostEffect.Length; i++)
        //     {
        //         ghostEffect[i].gameObject.SetActive(false);
        //     }
        // }

        public HitFeedback hitFeedback;

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
            gameObject.SetActive(false);
            anim.CrossFade("Die", 0.1f);
        }
    }
}