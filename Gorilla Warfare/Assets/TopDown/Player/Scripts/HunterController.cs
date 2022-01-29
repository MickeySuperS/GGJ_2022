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
                item.ShootBullet(shoots[0].transform.parent.rotation);
            }
            //source.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            //source.PlayOneShot(attackAudio);
        }

        public override void TakeDamage(int damageAmount)
        {
            health.ApplyDamage(20);
        }
    }
}