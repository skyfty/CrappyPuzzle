using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour {

	public int number;
	public int rowCount;
	public int columnCount;
//	public GameObject gameManager;
//	public GameObject canvasManager;
	public GameObject buttonLevelManager;
	public GameObject canvasMenu;
	public GameObject mark;
	public GameObject lockset;
	public Sprite buttonImageLock;
	public Sprite buttonImageCurrent;
	public Sprite buttonImageFinish;

	private int state;
	private const int STATE_LOCK = 0;
	private const int STATE_CURRENT = 1;
	private const int STATE_FINISH = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayLevel(){
		if (number > buttonLevelManager.GetComponent<ButtonLevelManager> ().finishLevel + 1)
			return;
		print("name "+buttonLevelManager.transform.parent.parent.name);
		if (buttonLevelManager.transform.parent.parent.name == "Canvas")
			buttonLevelManager.transform.parent.parent.GetComponent<CanvasManager> ().gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayClick ();
		canvasMenu.GetComponent<CanvasMenu> ().SetLevelNumber (number);
	}

	public void SetLock () {
		mark.transform.gameObject.SetActive (false);
		lockset.transform.gameObject.SetActive (true);
		this.transform.GetComponent<Image> ().sprite = buttonImageLock;
	}

	public void SetCurrent () {
		mark.transform.gameObject.SetActive (false);
		lockset.transform.gameObject.SetActive (false);
		this.transform.GetComponent<Image> ().sprite = buttonImageCurrent;
	}

	public void SetFinish () {
		mark.transform.gameObject.SetActive (true);
		lockset.transform.gameObject.SetActive (false);
		this.transform.GetComponent<Image> ().sprite = buttonImageFinish;
	}
}
