using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPowerModeButton : MonoBehaviour {

	private GameObject lowPowerMode;

	// Use this for initialization
	void Start () {
		lowPowerMode = GameObject.Find("LowPowerMode");
	}

	void OnMouseDown() {
		lowPowerMode.SetActive(false);
	}
}
