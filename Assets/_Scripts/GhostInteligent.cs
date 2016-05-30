using UnityEngine;
using System.Collections;

public class GhostInteligent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject castle = GameObject.Find("Pacman");
		if (castle)
			GetComponent<NavMeshAgent>().destination = castle.transform.position;
		transform.Rotate (new Vector3 (0, 180, 0));
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
