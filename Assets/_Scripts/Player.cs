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

	bool wakawaka;

	//public int score;
	public int lives;

	public bool move;

	GameObject map;
	SoundController sc;


	private Animator animator;
	// Use this for initialization
	void Start () {
		//anim = gameObject.GetComponent<Animator>();
		lives = 3;
		move = false;
		map = GameObject.Find("Map");
		sc = map.GetComponent<SoundController>();
		animator = GetComponent<Animator>();
		//moveSpeed = 50;
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
		//anim.speed = x != 0 || y != 0 ? 1 : 0;
		//Debug.Log("level: " + GameController.level);

		//if (anim.speed != 0) {
		if(move) {
			x = Input.GetAxisRaw("Horizontal");
			y = Input.GetAxisRaw("Vertical");

			if(GameController.level == 1) {
				moveLevel1();
			} else if(GameController.level == 2) {
				moveLevel2();
			}
		//}
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Pick Up"))
		{
			GameController.score += 10;
			other.gameObject.SetActive(false);

			sc.soundEating();
		}
	}
		
}


