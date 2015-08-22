using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Default 
{
	public class PlayerEntity : BaseEntity
	{
	    public GameObject[] WeaponPos;

	    private Animator animator;
	    private LineRenderer line;

	    void Start()
	    {
	        base.Start();

	        animator = GetComponent<Animator>();
	        line = GetComponent<LineRenderer>();
	    }

        void Update()
        {
            base.Update();

            //moving
            var movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            movement *= MoveSpeed * Time.deltaTime;
            Vector3 newPos = this.transform.position + (Vector3)movement;
            newPos = new Vector3(
                Mathf.Clamp(newPos.x, -GameManager.Instance.ArenaSize, GameManager.Instance.ArenaSize),
                Mathf.Clamp(newPos.y, -GameManager.Instance.ArenaSize, GameManager.Instance.ArenaSize));
            newPos.z = newPos.y - collider.bounds.size.y / 2;

            this.transform.position = newPos;
            
            //animator params
            int dir = 2;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 relMousePos = mousePos - this.transform.position;
            float angle = Vector2.Angle(Vector2.up, relMousePos) % 360;
            Vector3 cross = Vector3.Cross(Vector2.up, relMousePos);
            if (cross.z > 0)
                angle = 360 - angle;

            if ((angle < 45 && angle > 0) || (angle < 360 && angle > 315))
            {
                dir = 0;
            }
            else if (angle > 45 && angle < 135)
            {
                dir = 1;
            }
            else if (angle > 135 && angle < 225)
            {
                dir = 2;
            }
            else if (angle > 225 && angle < 315)
            {
                dir = 3;
            }


            var wPos = WeaponPos[dir].transform.position;
            
            animator.SetInteger("dir", dir);
            animator.SetFloat("speed", movement.magnitude > 0 ? 1 : 0);

            //mouse target line
            line.SetPosition(0, wPos);
            line.SetPosition(1, mousePos);
        }

	    public override void Kill()
	    {
	        Application.LoadLevel(0);
	    }
	}
}