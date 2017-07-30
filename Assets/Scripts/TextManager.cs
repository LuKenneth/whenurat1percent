using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour {

	public GameObject blueBubble;
	public GameObject grayBubble;
	public GameObject theirMessage;
	public GameObject playerMessage;
	public Timer time;
	public GameObject powerDown;
	private float gameTime = 20.0f;

	// Use this for initialization
	void Start () {
		grayBubble.SetActive(false);
		TheirMessage tm = new TheirMessage("Heyyy");
		theirMessage.GetComponent<TextMesh>().text = tm.message;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime -= Time.deltaTime;
		if(gameTime <= 0.0f) {
			shutDown();
		}
	}

	void shutDown() {
		powerDown.SetActive(true);
	}

	public void reply(int num) {
		
	}
}
