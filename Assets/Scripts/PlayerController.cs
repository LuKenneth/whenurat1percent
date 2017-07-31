using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private GameObject player;
	public GameObject showMessage;
	public GameObject hideMessage;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.visible = !Cursor.visible;
			showMessage.SetActive(!Cursor.visible);
			hideMessage.SetActive(Cursor.visible);
		}
	}

}
