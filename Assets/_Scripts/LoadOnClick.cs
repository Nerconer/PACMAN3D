﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public GameObject instructions;

	public GameObject credits;

	public void LoadScene(int level)
	{
		loadingImage.SetActive(true);
		//bool act = loadingImage.activeSelf();
		SceneManager.LoadScene(level);
	}

	// val = 0 -> instructions (true)
	// val = 1 -> credits	(true)
	// val = 2 -> instructions (false)
	// val = 3 -> credits (false)
	public void activateMenuItem(int val)
	{
		if(val == 0) {
			instructions.SetActive(true);
		} else if(val == 1) {
			credits.SetActive(true);
		} else if(val == 2) {
			instructions.SetActive(false);
		} else if(val == 3) {
			credits.SetActive(false);
		}
	}
}
