using UnityEngine;
using System.Collections;

public class GhostInteligent : MonoBehaviour {

	// Use this for initialization
	NavMeshAgent agent;
	GameObject Pacman;
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		Pacman = GameObject.Find("Pacman");
	
	}
	
	// Update is called once per frame
	void Update () {

		agent.destination = Pacman.transform.position;
	}
}
