using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public Transform[] waypoints;
	Transform[] returnRute;
	public Vector3 initialPosition;
	int current = 1;

	int startTime;

	public float pauseDelay = 0;

	public bool isCollision = false;
	public int equalCoordenate = 2;

	public bool isFirstPoint = true;

	public float lastAngle = 0;

	public float speed = 0.3f;

	public bool isDeath = false;

	public bool isRunningAway = false;

	private Animator scaredAnimator;

	GameObject ScaredGhost;

	GameObject NormalGhost;
	NavMeshAgent agent;

	void Start() {
		ScaredGhost = this.transform.GetChild (1).gameObject;
		NormalGhost = this.transform.GetChild (0).gameObject;
		scaredAnimator = ScaredGhost.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = waypoints [current].position;
		initialPosition = transform.position;
	}


	void Update() {
		
		if ( pauseDelay == 0 && !isDeath) {

			Vector3 actualPosition = transform.position;
			float dist = agent.remainingDistance;
			if (dist <= 5) {
				if (isRunningAway) {
					current = (current - 1);
					if (current < 0) {
						current = 0;
						isRunningAway = false;
						scaredAnimator.SetBool ("isScared", false);
						ScaredGhost.SetActive (false);
						NormalGhost.SetActive (true);
						//Speed, angular and aceleration modifications
						//Speed, angular and aceleration modifications
						agent.speed = 60;
						agent.angularSpeed = 360;
						agent.acceleration = 200;
					}
				} else {
					current = (current + 1) % waypoints.Length;
					if (current == 0)
						++current;


				}

				agent.destination = waypoints [current].position;
				if (!ScaredGhost.transform.FindChild ("Cylinder").gameObject.activeSelf) {
					ScaredGhost.transform.FindChild ("Cylinder").gameObject.SetActive (true);
					ScaredGhost.transform.FindChild ("Plane").gameObject.SetActive (false);
				}
				
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


	public void setRunningAway() {
		isRunningAway = true;
		isFirstPoint = true;
		if (current >= 0) --current;
		agent.destination = waypoints [current].position;
		NormalGhost.SetActive (false);
		ScaredGhost.SetActive (true);
		scaredAnimator.SetBool ("isScared", true);
		//Speed, angular and aceleration modifications
		agent.speed = 15;
		agent.angularSpeed = 180;
		agent.acceleration = 50;
		
	}

	public void returnToInitialPosition() {
		agent.Stop ();
		agent.Warp (initialPosition);
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
		agent.acceleration = 200;
		isDeath = true;
		Time.timeScale = 0;
		pauseDelay = 50;
		agent.Stop ();
		agent.destination = waypoints[0].position;

	}

}
