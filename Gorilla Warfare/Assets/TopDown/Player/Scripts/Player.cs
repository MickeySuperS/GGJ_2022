using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Player : MonoBehaviour
    {

        [HideInInspector] public PlayerController controller;
        [SerializeField] HunterController hController;
        [SerializeField] GorillaControler gController;

        Camera camMain;
        Plane groundPlane;
        Vector3 mousePoint;

        public float switchingRandomMin = 5, switchingRandomMax = 8;

        private void Start()
        {
            camMain = Camera.main;
            groundPlane = new Plane(Vector3.up, Vector3.zero);
            controller = hController;
            gController.enabled = false;

            lastShootingTime = -100;

            StartCoroutine(SwitchControllerCORO());
        }

        private void Update()
        {
            if (controller.isDead) return;
            HandleRotationInput();
            HandleMoveInput();
            HandleAttack();
            HandleDash();
            HandleWarning();
        }

        public GameObject warningGameObject;
        public float warnBeforeSeconds = 3;
        float timeRemainingForSwitch = 0;

        void HandleWarning()
        {

            warningGameObject.SetActive(timeRemainingForSwitch <= warnBeforeSeconds);
        }

        private void HandleDash()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                controller.Dash();
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


        public IEnumerator SwitchControllerCORO()
        {
            while (!controller.isDead)
            {
                SwitchController();
                timeRemainingForSwitch = Random.Range(switchingRandomMin, switchingRandomMax);
                while (timeRemainingForSwitch > 0)
                {
                    timeRemainingForSwitch -= Time.deltaTime;
                    yield return null;
                }

            }
        }



        [ContextMenu("Controller Switch")]
        public void SwitchController()
        {
            controller = controller == hController ? gController : hController;
            gController.enabled = controller != hController;
            hController.enabled = controller == hController;
        }

        float lastShootingTime;
        void HandleAttack()
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time < lastShootingTime + controller.attackCD) return;
                lastShootingTime = Time.time;

                controller.Attack();
            }
        }

    }
}