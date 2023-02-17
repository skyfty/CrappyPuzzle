using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private int mainState;
	private int branchState;

	private const int STATE_INITIAL = 0;
	private const int STATE_INITIAL_INITIAL = 0;
	private const int STATE_INITIAL_FINISH = 1;

	private const int STATE_LANGUAGE = 1;
	private const int STATE_LANGUAGE_INITIAL = 10;
	private const int STATE_LANGUAGE_SHOW = 11;
	private const int STATE_LANGUAGE_CONFIRM = 12;
	private const int STATE_LANGUAGE_FINISH = 13;

	private const int STATE_TITLE = 2;
	private const int STATE_TITLE_INITIAL = 20;
	private const int STATE_TITLE_SHOW = 21;
	private const int STATE_TITLE_FINISH = 22;

	private const int STATE_MENU = 93;
	private const int STATE_MENU_INITIAL = 930;
	private const int STATE_MENU_SHOW = 931;
	private const int STATE_MENU_FINISH = 932;

	private const int STATE_OFFICE = 3;
	private const int STATE_OFFICE_INITIAL = 30;
	private const int STATE_OFFICE_SHOW = 31;
	private const int STATE_OFFICE_FINISH = 32;

	private const int STATE_MATCH = 4;
	private const int STATE_MATCH_INITIAL = 40;
	private const int STATE_MATCH_SHOW = 41;
	private const int STATE_MATCH_FINISH = 42;

	private const int STATE_LEVEL = 94;
	private const int STATE_LEVEL_INITIAL = 940;
	private const int STATE_LEVEL_SHOW = 941;
	private const int STATE_LEVEL_FINISH = 942;

	private const int STATE_RESULT = 5;
	private const int STATE_RESULT_INITIAL = 50;
	private const int STATE_RESULT_SHOW = 51;
	private const int STATE_RESULT_FINISH = 52;

	private const int STATE_PAUSE = 6;

    public GameObject canvasBack;
    public GameObject canvasLanguage;
	public GameObject canvasTitle;
	public GameObject canvasOffice;
	public GameObject canvasMatch;
	public GameObject canvasMenu;
	public GameObject canvasLevel;
	public GameObject canvasResult;

	public GameObject gameManager;
	public GameObject soundManager;
	public GameObject languageManager;
	public GameObject buttonLevelManager;
	public GameObject imageJoystick;

	private Vector3 recordPositonImageTitle;
	private Vector3 recordPositionButtonAbout;
	private Vector3 recordPositionButtonHelp;
	private Vector3 recordPositionButtonSound;
