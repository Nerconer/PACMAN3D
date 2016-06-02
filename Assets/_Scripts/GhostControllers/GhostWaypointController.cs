using UnityEngine;
using System.Collections;

public class GhostWaypointController : MonoBehaviour {

	public Transform[] waypoints;

	public Vector3 initialPosition;
	int current = 1;

	int startTime;

	public float pauseDelay = 0;


	public bool isFirstPoint = true;

	public float lastAngle = 0;

	public float speed = 0.3f;

	public bool isDeath = false;

	public bool isRunningAway = false;

	private Animator scaredAnimator;

	GameObject ScaredGhost;

	GameObject NormalGhost;

	Color lastColor;
	Color basicColor;

	Quaternion initialRotation;

	private int timeBlink;


	void Start() {
		ScaredGhost = this.transform.GetChild (1).gameObject;
		NormalGhost = this.transform.GetChild (0).gameObject;
		scaredAnimator = ScaredGhost.GetComponent<Animator>();
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		basicColor = ScaredGhost.transform.FindChild ("Cylinder").GetComponent<Renderer> ().material.color;
		lastColor = Color.black;
		timeBlink = 0;
	}


	void setOrientation() 
	{
		float cx, cy, cz;
		float nx, ny, nz;

		Vector3 currentV, nextV, rotation;


		nextV = waypoints[current].position;
		nx = nextV.x;
		ny = nextV.y;
		nz = nextV.z;


		if(transform.name == "ghost20" || transform.name == "ghost21") {
			if((current - 1) == -1 && transform.name == "ghost20") {
				currentV = waypoints[13].position;
			} else if((current - 1) == -1 && transform.name == "ghost21") {
				currentV = waypoints[17].position;
			} else {
				currentV = waypoints[current-1].position;
			}

			cx = currentV.x;
			cy = currentV.y;

			if(ny > cy) {
				rotation = new Vector3(90.0f, 180.0f, 0.0f);
				transform.eulerAngles = rotation;
			} else if(ny < cy) {
				rotation = new Vector3(-90.0f, 270.0f, 90.0f);
				transform.eulerAngles = rotation;
			} else if(nx > cx) {
				rotation = new Vector3(0.0f, 270.0f, 90.0f);
				transform.eulerAngles = rotation;
			} else if(nx < cx) {
				rotation = new Vector3(180.0f, 270.0f, 90.0f);
				transform.eulerAngles = rotation;
			}
		} else if(transform.name == "ghost30" || transform.name == "ghost31") {
			if((current - 1) == -1 && transform.name == "ghost30") {
				currentV = waypoints[17].position;
			} else if((current - 1) == -1 && transform.name == "ghost31") {
				currentV = waypoints[16].position;
			} else {
				currentV = waypoints[current-1].position;
			}
			cz = currentV.z;
			cy = currentV.y;

			if(ny > cy) {
				rotation = new Vector3(90.0f, 180.0f, 270.0f);
				transform.eulerAngles = rotation;
			} else if(ny < cy) {
				rotation = new Vector3(270.0f, 180.0f, 270.0f);
				transform.eulerAngles = rotation;
			} else if(nz > cz) {
				rotation = new Vector3(0.0f, 180.0f, 270.0f);
				transform.eulerAngles = rotation;
			} else if(nz < cz) {
				rotation = new Vector3(180.0f, 180.0f, 270.0f);
				transform.eulerAngles = rotation;
			}


		} else if(transform.name == "ghost40" || transform.name == "ghost41") {
			if((current - 1) == -1 && transform.name == "ghost40") {
				currentV = waypoints[14].position;
			} else if((current - 1) == -1 && transform.name == "ghost41") {
				currentV = waypoints[7].position;
			} else {
				currentV = waypoints[current-1].position;
			}
			cz = currentV.z;
			cy = currentV.y;

			if(ny > cy) {
				rotation = new Vector3(90.0f, 180.0f, 90.0f);
				transform.eulerAngles = rotation;
			} else if(ny < cy) {
				rotation = new Vector3(270.0f, 180.0f, 90.0f);
				transform.eulerAngles = rotation;
			} else if(nz > cz) {
				rotation = new Vector3(0.0f, 180.0f, 90.0f);
				transform.eulerAngles = rotation;
			} else if(nz < cz) {
				rotation = new Vector3(180.0f, 180.0f, 90.0f);
				transform.eulerAngles = rotation;
			}


		} else if(transform.name == "ghost50" || transform.name == "ghost51") {
			if((current -1) == -1 && transform.name == "ghost50" ) {	//size
				currentV = waypoints[15].position;
			} else if((current -1) == -1 && transform.name == "ghost51") {
				currentV = waypoints[12].position;
			} else {
				currentV = waypoints[current-1].position;
			}
			cx = currentV.x;
			cy = currentV.y;

			if(ny > cy) {
				rotation = new Vector3(90.0f, 0.0f, 0.0f);
				transform.eulerAngles = rotation;
			} else if(ny < cy) {
				rotation = new Vector3(-90.0f, 0.0f, 180.0f);
				transform.eulerAngles = rotation;
			} else if(nx > cx) {
				rotation = new Vector3(0.0f, 270.0f, 270.0f);
				transform.eulerAngles = rotation;
			} else if(nx < cx) {
				rotation = new Vector3(0.0f, 90.0f, 90.0f);
				transform.eulerAngles = rotation;
			}

		}
	}

