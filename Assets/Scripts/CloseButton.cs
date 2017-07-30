using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {

	private GameObject lowPowerMode;
	private GameObject powerDown;
	private GameObject off;

	// Use this for initialization
	void Start () {
		lowPowerMode = GameObject.Find("LowPowerMode");
		powerDown = GameObject.Find("power-down");
		off = GameObject.Find("Off");
	}

	void OnMouseDown() {
		lowPowerMode.SetActive(false);
		powerDown.SetActive(true);
		Timer timer = new Timer();
		timer.RunAfter(turnOff, 3.0f);
	}

	void turnOff() {
		off.SetActive(true);
	}
}
