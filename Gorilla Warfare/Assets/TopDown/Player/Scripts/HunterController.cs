using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class HunterController : PlayerController
    {

        //Shooting
        Shoot[] shoots;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            shoots = GetComponentsInChildren<Shoot>();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (isDashing)
            {
                rb.velocity = dashDirection * dashSpeed;
            }
            else
            {
                rb.velocity = moveDirection * playerSpeed;
            }
        }


        public override void LookAt(Vector3 lookAtPoint)
        {
            base.LookAt(lookAtPoint);
            if (shoots.Length > 0 && shoots[0])
                shoots[0].transform.parent.LookAt(lookAtPoint);
        }

        public override void Attack()
        {
            foreach (var item in shoots)
            {
                item.ShootBullet(shoots[0].transform.parent.rotation, damageValue);
            }
            source.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            source.PlayOneShot(attackAudio);
        }

        public override void OtherAbility()
        {
            Dash();
        }

        public override void TakeDamage(int damageAmount)
        {
            int finalDamangeAmount = (int)((float)damageAmount * damageFactor);
            base.TakeDamage(finalDamangeAmount);
            health.ApplyDamage(finalDamangeAmount);
            playerAnimatoin.hitFeedback.AnimateTakeDamage();
        }


        //Dash
        bool isDashing = false;
        Vector3 dashDirection;
        public float dashSpeed, dashTime;

        public AudioClip dashAudioClip;
        public bool canDash = false;
        public ParticleSystem ps;


        void Dash()
        {
            if (canDash)
                StartCoroutine(DashCORO());
        }

        IEnumerator DashCORO()
        {
            source.PlayOneShot(dashAudioClip);
            isDashing = true;
            dashDirection = moveDirection == Vector3.zero ? (lookAtPoint - transform.position).normalized : moveDirection;
            if (ps)
                ps.Play();
            yield return new WaitForSeconds(dashTime);

            isDashing = false;
            if (ps)
                ps.Stop();

        }

        public override void EndPS()
        {
            base.EndPS();
            if (ps)
                ps.Stop();
        }

    }
}