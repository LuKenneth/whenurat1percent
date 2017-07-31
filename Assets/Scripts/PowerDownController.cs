using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDownController : MonoBehaviour {

	public GameObject off;
	public GameObject needCharge;
	public Timer timer;

	void Start () {
		timer.RunAfter(shutdown, 2.0f);
	}

	void shutdown() {
		off.SetActive(true);
		timer.RunAfter (needCharging, 1.0f);
	}

	void needCharging() {
		needCharge.SetActive (true);
	}

}
