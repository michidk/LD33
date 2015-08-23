using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Default
{
    public class PlayerEntity : BaseEntity
    {
        public static PlayerEntity Instance;

        public AudioClip GunSound;
        public AudioClip HurtSound;

        public GameObject[] WeaponPos;

        [HideInInspector]
        public bool IsControlling = true;

        private Animator animator;
        private AudioSource audio;
        private LineRenderer line;

        private Vector2 movement = Vector2.zero;
        private int dir = 2;

        void Awake()
        {
            base.Awake();

            Instance = this;

            animator = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();
            line = GetComponent<LineRenderer>();
        }

        void Start()
        {
            base.Start();
        }

        void Update()
        {
            base.Update();

            if (IsControlling)
            {
                Move();
                Animate();
                //DrawLaserLine();
                Shoot();
            }
            else
            {
                Animate();
            }

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyEntity enemy = collision.GetComponent<EnemyEntity>();
                enemy.Kill();
            }
        }

        private void Move()
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            movement *= MoveSpeed * Time.deltaTime;
            Vector3 newPos = this.transform.position + (Vector3)movement;
            newPos = new Vector3(
                Mathf.Clamp(newPos.x, -GameManager.Instance.ArenaSize, GameManager.Instance.ArenaSize),
                Mathf.Clamp(newPos.y, -GameManager.Instance.ArenaSize, GameManager.Instance.ArenaSize));
            newPos.z = newPos.y - collider.bounds.size.y / 2;

            this.transform.position = newPos;
        }

        private void Animate()
        {
            Vector2 mousePos;
            var controllerAim = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
            if (controllerAim.magnitude > 0)
            {
                //use controller
                mousePos = new Vector2(Screen.width / 2, Screen.height / 2) + controllerAim;
            }
            else
            {
                mousePos = Input.mousePosition;
            }

            var objectPos = Camera.main.WorldToScreenPoint(this.transform.position);

            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg * -1 + 90;

            if ((angle > -45 && angle < 0) || (angle < 45 && angle > 0))
            {
                dir = 0;
            }
            if (angle > 45 && angle < 135)
            {
                dir = 1;
            }
            if (angle > 135 && angle < 225)
            {
                dir = 2;
            }
            if (angle > 225 && angle < 315 || angle > -90 && angle < -45)
            {
                dir = 3;
            }

            animator.SetInteger("dir", dir);
            animator.SetFloat("speed", movement.magnitude > 0 ? 1 : 0);
        }

        private void DrawLaserLine()
        {
            var wPos = WeaponPos[dir].transform.position;
            line.SetPosition(0, wPos);
            line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        private bool lastFrameShot = false;
        private void Shoot()
        {
            var weaponPos = WeaponPos[dir].gameObject.transform.position;
            var objectPos = Camera.main.WorldToScreenPoint(weaponPos);

            Vector2 mousePos;
            var controllerAim = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
            if (controllerAim.magnitude > 0)
            {
                //use controller
                mousePos = (Vector2) objectPos + controllerAim;
            }
            else
            {
                mousePos = Input.mousePosition;
            }

            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (Input.GetButtonDown("Fire1") || (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.Alpha3)) || (Mathf.Abs(Input.GetAxisRaw("Fire1")) > 0 && !lastFrameShot))
            {
                //Quaternion.AngleAxis(angle, Vector3.forward)
                bulletPool.Take(weaponPos, Quaternion.Euler(new Vector3(0, 0, angle)));
                audio.PlayOneShot(GunSound);
                lastFrameShot = true;
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Fire1")) <= 0)
            {
                lastFrameShot = false;
            }
        }

        public override void Kill()
        {
            Application.LoadLevel(0);
        }

        public AudioSource GetAudio()
        {
            return audio;
        }
    }
}