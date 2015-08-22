using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
    [ExecuteInEditMode]
	public class PixelPerfectCamera : MonoBehaviour 
	{

        public float PixelSize = 1;

        private Camera cam;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
#if UNITY_EDITOR
            cam.orthographicSize = (((Screen.width / Screen.height) * 2) * PixelSize);
#endif
        }

	}
}