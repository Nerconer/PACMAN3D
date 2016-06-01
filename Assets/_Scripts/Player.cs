using UnityEngine;
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


	public Quaternion initialRotation;
	public Vector3 initialPosition;


	int startTime;

	private float pauseDelay = 0;

	public string name;
	static bool isDeath;

	//public int score;
	public int lives;

	public static bool move;

	GameObject map;
	SoundController sc;

	bool isPowerUp = false;

	private Animator animator;
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
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (move && pauseDelay == 0) {
			x = Input.GetAxisRaw ("Horizontal");
			y = Input.GetAxisRaw ("Vertical");

			if (GameController.level == 1) {
				moveLevel1 ();
			} else if (GameController.level == 2) {
				moveLevel2 ();
			}
		} else if (pauseDelay > 0) {
			--pauseDelay;
			if (pauseDelay == 0) {
				isDeath = false;
				transform.position = initialPosition;
				transform.rotation = initialRotation;
			}

		}


		if ((int)Time.time >= startTime + 15 && isPowerUp) {

			isPowerUp = false;

			GameObject[] ghosts = GameObject.FindGameObjectsWithTag ("Ghost Intelligent");

			for (int i = 0; i < ghosts.Length; ++i) {
				if (ghosts [i].activeSelf &&  !ghosts[i].GetComponent<GhostInteligent>().isDeath)
					ghosts [i].GetComponent<GhostInteligent> ().toNormalState ();
			}

			ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

			for (int i = 0; i < ghosts.Length; ++i) {
				if (ghosts [i].activeSelf && !ghosts[i].GetComponent<GhostController>().isDeath )
					ghosts [i].GetComponent<GhostController> ().toNormalState ();
			}


		} else if (isPowerUp && (int)Time.time >= startTime + 10) {
				GameObject[] ghosts = GameObject.FindGameObjectsWithTag ("Ghost Intelligent");

				for (int i = 0; i < ghosts.Length; ++i) {
				if (ghosts [i].activeSelf &&  !ghosts[i].GetComponent<GhostInteligent>().isDeath)
						ghosts [i].GetComponent<GhostInteligent> ().Blink ();
				}

				ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

				for (int i = 0; i < ghosts.Length; ++i) {
				if (ghosts [i].activeSelf  && !ghosts[i].GetComponent<GhostController>().isDeath)
						ghosts [i].GetComponent<GhostController> ().Blink ();
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

			sc.soundEating();
		}

		else if (other.gameObject.CompareTag ("Ghost") && !isDeath)
		{
			GhostController ghostController = other.gameObject.GetComponent<GhostController> ();

			if (!ghostController.isDeath) {

				if (ghostController.getIsRunningAway ()) {
					animator.SetBool ("wakawaka", false);
					ghostController.isDeathTime ();

				}
				else {
					animator.SetBool ("wakawaka", false);
					ghostController.returnToInitialPosition ();
					animator.SetBool ("isDeath", true);
					pauseDelay = 150;
					isDeath = true;
					initialRotation = transform.rotation;


				}
					
			}
		}

		else if (other.gameObject.CompareTag ("Ghost Intelligent") && !isDeath) {

			GhostInteligent ghostIntelligent = other.gameObject.GetComponent<GhostInteligent> ();
			if (!ghostIntelligent.isDeath) {

				if (ghostIntelligent.getIsRunningAway ()) {
					animator.SetBool ("wakawaka", false);
					ghostIntelligent.isDeathTime ();

				}
				else {
					animator.SetBool ("wakawaka", false);
					ghostIntelligent.returnToInitialPosition ();
					animator.SetBool ("isDeath", true);
					pauseDelay = 150;
					isDeath = true;
					initialRotation = transform.rotation;

				}
			}


		}

		else if (other.gameObject.CompareTag ("Power Up")) 
		{
			isPowerUp = true;
			startTime = (int)Time.time;
			other.gameObject.SetActive (false);
			GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost Intelligent");

			for (int i = 0; i < ghosts.Length; i++) {
				if (ghosts [i].activeSelf)
					ghosts [i].GetComponent<GhostInteligent> ().setIsRunningAway ();
			}

			ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

			for (int i = 0; i < ghosts.Length; i++) {
				if (ghosts [i].activeSelf)
					ghosts [i].GetComponent<GhostController> ().setRunningAway ();
			}

		}


	}

	void EndAnimationDeath() {

		animator.SetBool ("isDeath", false);

	}
		
		
}


