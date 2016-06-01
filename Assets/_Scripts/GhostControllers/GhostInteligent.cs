using UnityEngine;
using System.Collections;

public class GhostInteligent : MonoBehaviour {

	// Use this for initialization
	NavMeshAgent agent;
	public Transform returnPosition;

	public Quaternion initialRotation;
	public Vector3 initialPosition;

	bool isScaredActive;
	GameObject ScaredGhost;
	GameObject NormalGhost;

	private int timeBlink = 0;

	public bool isDeath = false;

	private Animator scaredAnimator;


	public bool isRunningAway = false;

	public float remainningDistance;

	public float pauseDelay = 0;

	Color lastColor;
	Color basicColor;


	GameObject Pacman;
	void Start () {
		ScaredGhost = this.transform.GetChild (1).gameObject;
		NormalGhost = this.transform.GetChild (0).gameObject;
		scaredAnimator = ScaredGhost.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent> ();
		Pacman = GameObject.Find("Pacman");
		pauseDelay = 0;
		isRunningAway = false;
		isScaredActive = false;
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		basicColor = ScaredGhost.transform.FindChild ("Cylinder").GetComponent<Renderer> ().material.color;
		lastColor = Color.black;
		timeBlink = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Player.move) {
			if (pauseDelay == 0 && !isDeath) {
				if (!isRunningAway)
					agent.destination = Pacman.transform.position;

				if (isRunningAway && CompareVector (transform.position, returnPosition.position) && CompareVector (agent.destination, returnPosition.position)) {

					toNormalState ();
				}
			
			} else {
				--pauseDelay;
				if (pauseDelay == 0) {
					Time.timeScale = 1;
					agent.Resume ();
					if (isDeath) {
						isDeath = false;
					}

				}

			}
		}

	}


	public void setIsRunningAway() 
	{

		isRunningAway = true;
		isScaredActive = true;
		NormalGhost.SetActive (false);
		ScaredGhost.SetActive (true);
		scaredAnimator.SetBool ("isScared", true);
		agent.destination = returnPosition.position;

		//Speed, angular and aceleration modifications
		agent.speed = 15;
		agent.angularSpeed = 180;
		agent.acceleration = 50;

	}

	private bool CompareVector(Vector3 a, Vector3 b) {
		float diff = Mathf.Abs (a.sqrMagnitude - b.sqrMagnitude);
		print (diff);
		if (diff < 10)
			return true;
		return false;

	}


	public void returnToInitialPosition() {
		
		agent.Stop ();
		agent.Warp (initialPosition);
		transform.rotation = initialRotation;
		Time.timeScale = 0;
		pauseDelay = 50;
	}


	public bool getIsRunningAway() {
		return isRunningAway;
	}

	public void isDeathTime() {
		
		ScaredGhost.transform.FindChild("Cylinder").gameObject.SetActive(false);
		ScaredGhost.transform.FindChild ("Plane").gameObject.SetActive (false);
		//Speed, angular and aceleration modifications
		agent.speed = 80;
		agent.angularSpeed = 360;
		agent.acceleration = 300;
		isDeath = true;
		Time.timeScale = 0;
		pauseDelay = 50;
		agent.Stop ();
		isRunningAway = true;

	}

	public void toNormalState() {

		scaredAnimator.SetBool ("isScared", false);
		ScaredGhost.transform.FindChild ("Cylinder").gameObject.GetComponent<Renderer> ().material.color = basicColor;

		ScaredGhost.SetActive (false);

		NormalGhost.SetActive (true);

		isRunningAway = false;

		//Speed, angular and aceleration modifications
		agent.speed = 60;
		agent.angularSpeed = 360;
		agent.acceleration = 200;


		if (!ScaredGhost.transform.FindChild ("Cylinder").gameObject.activeSelf) {
			ScaredGhost.transform.FindChild ("Cylinder").gameObject.SetActive (true);
			ScaredGhost.transform.FindChild ("Plane").gameObject.SetActive (true);
		}

		timeBlink = 0;
		lastColor = Color.black;
	}


	public void Blink() {
		if (timeBlink == 0) {
			Renderer renderer = ScaredGhost.transform.FindChild ("Cylinder").gameObject.GetComponent<Renderer> ();
			if (lastColor == Color.black) {
				lastColor = renderer.material.color;
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
