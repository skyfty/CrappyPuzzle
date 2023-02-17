using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour {

	public GameObject gameManager;
	public GameObject levelManager;
	public GameObject buttonLevelManager;
	public GameObject quantityMoney;

	public bool isFinish;
	public bool isFinishShow;
	public GameObject imageBack;
	public GameObject textFinish;
	public GameObject buttonFinishReturn;

	private int state;
	private const int STATE_SHOW = 0;
	private const int STATE_LEVEL = 1;

	// Use this for initialization
	void Start () {
		state = STATE_SHOW;
		isFinishShow = false;
	}

	void OnEnable() {
		if (!isFinishShow) {
			if (levelManager.GetComponent<LevelManager> ().GetFinishLevelNumber () == buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length - 1) {
				imageBack.SetActive (true);
				textFinish.SetActive (true);
				buttonFinishReturn.SetActive (true);
				isFinishShow = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_SHOW:
			//quantityMoney.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().player.GetComponent<Player> ().GetData (Player.DataType.Money));
//			if (!isFinishShow) {
//				if (levelManager.GetComponent<LevelManager> ().GetFinishLevelNumber () == buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length - 1) {
//					imageBack.SetActive (true);
//					textFinish.SetActive (true);
//					buttonFinishReturn.SetActive (true);
//					isFinishShow = true;
//				}
//			}
			break;
		case STATE_LEVEL:
			break;
		}

	}

	public void Reset () {
		state = STATE_SHOW;
	}

	public bool IsCanToLevel (){
		if (state == STATE_LEVEL)
			return true;
		return false;
	}

	public void SetLevelNumber (int setLevelNumber) {
		levelManager.GetComponent<LevelManager> ().SetSelectLevelNumber (setLevelNumber);
		state = STATE_LEVEL;
	}
}