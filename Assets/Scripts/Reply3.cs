﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reply3 : MonoBehaviour {

	public TextManager textManager;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown() {
		textManager.reply(3);
	}
}
