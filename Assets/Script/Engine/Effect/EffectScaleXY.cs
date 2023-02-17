using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScaleXY : MonoBehaviour {

	private float speed = 0.05f;
	private float tempScaleX;
	private float tempScaleY;

	private int state;
	private const int STATE_RANDOM = 0;
	private const int STATE_SCALE = 1;
	private const int STATE_STOP = 2;
	private const int STATE_KEEP = 3;

	void Awake () {
		//can not work from setactive(true) to setactive(false) to setactive(true).
	}

	void OnEnable() {
		state = STATE_RANDOM;
		this.GetComponent<RectTransform> ().localScale = new Vector3 (Random.Range (0.0f, 2.0f), Random.Range (0.0f, 2.0f), 1.0f);
		state = STATE_SCALE;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_RANDOM:
			break;
		case STATE_SCALE:
			tempScaleX = this.GetComponent<RectTransform> ().localScale.x;
			if (tempScaleX > 1.0f) {
				tempScaleX -= speed;
				if (tempScaleX < 1.0f)
					tempScaleX = 1.0f;
			} else {
				tempScaleX += speed;
				if (tempScaleX > 1.0f)
					tempScaleX = 1.0f;
			}
			tempScaleY = this.GetComponent<RectTransform> ().localScale.y;
			if (tempScaleY > 1.0f) {
				tempScaleY -= speed;
				if (tempScaleY < 1.0f)
					tempScaleY = 1.0f;
			} else {
				tempScaleY += speed;
				if (tempScaleY > 1.0f)
					tempScaleY = 1.0f;
			}
			this.GetComponent<RectTransform> ().localScale = new Vector3 (tempScaleX, tempScaleY, 1.0f);
			if (tempScaleX == 1.0f && tempScaleY == 1.0f)
				state = STATE_STOP;
			break;
		case STATE_STOP:
			this.GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			state = STATE_KEEP;
			break;
		case STATE_KEEP:
			break;
		}
	}
}
