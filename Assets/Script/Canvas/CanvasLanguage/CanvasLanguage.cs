using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLanguage : MonoBehaviour {

	public GameObject dialogConform;

	private int state;
	private const int STATE_SHOW = 0;
	private const int STATE_CONFIRM = 1;
	private const int STATE_SAVED = 2;

	// Use this for initialization
	void Start () {
		state = STATE_SHOW;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowDialogConfirm () {
		if (dialogConform.activeSelf == false)
			dialogConform.SetActive (true);
		state = STATE_CONFIRM;
	}

	public void ConfirmDialogConfirm () {
		if (dialogConform.activeSelf == true)
			dialogConform.SetActive (false);
		state = STATE_SAVED;
	}

	public void CancelDialogConfirm () {
		if (dialogConform.activeSelf == true)
			dialogConform.SetActive (false);
		state = STATE_SHOW;
	}

	//for canvasManager to see the state
	public bool IsDialogConfirm () {
		if (state == STATE_CONFIRM)
			return true;
		return false;
	}

	public bool IsConfirmDialogConfirm () {
		if (state == STATE_SAVED)
			return true;
		return false;
	}

	public bool IsCancelDialogConfirm () {
		if (state == STATE_SHOW)
			return true;
		return false;
	}
}
