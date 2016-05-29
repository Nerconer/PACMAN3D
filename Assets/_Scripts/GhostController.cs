using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour {

	public Transform[] waypoints;
	Transform[] returnRute;
	public Transform returnPoint;
	int current = 0;

	public bool isCollision = false;
	public int equalCoordenate = 2;

	private bool isFirstPoint = true;

	public float lastAngle = 0;

	public float speed = 0.3f;

	public bool isRunningAway = false;


	void Start () {
		//anim = gameObject.GetComponent<Animator>();
		returnRute = new Transform[waypoints.Length + 1];
		returnRute[0] = returnPoint;
		for (int i = 0; i < waypoints.Length; ++i) {
			returnRute [i + 1] = waypoints[i];
		}

	}

	void FixedUpdate() {

		if (isRunningAway) {
			Vector3 actualPosition = transform.position;
			if (transform.position != returnRute [current].position) {
				Vector3 position = Vector3.MoveTowards (transform.position,
					returnRute [current].position,
					                   speed);
				GetComponent<Rigidbody> ().MovePosition (position);

			} else {
				current = (current - 1);
				if (current < 0) {
					current = 0;
					isRunningAway = false;
					isFirstPoint = true;
				} else {
					float newAngle = 0;
					Vector3 futurePosition = returnRute [current].position;

					if (futurePosition.x == actualPosition.x) {
						//Nos movemos para arriba o abajo
						if (futurePosition.z > actualPosition.z) 
						//Arriba
						newAngle = 180;
						else
							newAngle = 0;
					} else if (futurePosition.z == actualPosition.z) {
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




		} else {


			Vector3 actualPosition = transform.position;
			if (transform.position != waypoints [current].position) {
				Vector3 position = Vector3.MoveTowards (transform.position,
					                  waypoints [current].position,
					                  speed);
				GetComponent<Rigidbody> ().MovePosition (position);
		
			} else {
				current = (current + 1) % waypoints.Length;
				float newAngle = 0;
				Vector3 futurePosition = waypoints [current].position;

				if (futurePosition.x == actualPosition.x) {
					//Nos movemos para arriba o abajo
					if (futurePosition.z > actualPosition.z) 
					//Arriba
					newAngle = 180;
					else
						newAngle = 0;
				} else if (futurePosition.z == actualPosition.z) {
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
			
	}

	void onTriggerEnter(Collider coll) {
		if (coll.name == "Pacman") {
			isCollision = true;
		}
	}

	private int CalculateAngle(Vector3 futurePosition, Vector3 actualPosition) {
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


}
