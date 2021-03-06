﻿using System;
using UnityEngine;

public class Timer : MonoBehaviour {

	public delegate void TimerCallback();

	public TimerCallback callback;
	float time;
	public bool run = false;

	public void RunAfter(TimerCallback callback, float time) {
		this.callback = callback;
		this.time = time;
		run = true;
	}

	void Update() {

		if(run) {
			time -= Time.deltaTime;
			if(time <= 0.0f) {
				callback();
			}
		}

	}

}


