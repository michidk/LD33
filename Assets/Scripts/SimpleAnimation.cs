using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
    [RequireComponent(typeof(SpriteRenderer))]
	public class SimpleAnimation : MonoBehaviour
    {

        public float AnimationTime;
	    public Sprite[] Sprites;
        
        private SpriteRenderer renderer;

        private int currSprite = 0;
        private float currTime = 0;

        void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (Sprites.Length > 0)
                renderer.sprite = Sprites[currSprite];

            if (currTime > AnimationTime)
            {
                currSprite = (currSprite + 1) % Sprites.Length;

                currTime = 0;
            }

            currTime += Time.deltaTime;
        }
	}
}