using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Shoot : MonoBehaviour
    {
        public Transform muzzle;
        public Bullet bulletPrefab;

        public void ShootBullet(Quaternion rotation, int attackDamage)
        {
            var bullet = Instantiate(bulletPrefab, muzzle.position, rotation) as Bullet;
            bullet.bulletDamange = attackDamage;
            Destroy(bullet.gameObject, 5f);
            //bullet.transform.rotation = Quaternion.LookRotation(targetLocation - transform.position);
        }

    }
}
