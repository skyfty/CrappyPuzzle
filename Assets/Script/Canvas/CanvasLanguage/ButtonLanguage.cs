using GameFramework.Localization;
using System;
using UnityEngine;

public class ButtonLanguage : MonoBehaviour {
	
	public GameObject languageManager;
	public GameObject canvasLanguage;
    public GameObject gameManager;
	public string language;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLanguage (){
        languageManager.GetComponent<LanguageManager>().SetLanguage((Language)Enum.Parse(typeof(Language), language));
        canvasLanguage.GetComponent<CanvasLanguage>().ShowDialogConfirm();
    }
}
