using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{

    public class EnemyAnimation : MonoBehaviour
    {
        SpriteRenderer rend;
        public Animator anim;
        private void Start()
        {
            anim = GetComponent<Animator>();
            rend = GetComponent<SpriteRenderer>();
        }


        public void Animate(Vector3 targetPosition)
        {
            var diff = transform.position - targetPosition;
            rend.flipX = diff.x < 0;

        }
    }
}
