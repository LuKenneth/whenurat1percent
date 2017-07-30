using System;
using UnityEngine;
using UnityEditor;

public class Timer {

	public delegate void TimerCallback();
	float interval = 0.0f;


	public void RunAfter(TimerCallback callback, float time) {
		if(interval >= time) {
			callback;
		}
	}

	public void Update() {
		interval += 1.0f;
	}

}