//	private Vector3 recordPositionButtonSoundOff;
	private Vector3 recordPositionButtonExit;

	// Use this for initialization
	void Start () {
		mainState = STATE_INITIAL;
		branchState = STATE_INITIAL_INITIAL;
	}
	
	// Update is called once per frame
	void Update () {
		switch (mainState) {
		case STATE_INITIAL:
			switch (branchState) {
			case STATE_INITIAL_INITIAL:
				branchState = STATE_INITIAL_FINISH;
				break;
			case STATE_INITIAL_FINISH:
				break;
			}
			break;
		case STATE_LANGUAGE:
			switch (branchState) {
			case STATE_LANGUAGE_INITIAL:
				if (languageManager.GetComponent<LanguageManager> ().IsSaved ()) {
					branchState = STATE_LANGUAGE_FINISH;
				} else {
					if (canvasLanguage.activeSelf == false)
						canvasLanguage.SetActive (true);
                    //if (canvasBack.activeSelf == false)
                    //    canvasBack.SetActive(false);
					branchState = STATE_LANGUAGE_SHOW;
				}
				break;
			case STATE_LANGUAGE_SHOW:
				if (canvasLanguage.GetComponent<CanvasLanguage> ().IsDialogConfirm())
					branchState = STATE_LANGUAGE_CONFIRM;
				break;
			case STATE_LANGUAGE_CONFIRM:
				if (canvasLanguage.GetComponent<CanvasLanguage> ().IsConfirmDialogConfirm ()) {
					if (canvasLanguage.activeSelf == true)
						canvasLanguage.SetActive (false);
					branchState = STATE_LANGUAGE_FINISH;
				}
				if (canvasLanguage.GetComponent<CanvasLanguage> ().IsCancelDialogConfirm())
					branchState = STATE_LANGUAGE_SHOW;
				break;
			case STATE_LANGUAGE_FINISH:
				break;
			}
			break;
		case STATE_TITLE:
			switch (branchState) {
			case STATE_TITLE_INITIAL:
				if (canvasTitle.activeSelf == false)
					canvasTitle.SetActive (true);
                //if (canvasBack.activeSelf == false)
                //    canvasBack.SetActive(true);
					canvasBack.SetActive(false);
				canvasTitle.GetComponent<CanvasTitle> ().Reset ();
				soundManager.GetComponent<SoundManager> ().PlayMenu ();
				branchState = STATE_TITLE_SHOW;
				break;
			case STATE_TITLE_SHOW:
				if (canvasTitle.GetComponent<CanvasTitle> ().IsCanToOffice()) {
					if (canvasTitle.activeSelf == true)
						canvasTitle.SetActive (false);
					branchState = STATE_TITLE_FINISH;
				}
				break;
			case STATE_TITLE_FINISH:
				break;
			}
			break;
		case STATE_OFFICE:
			switch (branchState) {
			case STATE_OFFICE_INITIAL:
				if (canvasOffice.activeSelf == false)
					canvasOffice.SetActive (true);
				canvasOffice.GetComponent<CanvasOffice> ().Reset ();
				branchState = STATE_OFFICE_SHOW;
				break;
			case STATE_OFFICE_SHOW:
				if (canvasOffice.GetComponent<CanvasOffice> ().IsCanToMatch()) {
					if (canvasOffice.activeSelf == true)
						canvasOffice.SetActive (false);
					branchState = STATE_OFFICE_FINISH;
				}
				break;
			case STATE_OFFICE_FINISH:
				break;
			}
			break;
		case STATE_MATCH:
			switch (branchState) {
			case STATE_MATCH_INITIAL:
				if (canvasMatch.activeSelf == false)
					canvasMatch.SetActive (true);
				//canvasOffice.GetComponent<CanvasOffice> ().ToMatch();
				branchState = STATE_MATCH_SHOW;
				break;
			case STATE_MATCH_SHOW:
			/*
				if (canvasOffice.GetComponent<CanvasOffice> ().IsCanToLevel()) {
					if (canvasOffice.activeSelf == true)
						canvasOffice.SetActive (false);
					branchState = STATE_MATCH_FINISH;
				}
				*/
				branchState = STATE_MATCH_FINISH;
				break;
			case STATE_MATCH_FINISH:
				break;
			}
			break;
		case STATE_MENU:
			switch (branchState) {
			case STATE_MENU_INITIAL:
				if (canvasMenu.activeSelf == false)
					canvasMenu.SetActive (true);
                if (canvasBack.activeSelf == false)
                    canvasBack.SetActive(true);
				canvasMenu.GetComponent<CanvasMenu> ().Reset ();
				branchState = STATE_MENU_SHOW;
				break;
			case STATE_MENU_SHOW:
				if (canvasMenu.GetComponent<CanvasMenu> ().IsCanToLevel()) {
					if (canvasMenu.activeSelf == true)
						canvasMenu.SetActive (false);
                    if (canvasBack.activeSelf == true)
						canvasBack.SetActive (false);
					branchState = STATE_MENU_FINISH;
				}
				break;
			case STATE_MENU_FINISH:
				break;
			}
			break;
		case STATE_LEVEL:
			switch (branchState) {
			case STATE_LEVEL_INITIAL:
				if (canvasLevel.activeSelf == false)
					canvasLevel.SetActive (true);
				canvasLevel.GetComponent<CanvasLevel> ().Reset ();
				soundManager.GetComponent<SoundManager> ().sourceMenu.Stop ();
				soundManager.GetComponent<SoundManager> ().PlayBack ();
				branchState = STATE_LEVEL_SHOW;
				break;
			case STATE_LEVEL_SHOW:
				if (canvasLevel.GetComponent<CanvasLevel> ().IsCanToResult()) {
					if (canvasLevel.activeSelf == true)
						canvasLevel.SetActive (false);
                    soundManager.GetComponent<SoundManager> ().sourceBack.Stop ();
					soundManager.GetComponent<SoundManager> ().PlayMenu ();
					canvasResult.GetComponent<CanvasResult> ().GetLevelResult ();
					branchState = STATE_LEVEL_FINISH;
				}
				break;
			case STATE_LEVEL_FINISH:
				
				break;
			}
			break;
		case STATE_RESULT:
			switch (branchState) {
			case STATE_RESULT_INITIAL:
                if (canvasBack.activeSelf == false)
                    canvasBack.SetActive(true);
                if (canvasResult.activeSelf == false)
					canvasResult.SetActive (true);
				canvasResult.GetComponent<CanvasResult> ().Reset ();
				branchState = STATE_RESULT_SHOW;
				break;
			case STATE_RESULT_SHOW:
				if (canvasResult.GetComponent<CanvasResult> ().IsCanToMenu()) {
					if (canvasResult.activeSelf == true)
						canvasResult.SetActive (false);
					branchState = STATE_RESULT_FINISH;
				}
				break;
			case STATE_RESULT_FINISH:
				break;
			}
			break;
		default:
			break;
		}
	}

	//change mainState function
	public bool ChangeMainStateFromInitialToLanguage () {
		if (mainState == STATE_INITIAL && branchState == STATE_INITIAL_FINISH) {
			mainState = STATE_LANGUAGE;
			branchState = STATE_LANGUAGE_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromLanguageToTitle () {
		if (mainState == STATE_LANGUAGE && branchState == STATE_LANGUAGE_FINISH) {
			mainState = STATE_TITLE;
			branchState = STATE_TITLE_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromTitleToMenu () {
		if (mainState == STATE_TITLE && branchState == STATE_TITLE_FINISH) {
			mainState = STATE_MENU;
			branchState = STATE_MENU_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromTitleToOffice () {
		if (mainState == STATE_TITLE && branchState == STATE_TITLE_FINISH) {
			mainState = STATE_OFFICE;
			branchState = STATE_OFFICE_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromOfficeToMatch () {
		if (mainState == STATE_OFFICE && branchState == STATE_OFFICE_FINISH) {
			mainState = STATE_MATCH;
			branchState = STATE_MATCH_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromMatchToOffice () {
		if (mainState == STATE_MATCH && branchState == STATE_MATCH_FINISH) {
			mainState = STATE_OFFICE;
			branchState = STATE_OFFICE_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromMenuToLevel () {
		if (mainState == STATE_MENU && branchState == STATE_MENU_FINISH) {
			mainState = STATE_LEVEL;
			branchState = STATE_LEVEL_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromLevelToResult () {
		if (mainState == STATE_LEVEL && branchState == STATE_LEVEL_FINISH) {
			mainState = STATE_RESULT;
			branchState = STATE_RESULT_INITIAL;
			return true;
		}
		return false;
	}

	public bool ChangeMainStateFromResultToMenu () {
		if (mainState == STATE_RESULT && branchState == STATE_RESULT_FINISH) {
			mainState = STATE_MENU;
			branchState = STATE_MENU_INITIAL;
			return true;
		}
		return false;
	}

	public void InitialToTitle() {
		canvasTitle.SetActive(true);
		mainState = STATE_TITLE;
	}

	public void TitleToOffice() {
		canvasTitle.SetActive(false);
		canvasOffice.SetActive(true);
		mainState = STATE_OFFICE;
	}

	public void OfficeToMatch() {
		canvasOffice.GetComponent<CanvasOffice>().Reset();
		canvasTitle.SetActive(false);
		canvasOffice.SetActive(false);
		canvasMatch.SetActive(true);
		mainState = STATE_MATCH;
	}

	public void MatchToOffice() {
		canvasMatch.GetComponent<CanvasMatch>().Reset();
		canvasTitle.SetActive(false);
		canvasOffice.SetActive(true);
		canvasMatch.SetActive(false);
		mainState = STATE_OFFICE;
	}
	

}