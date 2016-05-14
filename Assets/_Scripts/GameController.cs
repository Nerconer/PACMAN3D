﻿using UnityEngine;
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

	private bool isEating;

	int elapsed_time;

	float x, y;

	GameObject pacman;
	GameObject map;
	Player pacmanScript;
	SoundController pacmanScriptMusic;

	bool starting;

	void getComponents()
	{
		x = GameObject.Find("Pacman").GetComponent<Player>().x;
		y = GameObject.Find("Pacman").GetComponent<Player>().y;
		score = GameObject.Find ("Pacman").GetComponent<Player>().score;
		lives = GameObject.Find("Pacman").GetComponent<Player>().lives;
		//isEating = GameObject.Find("Pacman").GetComponent<Player>().isEating;
	}

	// Use this for initialization
	void Start () {
		starting = true;
		getComponents();
		highScore = 0;
		elapsed_time = 0;
		pacman = GameObject.Find("Pacman");
		map = GameObject.Find("Map");
		pacmanScript = pacman.GetComponent<Player>();
		pacmanScriptMusic = map.GetComponent<SoundController>();
	}
	
	// Update is called once per frame
	void Update () {
		//isEating = false;
		elapsed_time = (int)Time.time;
		if(elapsed_time >= 4) {
			pacmanScript.move = true;
			starting = false;
		}
		getComponents();
		printTestText();
		printText();

		if(starting) {	

		} else {	// End starting song
			/*if(isEating) {
				//Debug.Log("Entro");
				pacmanScriptMusic.isEating = true;	// REVISAAAAAARRRR
			} else {
				pacmanScriptMusic.isEating = false;
				//pacmanScriptMusic.beginingMusic.Play();
			}*/
		}
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
		highScoreText.text = "High Score " + elapsed_time.ToString();
	}
		

}
