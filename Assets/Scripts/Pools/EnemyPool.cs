using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class EnemyPool : Pool 
	{

        public static EnemyPool Instance;

        void Awake()
        {
            Instance = this;
        }

    }
}