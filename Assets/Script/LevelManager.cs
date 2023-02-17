using Assets.Script.Data;
using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Resource;

public class LevelManager : MonoBehaviour {

	public GameObject gameManager;
	public GameObject[] level;
	public GameObject selectLevel;
	private int currentLevelNumber;
	private int finishLevelNumber;
	private int selectLevelNumber;

	private int state;
	private const int STATE_INITIAL = 0;
//	private const int STATE_PAUSE = 1;
//	private const int STATE_RESULT = 2;
	private const int STATE_FINISH = 3;

	private bool isSelectLevelWin;

	// Use this for initialization
	void Start () {
 
    }

    // Update is called once per frame
    void Update () {

	}

	public void SetSelectLevelNumber (int setSelectLevelNumber) {
		if (setSelectLevelNumber < 0 || setSelectLevelNumber >= level.Length) {
			print ("class LevelManager function PlayLevel parameter ‘setLevel’ value "+setSelectLevelNumber+" is wrong!");
			return;
		}
		selectLevelNumber = setSelectLevelNumber;
	}

	public void PlaySelectLevel () {
		selectLevel = Instantiate (level[selectLevelNumber]) as GameObject;
	}

	public void DestroySelectLevel () {
		Destroy (selectLevel);
	}

	public int GetCurrentLevelNumber () {
		if (PlayerPrefs.HasKey ("GameManager.LevelManager.currentLevelNumber")) {
			currentLevelNumber = PlayerPrefs.GetInt ("GameManager.LevelManager.currentLevelNumber");
		} else {
			currentLevelNumber = 0;
			PlayerPrefs.SetInt ("GameManager.LevelManager.currentLevelNumber", currentLevelNumber);
		}
		return currentLevelNumber;
	}

	public int GetFinishLevelNumber () {
		if (PlayerPrefs.HasKey ("GameManager.LevelManager.finishLevelNumber")) {
			finishLevelNumber = PlayerPrefs.GetInt ("GameManager.LevelManager.finishLevelNumber");
		} else {
			finishLevelNumber = -1;
			PlayerPrefs.SetInt ("GameManager.LevelManager.finishLevelNumber", finishLevelNumber);
		}
		return finishLevelNumber;
	}

	public void SetSelectLevelWin (bool setWin) {
		isSelectLevelWin = setWin;
		if (isSelectLevelWin) {		//if win, updata param.
			if (currentLevelNumber > finishLevelNumber) {
				finishLevelNumber = currentLevelNumber;
			}
			currentLevelNumber++;
			if (currentLevelNumber >= gameManager.GetComponent<GameManager>().canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length) {
				currentLevelNumber = gameManager.GetComponent<GameManager>().canvasManager.GetComponent<CanvasManager> ().buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length - 1;
			}
		} else {					//if fail, 
			currentLevelNumber = selectLevelNumber;
		}
		PlayerPrefs.SetInt ("GameManager.LevelManager.currentLevelNumber", currentLevelNumber);
		PlayerPrefs.SetInt ("GameManager.LevelManager.finishLevelNumber", finishLevelNumber);
	}

	public bool IsSelectLevelWin () {
		return isSelectLevelWin;
	}


}
