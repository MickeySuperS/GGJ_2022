using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Screenshake : MonoBehaviour
    {

        public static Screenshake instance;

        private void Awake()
        {
            instance = this;
        }


        public Transform camTrans;

        public AnimationCurve xOffse, yOffset;
        float shakeTime;
        float totalShakeTime = 0;

        Vector3 originalRotation;
        float shakeMultiplier;

        private void Start()
        {
            originalRotation = camTrans.eulerAngles;
        }


        public void StartShake(float shakeMultiplier = 1f, float totalShakeTime = 0.1f)
        {
            this.totalShakeTime = totalShakeTime;
            this.shakeMultiplier = shakeMultiplier;
            shakeTime = 0;
            StopAllCoroutines();
            StartCoroutine(CameraShake());
        }

        IEnumerator CameraShake()
        {
            while (shakeTime < 1)
            {
                ApplyShakeRotation(shakeTime);
                shakeTime += Time.deltaTime / totalShakeTime;
                yield return null;
            }
        }

        void ApplyShakeRotation(float t)
        {
            camTrans.transform.rotation = Quaternion.Euler(originalRotation + new Vector3(xOffse.Evaluate(t), yOffset.Evaluate(t), 0) * shakeMultiplier);
        }



    }
}
