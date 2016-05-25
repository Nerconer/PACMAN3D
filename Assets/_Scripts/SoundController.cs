using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public AudioSource beginingMusic;
	public AudioSource eatingMusic;

	private int elapsed_time;
	private int starting_time;
	private bool starting;

	public bool isEating;

	void Awake() {
		AudioSource[] source = GetComponents<AudioSource>();
		beginingMusic = source[0];
		eatingMusic = source[1];
	}

	// Use this for initialization
	void Start () {
		elapsed_time = 0;
		starting = true;
		starting_time = (int)Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		elapsed_time = (int) Time.time - starting_time;
		if(elapsed_time >= 4 || starting == false) {
			starting = false;
		} else {
			if(!beginingMusic.isPlaying) {
				beginingMusic.Play();
				starting = false;
			}
		}
	}

	public void soundEating(){
		if(!eatingMusic.isPlaying)
			eatingMusic.Play();

	}
}
