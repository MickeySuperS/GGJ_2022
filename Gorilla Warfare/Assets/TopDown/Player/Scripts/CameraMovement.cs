using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform cameraTarget;
        Vector3 offset;

        private void Start()
        {
            offset = cameraTarget.position - transform.position;
        }

        private void LateUpdate()
        {
            transform.position = cameraTarget.position - offset;
        }
    }
}
