using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator anim;
    SpriteRenderer rend;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    public void Animate(Vector3 moveDirection, Vector3 lookAtPoint)
    {
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

    internal void Die()
    {
        anim.CrossFade("Die", 0.1f);
    }
}
