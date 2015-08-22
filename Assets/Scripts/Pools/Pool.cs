using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class Pool : MonoBehaviour 
	{
        public int StartCount = 10;
        public GameObject[] Prefabs;
	    public int GoCount { get; private set; }
	    public int MaxCount = -1;

	    private List<GameObject> pool = new List<GameObject>();

        void Start()
        {
            for (int x = 0; x < 10; x++)
            {
                Put((GameObject)Instantiate(Prefabs[Random.Range(0, Prefabs.Length - 1)], Vector3.zero, Quaternion.identity));
                GoCount++;
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
                if (MaxCount == -1 || GoCount < MaxCount)
                {
                    go = (GameObject) Instantiate(Prefabs[Random.Range(0, Prefabs.Length - 1)], pos, rot);
                    GoCount++;
                }
                else
                {
                    return null;
                }
            }
            go.transform.parent = this.transform;

            return go;
        }

        public void Put(GameObject go)
        {
            go.SetActive(false);
            pool.Add(go);
        }
        
	}
}