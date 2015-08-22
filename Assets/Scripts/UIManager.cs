using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class UIManager : MonoBehaviour 
	{

        public float ScoreToWin = 50;
        public float ScoreToLoose = -100;

        public GameObject WinScreen;
        public GameObject LooseScreen;

        public GameObject ScoreText;

        private bool didWin = false;
        private bool didLoose = false;

        void Update()
        {
            if (GameManager.Instance.Score > 50 && !didWin)
            {
                didWin = true;
                WinScreen.SetActive(true);

                Time.timeScale = 0;
                PlayerEntity.Instance.IsControlling = false;
                ScoreText.SetActive(false);
            }

            if (GameManager.Instance.Score < ScoreToLoose && !didLoose)
            {
                didLoose = true;
                LooseScreen.SetActive(true);

                Time.timeScale = 0;
                PlayerEntity.Instance.IsControlling = false;
                ScoreText.SetActive(false);
            }
        }

        public void CloseScreen()
        {
            Time.timeScale = 1;
            PlayerEntity.Instance.IsControlling = true;

            ScoreText.SetActive(true);

            WinScreen.SetActive(false);
            LooseScreen.SetActive(false);
        }
	}
}