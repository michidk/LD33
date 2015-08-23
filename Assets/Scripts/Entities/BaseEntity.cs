using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public float MoveSpeed = 15;
    
        protected SpriteRenderer renderer;
        protected BoxCollider2D collider;

        protected BulletPool bulletPool;
        protected EnemyPool enemyPool;
        
        protected virtual void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();

            bulletPool = BulletPool.Instance;
            enemyPool = EnemyPool.Instance;
        }

        protected virtual void Start() {}

        protected virtual void Update() {}

        public abstract void Kill();
    }
}