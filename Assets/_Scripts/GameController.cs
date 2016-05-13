using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameController : MonoBehaviour {

	private int score;
	private int highScore;
	private int level = 1;
	private int lives;

	public Text testing;
	public Text scoreText;
	public Text highScoreText;
	public Text levelText;
	public Text livesText;

	float x, y;

	void getComponents()
	{
		x = GameObject.Find("Pacman").GetComponent<Player>().x;
		y = GameObject.Find("Pacman").GetComponent<Player>().y;
		score = GameObject.Find ("Pacman").GetComponent<Player>().score;
		lives = GameObject.Find("Pacman").GetComponent<Player>().lives;
	}

	// Use this for initialization
	void Start () {
		getComponents();
		highScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		getComponents();
		printTestText();
		printText ();
	}

	void printTestText() 
	{
		testing.text = "x: " + x.ToString() + '\n';
		testing.text += "y: " + y.ToString() + '\n';
	}

	void printText()
	{
		scoreText.text = "Score " + score.ToString();
		levelText.text = "Level " + level.ToString();
		livesText.text = "Lives " + lives.ToString();
		highScoreText.text = "High Score " + highScore.ToString();
	}


}
