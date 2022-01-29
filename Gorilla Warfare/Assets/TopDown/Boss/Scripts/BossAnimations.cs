using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{

    public class BossAnimations : MonoBehaviour
    {
        Animator anim;
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void Summon()
        {
            anim.Play("Summon");
        }

    }
}
