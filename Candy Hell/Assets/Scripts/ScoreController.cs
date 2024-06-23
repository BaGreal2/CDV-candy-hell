using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	public TMP_Text scoreText;
	public TMP_Text highScoreText;

	int score = 0;
	int highScore = 0;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		highScore = PlayerPrefs.GetInt("highScore", 0);
		UpdateScore();
		UpdateHighScore();
	}


	public void AddPoint()
	{
		score += 1;
		UpdateScore();
		UpdateHighScore();
	}

	public void AddPoints(int scorePoints)
	{
		score += scorePoints;
		UpdateScore();
		UpdateHighScore();
	}

	void UpdateScore()
	{
		scoreText.text = "SCORE: " + score.ToString() + " POINTS";
	}
	void UpdateHighScore()
	{
		if (score > highScore)
		{
			highScore = score;
		}
		highScoreText.text = "HIGHSCORE: " + highScore.ToString() + " POINTS";
		PlayerPrefs.SetInt("highScore", score);
	}
}