	void FixedUpdate() {
		if (Player.move && !Player.isDeath) {
			if (pauseDelay == 0) {
				if (transform.position != waypoints [current].position) {
					Vector3 position = Vector3.MoveTowards (transform.position,
						                   waypoints [current].position,
						                   speed);
					GetComponent<Rigidbody> ().MovePosition (position);

					if (isFirstPoint && isRunningAway) {
						setOrientation ();
						isFirstPoint = false;
					}
						

				} else {

					if (isRunningAway) {

						if (current == 0) {
							isRunningAway = false;
							toNormalState ();
						}
						else {
							--current;
						}
					}
					else current = (current + 1) % waypoints.Length;

					setOrientation ();
				}
			} else {
				--pauseDelay;
				if (pauseDelay == 0) {
					Time.timeScale = 1;
				}
			}
		} 
	}

	//Pacman death
	public void returnToInitialPosition() {
		transform.position = initialPosition;
		transform.rotation = initialRotation;
	}

	public bool getIsRunningAway() {
		return isRunningAway;
	}

	public void setIsRunningAway() 
	{
		isRunningAway = true;
		if (current > 0)
			--current;
		NormalGhost.SetActive (false);
		ScaredGhost.SetActive (true);
		scaredAnimator.SetBool ("isScared", true);


	}

	//Eyes ony mode
	public void isDeathTime() {

		ScaredGhost.transform.FindChild("Cylinder").gameObject.SetActive(false);
		ScaredGhost.transform.FindChild ("Plane").gameObject.SetActive (false);
		isDeath = true;
		Time.timeScale = 0;
		pauseDelay = 10;


	}

	public void toNormalState() {

		ScaredGhost.transform.FindChild ("Cylinder").gameObject.GetComponent<Renderer> ().material.color = basicColor;
		isRunningAway = false;
		scaredAnimator.SetBool ("isScared", false);
		ScaredGhost.SetActive (false);
		NormalGhost.SetActive (true);

		if (!ScaredGhost.transform.FindChild ("Cylinder").gameObject.activeSelf) {
			ScaredGhost.transform.FindChild ("Cylinder").gameObject.SetActive (true);
			ScaredGhost.transform.FindChild ("Plane").gameObject.SetActive (false);
		}

		timeBlink = 0;
		isDeath = false;
		lastColor = Color.black;
		isFirstPoint = false;

	}



	public void Blink() {
		if (timeBlink == 0) {
			Renderer renderer = ScaredGhost.transform.FindChild ("Cylinder").gameObject.GetComponent<Renderer> ();
			if (lastColor == Color.black) {
				lastColor = renderer.material.color;
				ScaredGhost.transform.FindChild ("Cylinder").gameObject.GetComponent<Renderer> ().material.color = basicColor;
			} else {
				if (lastColor == basicColor) {
					renderer.material.color = Color.white;
				} else {
					renderer.material.color = basicColor;
				}
				lastColor = renderer.material.color;
			}
			timeBlink = 20;
		} else
			--timeBlink;



	}
}