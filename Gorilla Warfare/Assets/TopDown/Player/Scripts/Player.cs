using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class Player : MonoBehaviour, IHitable
    {

        [HideInInspector] public PlayerController controller;
        public HunterController hController;
        public GorillaControler gController;
        [SerializeField] GameObject hObject;
        [SerializeField] GameObject gObject;

        Camera camMain;
        Plane groundPlane;
        Vector3 mousePoint;

        public float switchingRandomMin = 5, switchingRandomMax = 8;

        public AudioClip transformClip, poofAudioClip;
        public ParticleSystem transformPs;

        private void Awake()
        {
            camMain = Camera.main;
            groundPlane = new Plane(Vector3.up, Vector3.zero);
            controller = gController;
            gController.enabled = true;
            hController.enabled = false;

            lastShootingTime = -100;


        }

        private void Start()
        {
            StartCoroutine(SwitchControllerCORO());
        }

        private void Update()
        {
            if (controller.isDead) return;
            HandleRotationInput();
            if (GamePause.gameIsPaused) return;
            HandleMoveInput();
            HandleAttack();
            HandleDash();
            HandleWarning();
        }

        public GameObject warningGameObject;
        public float warnBeforeSeconds = 3;
        float timeRemainingForSwitch = 0;

        public SpriteRenderer warningRenderer;

        void HandleWarning()
        {
            warningGameObject.SetActive(timeRemainingForSwitch <= warnBeforeSeconds);
            if (timeRemainingForSwitch <= warnBeforeSeconds)
                warningRenderer.material.SetFloat("_Gradient", timeRemainingForSwitch / warnBeforeSeconds);
        }

        private void HandleDash()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                controller.OtherAbility();
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
            hController.enabled = controller == hController;
            gController.enabled = controller != hController;
            hObject.SetActive(controller == hController);
            gObject.SetActive(controller != hController);
            hController.isAttacking = false;
            gController.isAttacking = false;
            gController.playerAnimatoin.anim.StopPlayback();
            hController.playerAnimatoin.anim.StopPlayback();
            lastShootingTime = -controller.attackCD;
            hController.playerAnimatoin.hitFeedback.ApplyColor(0);
            gController.playerAnimatoin.hitFeedback.ApplyColor(0);

            gController.source.PlayOneShot(poofAudioClip);
            transformPs.Play();
            if (gController.enabled)
            {
                gController.source.PlayOneShot(transformClip);
                CursorChanger.instance.SetGorillaCursor();
            }
            else
            {
                CursorChanger.instance.SetHunterCursor();
            }

            hController.EndPS();
        }

        float lastShootingTime;
        void HandleAttack()
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time < lastShootingTime + controller.attackCD) return;
                lastShootingTime = Time.time;

                controller.PlayAttackAnim();
            }
        }

        public AudioClip playerGetHitClip;
        public void TakeDamage(int damage)
        {
            if (playerGetHitClip)
                controller.source.PlayOneShot(playerGetHitClip);
            controller.TakeDamage(damage);
            Screenshake.instance.StartShake(-0.05f);
        }
    }
}