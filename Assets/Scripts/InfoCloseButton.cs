﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCloseButton : MonoBehaviour {

	public GameObject infoAlert;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown() {
		infoAlert.SetActive (false);
	}
}
