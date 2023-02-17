using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFinishReturn : MonoBehaviour {

	public GameObject gameManager;
	public GameObject soundManager;
	public GameObject imageBack;
	public GameObject textFinish;
	public GameObject buttonFinishReturn;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void HideFinish () {
		if (!gameManager.GetComponent<GameManager> ().IsMainStateMenuShow())
			return;
		imageBack.SetActive (false);
		textFinish.SetActive (false);
		buttonFinishReturn.SetActive (false);
		soundManager.GetComponent<SoundManager> ().PlayClick ();
	}
}
