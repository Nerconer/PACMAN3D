using UnityEngine;
using System.Collections;

public class GhostWaypointController : MonoBehaviour {

	public Transform[] waypoints;
	int current = 0;

	public bool isCollision = false;

	public float speed = 0.3f;

	void FixedUpdate() {
		if (transform.position != waypoints [current].position) {
			Vector3 position = Vector3.MoveTowards (transform.position,
				waypoints [current].position,
				speed);
			GetComponent<Rigidbody> ().MovePosition (position);

		}
		else current = (current + 1) % waypoints.Length;

	}

	void onTriggerEnter(Collider coll) {
		if (coll.name == "Pacman") {
			isCollision = true;
		}
	}
}