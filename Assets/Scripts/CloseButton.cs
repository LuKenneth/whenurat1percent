using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {

	private GameObject lowPowerMode;
	public Timer timer;

	// Use this for initialization
	void Start () {
		lowPowerMode = GameObject.Find("LowPowerMode");
	}

	void OnMouseDown() {
		lowPowerMode.SetActive(false);
	}

}
