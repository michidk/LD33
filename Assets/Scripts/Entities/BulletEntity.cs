using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Default
{
    public class BulletEntity : BaseEntity
    {

        public float MaxDistance = 20;

        void Update()
        {
            base.Update();

            this.transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(this.transform.position, PlayerEntity.Instance.transform.position) > MaxDistance)
            {
                Kill();
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyEntity enemy = collision.GetComponent<EnemyEntity>();
                enemy.Kill();
                Kill();
            }
        }

        public override void Kill()
        {
            bulletPool.Put(this.gameObject);
        }
    }
}