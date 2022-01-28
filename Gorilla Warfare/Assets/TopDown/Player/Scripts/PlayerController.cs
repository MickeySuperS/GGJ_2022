using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class PlayerController : MonoBehaviour
    {
        //Movment
        Rigidbody rb;
        [Range(5, 30)]
        public float playerSpeed;

        Vector3 moveDirection;
        Vector3 lookAtPoint;


        //Animations
        public Animator[] anim;
        public bool isDead = false;

        //Shooting
        Shoot[] shoots;

        //Audio
        AudioSource source;
        public AudioClip shootAudio;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            shoots = GetComponentsInChildren<Shoot>();
            source = GetComponent<AudioSource>();
            isDead = false;
        }

        private void FixedUpdate()
        {
            if (isDead) return;
            rb.velocity = moveDirection * playerSpeed;

            if (moveDirection != Vector3.zero)
            {
                var targetRotation = Quaternion.LookRotation(lookAtPoint - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.4f);
            }
        }

        public void SetMoveDirection(Vector3 moveDirection)
        {
            this.moveDirection = moveDirection;

            var localForward = ((lookAtPoint - transform.position).normalized);
            var localRight = new Vector3(localForward.z, 0, -localForward.x); //Rotate 90 degre //refereence: https://www.gamedev.net/forums/topic/357797-rotate-a-vector-by-90-degrees/

            foreach (var a in anim)
            {
                a.SetFloat("VelY", Vector3.Dot(localForward, moveDirection), 0.05f, Time.deltaTime);
                a.SetFloat("VelX", Vector3.Dot(localRight, moveDirection), 0.05f, Time.deltaTime);
            }
        }

        public void LookAt(Vector3 lookAtPoint)
        {
            this.lookAtPoint = new Vector3(lookAtPoint.x, transform.position.y, lookAtPoint.z);
            if (shoots.Length > 0 && shoots[0])
                shoots[0].transform.parent.LookAt(lookAtPoint);
        }

        public void Shoot()
        {
            foreach (var item in shoots)
            {
                item.ShootBullet(shoots[0].transform.parent.rotation);
            }
            source.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            source.PlayOneShot(shootAudio);
        }

        public void Die()
        {
            isDead = true;
            rb.isKinematic = true;
            moveDirection = Vector3.zero;
            rb.velocity = Vector3.zero;
            foreach (var a in anim)
            {
                a.CrossFade("Die", 0.1f);
            }
        }


    }
}
