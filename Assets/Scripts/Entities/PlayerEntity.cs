using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Default 
{
	public class PlayerEntity : BaseEntity
	{
	    public static PlayerEntity Instance;

	    public GameObject[] WeaponPos;

	    [HideInInspector]
        public bool IsControlling = true;

	    private Animator animator;
	    private LineRenderer line;

	    private Vector2 movement = Vector2.zero;
	    private int dir = 2;

	    void Awake()
	    {
	        base.Awake();

	        Instance = this;
	    }

	    void Start()
	    {
	        base.Start();

	        animator = GetComponent<Animator>();
	        line = GetComponent<LineRenderer>();
	    }

        void Update()
        {
            base.Update();

            if (IsControlling)
            {
                Move();
                Animate();
                DrawLaserLine();
                Shoot();
            }
            else
            {
                Animate();
            }

        }

	    private void Move()
	    {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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
            var object_pos = Camera.main.WorldToScreenPoint(this.transform.position);
            var mouse_pos = Input.mousePosition;
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg * -1 + 90;

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

	    private void Shoot()
	    {
	        var weaponPos = WeaponPos[dir].gameObject.transform.position;

            var object_pos = Camera.main.WorldToScreenPoint(weaponPos);
	        var mouse_pos = Input.mousePosition;
            mouse_pos.x = mouse_pos.x - object_pos.x;
            mouse_pos.y = mouse_pos.y - object_pos.y;
            var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

            if (Input.GetButtonDown("Fire1") || (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.Alpha3)))
            {
                //Quaternion.AngleAxis(angle, Vector3.forward)
                bulletPool.Take(weaponPos, Quaternion.Euler(new Vector3(0, 0, angle)));
            }
        }

	    public override void Kill()
	    {
	        Application.LoadLevel(0);
	    }
	}
}