using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class HitFeedback : MonoBehaviour
    {

        SpriteRenderer rend;
        private MaterialPropertyBlock block;
        private int lerpID;


        private void Start()
        {
            rend = GetComponent<SpriteRenderer>();
            block = new MaterialPropertyBlock();
            lerpID = Shader.PropertyToID("_DamageEffect");
        }

        public AnimationCurve DamageEffectCurve;
        public float DamageEffectTime = 0.2f;
        float damageTime = 0;
        public void AnimateTakeDamage()
        {
            damageTime = 0;
            StopAllCoroutines();
            StartCoroutine(TakeDamageAnimation());
        }

        IEnumerator TakeDamageAnimation()
        {
            while (damageTime < 1)
            {
                ApplyColor(damageTime);
                damageTime += Time.deltaTime / DamageEffectTime;
                yield return null;
            }
            ApplyColor(1);
        }

        public void ApplyColor(float t)
        {
            if (!rend) return;
            rend.GetPropertyBlock(block);
            block.SetFloat(lerpID, DamageEffectCurve.Evaluate(t));
            rend.SetPropertyBlock(block);
        }

    }
}
