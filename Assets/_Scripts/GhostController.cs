using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public Transform[] waypoints;
	int current = 0;

	public bool isCollision = false;
	public int equalCoordenate = 2;

	public float lastAngle = 0;

	public float speed = 0.3f;
	public bool isInsideBox = true;


	void FixedUpdate() {
		Vector3 actualPosition = transform.position;
		if (transform.position != waypoints [current].position) {
			Vector3 position = Vector3.MoveTowards (transform.position,
				                   waypoints [current].position,
				                   speed);
			GetComponent<Rigidbody> ().MovePosition (position);
		
		} else {
			current = (current + 1) % waypoints.Length;
			float newAngle = 0;
			Vector3 futurePosition = waypoints[current].position;

			if (futurePosition.x == actualPosition.x) {
				//Nos movemos para arriba o abajo
				if (futurePosition.z > actualPosition.z) 
					//Arriba
					newAngle = 180;
				else
					newAngle = 0;
			} 
			else if (futurePosition.z == actualPosition.z) {
				//Izquierda o derecha
				if (futurePosition.x > actualPosition.x)
					newAngle = -90;
				else
					newAngle = 90;
			}
					
		

			transform.Rotate (new Vector3 (0, -lastAngle, 0));
			transform.Rotate (new Vector3 (0, newAngle, 0));
			lastAngle = newAngle;

		}
			
	}

	void onTriggerEnter(Collider coll) {
		if (coll.name == "Pacman") {
			isCollision = true;
		}
	}
}
