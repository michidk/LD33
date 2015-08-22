using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class GameManager : MonoBehaviour 
	{
        public static GameManager Instance;

        public float ArenaSize = 100;


        void Awake()
        {
            Instance = this;
        }


	}
}