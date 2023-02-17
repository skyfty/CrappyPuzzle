using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLanguageCancel : MonoBehaviour {

	public GameObject languageManager;
	public GameObject canvasLanguage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CancelLanguage () {
		languageManager.GetComponent<LanguageManager> ().NotSet ();
		canvasLanguage.GetComponent<CanvasLanguage> ().CancelDialogConfirm ();
	}
}
