using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Camera camera;

	[SerializeField]
	private TextManager textManager;

	void Start () 
	{
	    float targetaspect = 9.0f / 16.0f;

	    // determine the game window's current aspect ratio
	    float windowaspect = (float)Screen.width / (float)Screen.height;

	    // current viewport height should be scaled by this amount
	    float scaleheight = windowaspect / targetaspect;

	    // obtain camera component so we can modify its viewport
	    camera = GetComponent<Camera>();

	    // if scaled height is less than current height, add letterbox
	    if (scaleheight < 1.0f)
	    {  
	        Rect rect = camera.rect;

	        rect.width = 1.0f;
	        rect.height = scaleheight;
	        rect.x = 0;
	        rect.y = (1.0f - scaleheight) / 2.0f;
	        
	        camera.rect = rect;
	    }
	    else // add pillarbox
	    {
	        float scalewidth = 1.0f / scaleheight;

	        Rect rect = camera.rect;

	        rect.width = scalewidth;
	        rect.height = 1.0f;
	        rect.x = (1.0f - scalewidth) / 2.0f;
	        rect.y = 0;

	        camera.rect = rect;
	    }
	}

	void Update() {
		if (textManager.countGameTime) {
			float time = textManager.onLowPower ? 25f : 20f;
			Color bg = new Color ((((time - textManager.gameTime) / time) / 255f) * 255f, 0, 0, 0);
			camera.backgroundColor = bg;
		} else if (textManager.gameTime < 0) {
			camera.backgroundColor = Color.black;
		}
		if (textManager.isGameOver) {
			camera.backgroundColor = Color.red;
		}
		if (textManager.victory) {
			camera.backgroundColor = Color.green;
		}
	}
}
