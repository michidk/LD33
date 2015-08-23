using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class EnemyEntity : BaseEntity
	{

	    public float JitterFac = 5;
	    public float MaxStraightTime = 5;
	    public GameObject BloodPrefab;

	    private float c = 0;
	    private bool straight = false;
	    private float straightTime = 1;
	    private float currStraightTime = 0;
        private Vector2 straightVec = Vector2.zero;
	    private float randPey = 0;
	    private int randCircleDir = 1;

	    void Start()
	    {
            base.Start();

            randPey = Random.Range(0f, 2f * Mathf.PI);
            randCircleDir = Random.value > .5f ? -1 : 1;
        }

	    void Update()
	    {
	        base.Update();

	        if (!straight)
	        {
	            c += Time.deltaTime;
                if (c > Mathf.PI * 2 - randPey)
                {
                    straight = true;
                    straightTime = Random.Range(0f, MaxStraightTime);
                    straightVec = Random.insideUnitCircle;
                    c = -1;
                    randPey = Random.Range(0f, 2f * Mathf.PI);
                    randCircleDir = Random.value > .5f ? -1 : 1;
                }
            }

	        Vector2 movement = Vector2.zero;

	        if (!straight)
	        {
	            movement = new Vector2(Mathf.Sin(c * randCircleDir), Mathf.Cos(c * randCircleDir));
	        }
	        else
	        {
                movement = straightVec;

                currStraightTime += Time.deltaTime;
	            if (currStraightTime > straightTime)
	            {
	                straight = false;
	                c = 0;
	                currStraightTime = 0;
	            }
	        }

            movement += new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * JitterFac;
	        movement *= MoveSpeed * Time.deltaTime;
            Vector3 newPos = this.transform.position + (Vector3) movement;
	        newPos = new Vector3(
	            Mathf.Clamp(newPos.x, -GameManager.Instance.ArenaSize, GameManager.Instance.ArenaSize),
	            Mathf.Clamp(newPos.y, -GameManager.Instance.ArenaSize, GameManager.Instance.ArenaSize));
            newPos.z = newPos.y;
            this.transform.position = newPos;
        }

	    public override void Kill()
	    {
            enemyPool.Put(this.gameObject);

	        var bloodPos = this.transform.position;
	        bloodPos.z += 400;
            Instantiate(BloodPrefab, bloodPos, Quaternion.identity);

	        PlayerEntity.Instance.GetAudio().PlayOneShot(PlayerEntity.Instance.HurtSound);
	        GameManager.Instance.Score--;
	    }

	    private RaycastHit2D Sidecast(Vector2 dir)
	    {
	        var origin = new Vector2(this.transform.position.x + dir.x * (collider.bounds.size.x / 2) + .02f * dir.x, this.transform.position.y + .15f);
#if UNITY_EDITOR
            Debug.DrawLine(origin, origin + dir * .3f);
#endif
            return Physics2D.Raycast(origin, dir, .3f, 1 << 8);
	    }

	}
}