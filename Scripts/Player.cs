using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Player : MonoBehaviour
    {

        PlayerController controller;
        [SerializeField] HunterController hController;
        [SerializeField] GorillaControler gController;

        Camera camMain;
        Plane groundPlane;
        Vector3 mousePoint;

        private void Start()
        {
            camMain = Camera.main;
            groundPlane = new Plane(Vector3.up, Vector3.zero);
            controller = hController;

            lastShootingTime = -shootingCooldown;
        }

        private void Update()
        {
            if (controller.isDead) return;
            HandleRotationInput();
            HandleMoveInput();
            //HandleShooting();
        }

        void HandleMoveInput()
        {
            controller.SetMoveDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        }

        void HandleRotationInput()
        {
            Ray ray = camMain.ScreenPointToRay(Input.mousePosition);

            float rayDistance = 0.0f;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 hitPoint = ray.GetPoint(rayDistance);
                //Debug.DrawLine(ray.origin, hitPoint, Color.red);
                mousePoint = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                controller.LookAt(mousePoint);
            }
        }

        [ContextMenu("Controller Switch")]
        public void SwitchController()
        {
            controller = controller == hController ? gController : hController;
        }

        public float shootingCooldown = 0.5f;
        float lastShootingTime;
        //void HandleShooting()
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        if (Time.time < lastShootingTime + shootingCooldown) return;
        //        lastShootingTime = Time.time;

        //        controller.Shoot();
        //    }
        //}

    }
}
