using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class MoveController : MonoBehaviour
    {
        bool isMoving;
        public float speed = 10;
        public float distanceFromPlayer = 1;
        float currentSpeed;

        float lastTimeSwitched = 0;
        public float timeToSwitchDirection = 6;
        float dirValue = 1;

        Rigidbody rb;
        Vector3 moveDirection;


        Boss boss;
        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>();
            boss = GetComponentInParent<Boss>();
            isMoving = false;
            currentSpeed = 0;
        }

        private void Update()
        {
            if (!isMoving) return;

            if (Time.time > lastTimeSwitched + timeToSwitchDirection)
            {
                lastTimeSwitched = Time.time - Random.Range(0, timeToSwitchDirection);
                dirValue *= -1;
            }

            Vector3 dir = (boss.target.position - transform.position);
            float forwardBackwardFactor = dir.magnitude - distanceFromPlayer;
            dir.Normalize();
            var localRight = new Vector3(dir.z, 0, -dir.x); //Rotate 90 degre //refereence: https://www.gamedev.net/forums/topic/357797-rotate-a-vector-by-90-degrees/

            moveDirection = (forwardBackwardFactor * dir + localRight * dirValue).normalized;
        }

        private void FixedUpdate()
        {
            rb.velocity = moveDirection * currentSpeed;
        }

        [ContextMenu("Start Moving")]
        public void StartMoving()
        {
            isMoving = true;
            currentSpeed = speed;
        }

        [ContextMenu("Stop Moving")]
        public void StopMoving()
        {
            isMoving = false;
            currentSpeed = 0;
        }


    }
}
