using UnityEngine;
using System;

namespace TopDown
{
    public class TeleportController : MonoBehaviour
    {

        [Range(0f, 2)] public float timeBeforeTeleport = 1f, timeAfterTeleport = 1f;
        Action teleportCompletedCallback;
        float teleportRequestTime, teleportCompleted;
        bool isTeleporting = false, completedTeleport = true;

        Boss boss;

        private void Start()
        {
            boss = GetComponentInParent<Boss>();
        }

        [ContextMenu("Teleport")]
        public void Teleport()
        {
            teleportRequestTime = Time.time;
            isTeleporting = true;
            completedTeleport = false;
            teleportCompleted = Time.time + timeBeforeTeleport;
        }
        public void Teleport(Action callbackWhenDone = null)
        {
            teleportCompletedCallback = callbackWhenDone;
            Teleport();
        }

        private void Update()
        {
            if (completedTeleport) return;

            if (!isTeleporting && Time.time > teleportCompleted + timeAfterTeleport)
            {
                teleportCompletedCallback?.Invoke();
                completedTeleport = true;
                return;
            }

            if (!isTeleporting) return;
            if (Time.time < teleportRequestTime + timeBeforeTeleport) return;

            Vector3 newRandomPoint = boss.GetRandomPoint();

            transform.parent.position = newRandomPoint;
            teleportCompleted = Time.time;
            isTeleporting = false;
        }



    }
}