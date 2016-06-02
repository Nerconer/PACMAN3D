using UnityEngine;
using System.Collections;

public class GhostWaypointController : MonoBehaviour {

	public Transform[] waypoints;
	int current = 0;

	public bool isCollision = false;

	public float speed = 0.3f;

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
		if (transform.position != waypoints [current].position) {
			Vector3 position = Vector3.MoveTowards (transform.position,
				waypoints [current].position,
				speed);
			GetComponent<Rigidbody> ().MovePosition (position);

		}
		else {



			current = (current + 1) % waypoints.Length;

			setOrientation();
		}

	}

	void onTriggerEnter(Collider coll) {
		if (coll.name == "Pacman") {
			isCollision = true;
		}
	}
}