using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject pacman;
	public static Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - pacman.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = pacman.transform.position + offset;
	}
}
