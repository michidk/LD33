using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Default 
{
	public class GameManager : MonoBehaviour 
	{
        public static GameManager Instance;

        public float ArenaSize = 100;
	    public float SpawnTime = 3;
	    public float ScoreChangedTime = 5;

	    public Text ScoreText;
        
        public int Score
	    {
	        get { return _score; }
	        set
	        {
	            _score = value; 
	            UpdateScore();
	        }
	    }
	    private int _score = 0;

	    private float lastScoreChange = 0;

	    private float currSpawnTime = 0;

        void Awake()
        {
            Instance = this;
        }

	    void Start()
	    {
	        SpawnMonsters();
	    }

	    private void SpawnMonsters()
	    {
	        for (int i = 0; i < 40; i++)
	        {
	            var randVec = new Vector2(Random.Range(-ArenaSize+20, ArenaSize-20), Random.Range(-ArenaSize + 20, ArenaSize - 20));
	            EnemyPool.Instance.Take(randVec, Quaternion.identity);
	        }
	    }

	    void Update()
	    {
	        if (currSpawnTime >= SpawnTime)
	        {
                var randVec = new Vector2(Random.Range(-ArenaSize + 20, ArenaSize - 20), Random.Range(-ArenaSize + 20, ArenaSize - 20));
                EnemyPool.Instance.Take(randVec, Quaternion.identity);

                currSpawnTime = 0;
	        }

	        currSpawnTime++;

	        float scoreTimeDiff = Time.time - lastScoreChange;
	        if (scoreTimeDiff > ScoreChangedTime)
	        {
	            Score++;
	        }
	    }

	    public void UpdateScore()
	    {
	        ScoreText.text = string.Format("Score: {0}", Score.ToString("D5"));
	        lastScoreChange = Time.time;
	    }

	}
}