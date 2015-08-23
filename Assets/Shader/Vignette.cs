using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
    [ExecuteInEditMode]
    public class Vignette : MonoBehaviour
    {
        [Range(0, 10)]
        public float Power = 5.0f;

        private Material mat;

        void Awake()
        {
            mat = new Material(Shader.Find("Custom/Vignette"));
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (Power <= 0)
            {
                Graphics.Blit(source, destination);
            }
            else
            {
                mat.SetFloat("_Power", Power);
                Graphics.Blit(source, destination, mat);
            }
        }
    }
}