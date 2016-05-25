using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct boxPosition {
	public float x1;
	public float x2;
	public float z1;
	public float z2;
}

public class GameController : MonoBehaviour {

	public GameObject readyText;
	public float moveSpeedReadyT = 5;

	boxPosition bposL, bposR;

	private int score;
	private int highScore;
	public int level = 1;
	private int lives;

	public Text testing;
	public Text scoreText;
	public Text highScoreText;
	public Text levelText;
	public Text livesText;

	private bool isEating;

	int elapsed_time;
	int starting_time;

	float x, y;
	float posx, posz;
	bool entrat;

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
		posx = GameObject.Find("Pacman").GetComponent<Player>().transform.position.x;
		posz = GameObject.Find("Pacman").GetComponent<Player>().transform.position.z;
	}

	void detectTeleport() {
		Vector3 temp;
		if(posx < bposL.x2 && posx >= bposL.x1 && posz <= bposL.z2 && posz >= bposL.z1) {
			entrat = true;
			if(level == 1) {
				temp = new Vector3(133, 5, -14);
				pacman.GetComponent<Player>().transform.position = temp;
			}
			else if (level == 2){
				//temp = new Vector3(133, 5, -14);
				//pacman.GetComponent<Player>().transform.position = temp;
			}
		} else if(posx > bposR.x1 && posx <= bposR.x2 && posz <= bposR.z2 && posz >= bposR.z1) {
			if(level == 1) {
				temp = new Vector3(-185, 5, -14);
				pacman.GetComponent<Player>().transform.position = temp;
			}
			else if (level == 2){
				//temp = new Vector3(133, 5, -14);
				//pacman.GetComponent<Player>().transform.position = temp;
			}
		} else {
			entrat = false;
		}
	}
		
	void setLevel() {
		if(level == 1) {
			bposL.x1 = -190;
			bposL.x2 = -188;
			bposL.z1 = -16;
			bposL.z2 = -10;

			bposR.x1 = 138;
			bposR.x2 = 140;
			bposR.z1 = -16;
			bposR.z2 = -10;

		} else if(level == 2) {

		}
	}

	// Use this for initialization
	void Start () {
		starting_time = (int)Time.time;
		entrat = false;
		starting = true;
		getComponents();
		highScore = 0;
		elapsed_time = 0;
		pacman = GameObject.Find("Pacman");
		map = GameObject.Find("Map");
		pacmanScript = pacman.GetComponent<Player>();
		pacmanScriptMusic = map.GetComponent<SoundController>();
		setLevel();
	}
	
	// Update is called once per frame
	void Update () {
		elapsed_time = (int)Time.time - starting_time;
		if(elapsed_time >= 4) {
			pacmanScript.move = true;
			starting = false;
			readyText.SetActive(false);
		}
		getComponents();
		printTestText();
		printText();

		if(starting) {	
			readyText.transform.position += Vector3.up * Time.deltaTime * moveSpeedReadyT;
		} else {	// End starting song
			detectTeleport();
		}
	}

	void printTestText() 
	{
		testing.text = "x: " + x.ToString() + '\n';
		testing.text += "y: " + y.ToString() + '\n';
		testing.text += "Position X: " + posx.ToString() + '\n';
		testing.text += "Position Z: " + posz.ToString() + '\n';
		testing.text += "Entrat: " + entrat.ToString() + '\n';
	}

	void printText()
	{
		scoreText.text = "Score " + score.ToString();
		levelText.text = "Level " + level.ToString();
		livesText.text = "Lives " + lives.ToString();
		highScoreText.text = "High Score " + elapsed_time.ToString();
	}
		

}
