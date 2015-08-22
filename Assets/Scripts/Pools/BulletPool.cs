using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class BulletPool : Pool
	{

	    public static BulletPool Instance;

	    void Awake()
	    {
	        Instance = this;
	    }

	}
}