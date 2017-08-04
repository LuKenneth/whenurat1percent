using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPowerModeButton : MonoBehaviour {

	private GameObject lowPowerMode;
	public GameObject theirMessageUpper;
	public GameObject grayBubbleUpper;
	public TextManager textManager;

	// Use this for initialization
	void Start () {
		lowPowerMode = GameObject.Find("LowPowerMode");
	}

	void OnMouseDown() {
		lowPowerMode.SetActive(false);
		theirMessageUpper.SetActive (true);
		grayBubbleUpper.SetActive (true);
		textManager.newReplies ();
		textManager.gameTime = 25.0f;
		textManager.countGameTime = true;
		textManager.onLowPower = true;
		if (textManager.volumeOn) {
			textManager.incoming_sound.Play ();
		}
	}
}
