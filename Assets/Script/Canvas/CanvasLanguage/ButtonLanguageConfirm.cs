using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLanguageConfirm : MonoBehaviour {

	public GameObject languageManager;
	public GameObject canvasLanguage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ConfirmLanguage () {
		languageManager.GetComponent<LanguageManager> ().SaveLanguage ();
		canvasLanguage.GetComponent<CanvasLanguage> ().ConfirmDialogConfirm ();
	}
}
