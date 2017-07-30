using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDownController : MonoBehaviour {

	public GameObject off;
	public Timer timer;

	void Start () {
		timer.RunAfter(shutdown, 3.0f);
	}

	void shutdown() {
		off.SetActive(true);
	}

}
