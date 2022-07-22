using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Paddle paddle { get; private set;}
	public Ball ball { get; private set;}
	public Brick[] bricks { get; private set;}
	public ScoreUI scoreUI;
		
	public int level = 1;
	public int score
	{
		get
		{
			return this.Score;
		}

		set
		{
			this.Score = value;
			if(this.scoreUI != null)
				this.scoreUI.SetScore(this.Score);
		}
	}
	private int Score;
	public int lives = 3;
	

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		SceneManager.sceneLoaded += OnLevelLoaded;
	}

	private void Start()
	{
		NewGame();
	}

	private void NewGame()
	{
		LoadLevel(1);
		this.score = 0;
		this.lives = 3;
	}

	private void LoadLevel(int level)
	{
		this.level = level;
		SceneManager.LoadScene("Level"+level);
	}

	private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
	{
		this.ball = FindObjectOfType<Ball>();
		this.paddle = FindObjectOfType<Paddle>();
		this.bricks = FindObjectsOfType<Brick>();
		this.scoreUI = FindObjectOfType<ScoreUI>();
	}
	
	private void ResetLevel()
	{
		this.ball.ResetBall();
		this.paddle.ResetPaddle();
	}

	private void GameOver()
	{
		NewGame();
	}

	public void Miss()
	{
		this.lives--;

		if(this.lives > 0)
		{
			ResetLevel();
		}
		else
		{
			GameOver();
		}

	}

	public void Hit(Brick brick)
	{
		this.score += brick.points;
		if(Cleared())
		{
			LoadLevel(this.level + 1);
		}
	}

	private bool Cleared()
	{
		for(int i = 0; i < this.bricks.Length; i++)
		{
			if(this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
			{
				return false;
			}
		}

		return true;
	}
}
