using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public Transform[] waypoints;
	Transform[] returnRute;
	public Transform returnPoint;
	int current = 1;

	public bool isCollision = false;
	public int equalCoordenate = 2;

	public bool isFirstPoint = true;

	public float lastAngle = 0;

	public float speed = 0.3f;

	public bool isRunningAway = false;



	void FixedUpdate() {


		Vector3 actualPosition = transform.position;
		if (transform.position != waypoints [current].position) {
			Vector3 position = Vector3.MoveTowards (transform.position,
				waypoints [current].position,
				                   speed);
			GetComponent<Rigidbody> ().MovePosition (position);
			if (isFirstPoint) {
				float newAngle = CalculateAngle (waypoints [current].position, actualPosition);
				transform.Rotate (new Vector3 (0, -lastAngle, 0));
				transform.Rotate (new Vector3 (0, newAngle, 0));
				lastAngle = newAngle;
				isFirstPoint = false;

			}
		}
		 else {
			if (isRunningAway) {
				current = (current - 1);
				if (current < 0) {
					current = 0;
					isRunningAway = false;
					isFirstPoint = true;
				}
			} else {
				current = (current + 1) % waypoints.Length;
				if (current == 0)
					++current;
			}

			Vector3 futurePosition = waypoints [current].position;
			float newAngle = CalculateAngle(futurePosition, actualPosition);
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

	private float CalculateAngle(Vector3 futurePosition, Vector3 actualPosition) {
		if (futurePosition.x == actualPosition.x) {
			//Nos movemos para arriba o abajo
			if (futurePosition.z > actualPosition.z) 
				//Arriba
				return 180;
			else
				return 0;
		} else if (futurePosition.z == actualPosition.z) {
			//Izquierda o derecha
			if (futurePosition.x > actualPosition.x)
				return -90;
			else
				return 90;
		}
		return 0;
	}
	
	public void setRunningAway() {
		isRunningAway = true;
		isFirstPoint = true;
		--current;
		
	}


}
