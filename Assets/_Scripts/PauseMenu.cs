using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public Transform pauseCanvas;
	public Transform guiCanvas;
	public Transform optionsCanvas;
	public Transform confirmCanvas;

	public void quitGame() {
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

	public void restoreOptions() {
		confirmCanvas.gameObject.SetActive(false);
		optionsCanvas.gameObject.SetActive(true);
	}

	public void confirmQ() {
		optionsCanvas.gameObject.SetActive(false);
		confirmCanvas.gameObject.SetActive(true);
	}

	public void backToGame() {
		pauseCanvas.gameObject.SetActive(false);
		guiCanvas.gameObject.SetActive(true);
		Time.timeScale = 1;
	}

	void Start(){
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if(pauseCanvas.gameObject.activeInHierarchy == false) {
				pauseCanvas.gameObject.SetActive(true);
				guiCanvas.gameObject.SetActive(false);
				Time.timeScale = 0;
			} else {
				backToGame();
			}
		}
	}
}
