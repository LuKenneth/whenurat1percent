using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reply : MonoBehaviour {

	public TextManager textManager;
	public PlayerMessage pm;

	// Use this for initialization
	void Start () {
		
	}

	void OnMouseDown() {
		textManager.reply(this);
	}
}
