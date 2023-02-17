using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTitle : MonoBehaviour {

	public GameObject levelManager;
	public GameObject buttonLevelManager;

	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_SHOW = 1;
	private const int STATE_CONFIRM = 2;

	// Use this for initialization
	void Start () {
		state = STATE_SHOW;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			state = STATE_SHOW;
			break;
		case STATE_SHOW:
			if (Input.GetMouseButtonUp (0))
				state = STATE_CONFIRM;
			break;
		case STATE_CONFIRM:
			break;
		}
	}

	public void Reset () {
		state = STATE_WAIT;
	}

	public bool IsCanToMenu (){
		if (state == STATE_CONFIRM)
			return true;
		return false;
	}

	public bool IsCanToOffice (){
		if (state == STATE_CONFIRM)
			return true;
		return false;
	}
}
