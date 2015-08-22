using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class PlayerEntity : BaseEntity
	{

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
            newPos.z = newPos.y;
            this.transform.position = newPos;
        }

	    public override void Kill()
	    {
	        Application.LoadLevel(0);
	    }
	}
}