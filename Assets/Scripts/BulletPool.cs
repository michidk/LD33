using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class BulletPool : MonoBehaviour 
	{
        public static BulletPool Instance;

        public int StartCount = 10;
        public GameObject BulletPrefab;

        private List<GameObject> pool = new List<GameObject>();

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            for (int x = 0; x < 10; x++)
            {
                Put((GameObject)Instantiate(BulletPrefab, Vector3.zero, Quaternion.identity));
            }
        }

        public GameObject Take(Vector3 pos, Quaternion rot)
        {
            GameObject go = null;

            if (pool.Count > 0)
            {
                go = pool[0];
                pool.RemoveAt(0);
                go.transform.position = pos;
                go.transform.rotation = rot;
                go.SetActive(true);
            }
            else
            {
                go = (GameObject) Instantiate(BulletPrefab, pos, rot);
            }

            return go;
        }

        public void Put(GameObject go)
        {
            go.SetActive(false);
            pool.Add(go);
        }
        
	}
}