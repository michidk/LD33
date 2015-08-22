using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Default 
{
    public class BulletEntity : BaseEntity
    {



        public override void Kill()
        {
            BulletPool.Instance.Put(this.gameObject);
        }
    }
}