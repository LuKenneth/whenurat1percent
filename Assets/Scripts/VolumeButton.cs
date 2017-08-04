using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeButton : MonoBehaviour {

	public TextManager textManager;

	void OnMouseDown() {
		textManager.volumeOn = !textManager.volumeOn;
	}
}
