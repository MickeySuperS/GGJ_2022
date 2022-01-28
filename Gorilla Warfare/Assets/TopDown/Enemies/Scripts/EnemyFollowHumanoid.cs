using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class EnemyFollowHumanoid : EnemyFollow
    {
        Animator anim;

        protected override void Start()
        {
            base.Start();
            anim = GetComponentInChildren<Animator>();
        }

        protected override void Update()
        {
            base.Update();
            
            var localForward = ((target.transform.position - transform.position).normalized);
            var localRight = new Vector3(localForward.z, 0, -localForward.x); //Rotate 90 degre //refereence: https://www.gamedev.net/forums/topic/357797-rotate-a-vector-by-90-degrees/

            anim.SetFloat("VelY", Vector3.Dot(localForward, localForward), 0.05f, Time.deltaTime);
            anim.SetFloat("VelX", Vector3.Dot(localRight, localForward), 0.05f, Time.deltaTime);
        }
    }
}
