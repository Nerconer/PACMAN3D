using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	Vector3[] cameraPositions = new [] {
		new Vector3(-0.45f, 24.0f, 7.0f),
		new Vector3(-0.20f, 17.0f, -14.4f),
		new Vector3(-15.0f, 17.0f, -0.4f),
		new Vector3(15.0f, 17.0f, -0.4f),
		new Vector3(0.2f, 17.0f, 14.4f)
	};

	public float moveSpeed;
	//private Animator anim;

	public float x;
	public float y;

	public Transform winCanvas;
	public Transform gameOverCanvas;
	public Text finalScoreCanvas;
	public GameObject final;
	public GameObject info;

	public Quaternion initialRotation;
	public Vector3 initialPosition;


	int startTime;

	private float pauseDelay = 0;

	public string name;
	public static bool isDeath;

	//public int score;
	public int lives;

	public static bool move;

	GameObject map;
	SoundController sc;

	bool isPowerUp = false;

	private int pills_num;

	private Animator animator;

	private bool win;
	private bool gameover;

	// Use this for initialization
	void Start () {
		//anim = gameObject.GetComponent<Animator>();
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		lives = 3;
		move = false;
		map = GameObject.Find("Map");
		isDeath = false;
		sc = map.GetComponent<SoundController>();
		animator = GetComponent<Animator>();
		//moveSpeed = 50;
		initialRotation = transform.rotation;

		startTime =0;

		pills_num = 0;

		win = false;
		gameover = false;


	}

	void moveLevel1() {
		bool isMoving = false;
		if(x > 0) {
			isMoving = true;
			transform.position += Vector3.right * Time.deltaTime * moveSpeed;
			transform.localEulerAngles = new Vector3(0, 90, 0);
		} else if(x < 0) {
			isMoving = true;
			transform.position += Vector3.left * Time.deltaTime * moveSpeed;
			transform.localEulerAngles = new Vector3(0, 270, 0);
		} else if(y > 0) {
			isMoving = true;
			transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
			transform.localEulerAngles = new Vector3(0, 0, 0);
		} else if(y < 0) {
			isMoving = true;
			transform.position += Vector3.back * Time.deltaTime * moveSpeed;
			transform.localEulerAngles = new Vector3(0, 180, 0);
		}
			
		animator.SetBool ("wakawaka", isMoving);


	}

	void moveLevel2(){
		if(TeleportsMap2.cubeFace == 1) {
			moveLevel1();
		} else if(TeleportsMap2.cubeFace == 2) {
			if(x > 0) {
				transform.position += Vector3.right * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, -270, -90);
			} else if(x < 0) {
				transform.position += Vector3.left * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, -90, 90);
			} else if(y > 0) {
				transform.position += Vector3.up * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(270, 0, 0);
			} else if(y < 0) {
				transform.position += Vector3.down * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(-270, 0, 0);
			}
		} else if(TeleportsMap2.cubeFace == 3) {
			if(x > 0) {	// RIGHT
				transform.position += Vector3.back * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 180, 270);
			} else if(x < 0) {	// LEFT
				transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 0, 90);
			} else if(y > 0) {	// UP
				transform.position += Vector3.up * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(-90, 90, 0);
			} else if(y < 0) {	// DOWN
				transform.position += Vector3.down * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(90, 90, 0);
			}
		} else if(TeleportsMap2.cubeFace == 4) {
			if(x > 0) {	// RIGHT
				transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 0, 90);
			} else if(x < 0) {	// LEFT
				transform.position += Vector3.back * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, 180, 270);
			} else if(y > 0) {	// UP
				transform.position += Vector3.up * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(-90, 90, 0);
			} else if(y < 0) {	// DOWN
				transform.position += Vector3.down * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(90, 90, 0);
			}
		} else if(TeleportsMap2.cubeFace == 5) {
			if(x > 0) {	// RIGHT
				transform.position += Vector3.left * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, -90, 90);
			} else if(x < 0) {	// LEFT
				transform.position += Vector3.right * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(0, -270, -90);
			} else if(y > 0) {	// UP
				transform.position += Vector3.up * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(270, 0, 0);
			} else if(y < 0) {	// DOWN
				transform.position += Vector3.down * Time.deltaTime * moveSpeed;
				transform.localEulerAngles = new Vector3(-270, 0, 0);			}
		} 

	}

	void stopAllMusic() {
		for (int i = 0; i < SoundController.source.Length; ++i) {
			SoundController.source[i].Stop();
		}
	}

	void Update() {
		if(win) {
			stopAllMusic();
			if(Input.anyKeyDown) {
				Time.timeScale = 1;
				SceneManager.LoadScene(0);
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if(win) {
			stopAllMusic();
			Time.timeScale = 0;
			move = false;
			winCanvas.gameObject.SetActive(true);
			//finalScoreCanvas.GetComponent<Text>().enabled = true;
			final.SetActive(true);
			info.SetActive(false);
			finalScoreCanvas.text = "Final Score: " + GameController.score.ToString() + '\n';
			finalScoreCanvas.text += "Time : " + (int) Time.time + '\n';


		} else if(gameover) {
			stopAllMusic();
			move = false;
			gameOverCanvas.gameObject.SetActive(true);
			if(Input.anyKeyDown) {
				SceneManager.LoadScene(0);
			}
		} else {
			
			isDeath = animator.GetBool("isDeath");

			if(move && pauseDelay == 0) {
				x = Input.GetAxisRaw("Horizontal");
				y = Input.GetAxisRaw("Vertical");

				if(GameController.level == 1) {
					moveLevel1();
				} else if(GameController.level == 2) {
					moveLevel2();
				}
			} else if(pauseDelay > 0) {
				--pauseDelay;
				if(pauseDelay == 0) {
					//isDeath = false;
					transform.position = initialPosition;
					transform.rotation = initialRotation;
				}

			}


			if((int)Time.time >= startTime + 7 && isPowerUp) {

				isPowerUp = false;

				GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost Intelligent");

				for (int i = 0; i < ghosts.Length; ++i) {
					if(ghosts[i].activeSelf && !ghosts[i].GetComponent<GhostInteligent>().isDeath)
						ghosts[i].GetComponent<GhostInteligent>().toNormalState();
				}

				ghosts = GameObject.FindGameObjectsWithTag("Ghost");

				for (int i = 0; i < ghosts.Length; ++i) {
					if(ghosts[i].activeSelf && !ghosts[i].GetComponent<GhostController>().isDeath)
						ghosts[i].GetComponent<GhostController>().toNormalState();
				}


			} else if(isPowerUp && (int)Time.time >= startTime + 4) {
				GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost Intelligent");

				for (int i = 0; i < ghosts.Length; ++i) {
					if(ghosts[i].activeSelf && !ghosts[i].GetComponent<GhostInteligent>().isDeath)
						ghosts[i].GetComponent<GhostInteligent>().Blink();
				}

				ghosts = GameObject.FindGameObjectsWithTag("Ghost");

				for (int i = 0; i < ghosts.Length; ++i) {
					if(ghosts[i].activeSelf && !ghosts[i].GetComponent<GhostController>().isDeath)
						ghosts[i].GetComponent<GhostController>().Blink();
				}

			}
		}



	}

	void OnTriggerEnter(Collider other) 
	{

		name = other.gameObject.tag;
		if (other.gameObject.CompareTag("Pick Up"))
		{
			GameController.score += 10;
			other.gameObject.SetActive(false);

			//sc.soundEating();
			sc.playMusic(1);
			++pills_num;
			Debug.Log("Pills num: " + pills_num);
			if(GameController.level == 1 && pills_num == 272)
				win = true;
		}

		else if (other.gameObject.CompareTag ("Ghost") && !isDeath)
		{
			GhostController ghostController = other.gameObject.GetComponent<GhostController> ();

			if (!ghostController.isDeath) {

				if (ghostController.getIsRunningAway ()) {
					animator.SetBool ("wakawaka", false);
					ghostController.isDeathTime ();
					sc.playMusic(5);
				}
				else {
					animator.SetBool ("wakawaka", false);
					GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost Intelligent");

					for (int i = 0; i < ghosts.Length; i++) {
						if (ghosts [i].activeSelf)
							ghosts [i].GetComponent<GhostInteligent> ().returnToInitialPosition ();
					}

					ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

					for (int i = 0; i < ghosts.Length; i++) {
						if (ghosts [i].activeSelf)
							ghosts [i].GetComponent<GhostController> ().returnToInitialPosition ();
					}
					animator.SetBool ("isDeath", true);
					isDeath = true;
					StartCoroutine(ResumeAfterSeconds(1));
					pauseDelay = 120;
					sc.playMusic(2);
					initialRotation = transform.rotation;
					lives--;
					Debug.Log("Lives "+lives);
					if(lives == 0)
						gameover = true;


				}
					
			}
		}

		else if (other.gameObject.CompareTag ("Ghost Intelligent") && !isDeath) {

			GhostInteligent ghostIntelligent = other.gameObject.GetComponent<GhostInteligent> ();
			if (!ghostIntelligent.isDeath) {

				if (ghostIntelligent.getIsRunningAway ()) {
					animator.SetBool ("wakawaka", false);
					ghostIntelligent.isDeathTime ();
					sc.playMusic(5);
				}
				else {
					animator.SetBool ("wakawaka", false);
					GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost Intelligent");

					for (int i = 0; i < ghosts.Length; i++) {
						if (ghosts [i].activeSelf)
							ghosts [i].GetComponent<GhostInteligent> ().returnToInitialPosition ();
					}

					ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

					for (int i = 0; i < ghosts.Length; i++) {
						if (ghosts [i].activeSelf)
							ghosts [i].GetComponent<GhostController> ().returnToInitialPosition ();
					}

					animator.SetBool ("isDeath", true);
					isDeath = true;
					StartCoroutine(ResumeAfterSeconds(1));
					pauseDelay = 120;
					initialRotation = transform.rotation; 
					sc.playMusic(2);
					lives--;
					Debug.Log("Lives "+lives);
					if(lives == 0)
						gameover = true;

				}
			}


		}

		else if (other.gameObject.CompareTag ("Power Up")) 
		{
			GameController.score += 50;
			sc.playMusic(4);
			isPowerUp = true;
			startTime = (int)Time.time;
			other.gameObject.SetActive (false);
			GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost Intelligent");

			for (int i = 0; i < ghosts.Length; i++) {
				if (ghosts[i].activeSelf)
					ghosts[i].GetComponent<GhostInteligent>().setIsRunningAway();
			}

			ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

			for (int i = 0; i < ghosts.Length; i++) {
				if (ghosts[i].activeSelf)
					ghosts[i].GetComponent<GhostController>().setRunningAway();
			}

		}


	}

	void EndAnimationDeath() 
	{
		animator.SetBool ("isDeath", false);

	}


	private IEnumerator ResumeAfterSeconds(int resumetime) // 3
	{
		Time.timeScale = 0.0001f;
		float pauseEndTime = Time.realtimeSinceStartup + resumetime; // 10 + 4 = 13

		float number3 = Time.realtimeSinceStartup + 1; // 10 + 1 = 11
		float number2 = Time.realtimeSinceStartup + 2; // 10 + 2 = 12
		float number1 = Time.realtimeSinceStartup + 3; // 10 + 3 = 13

		while (Time.realtimeSinceStartup < pauseEndTime) // 10 < 13
		{
			yield return null;
		}

		Time.timeScale = 1;
	}
		
}


