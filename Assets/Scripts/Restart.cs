using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

	public TextManager textManager;
	private GameObject gameOverAlert;
	private GameObject youWinAlert;

	void Start() {
		gameOverAlert = GameObject.Find ("GameOverAlert");
		youWinAlert = GameObject.Find ("YouWinAlert");
	}

	void OnMouseDown() {
		textManager.restart ();
		if (gameOverAlert != null) {
			gameOverAlert.SetActive (false);
		}
		if (youWinAlert != null) {
			youWinAlert.SetActive (false);
		}

	}
}
