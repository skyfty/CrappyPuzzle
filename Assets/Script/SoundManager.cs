using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public GameObject gameManager;
	public bool isSoundOn;

//	public AudioClip clipClick;
//	public AudioClip clipBack;
//	public AudioClip clipWin;

	public AudioSource sourceClick;
	public AudioSource sourceBack;
	public AudioSource sourceWin;
	public AudioSource sourceGameover;
	public AudioSource sourceBomb;
	public AudioSource sourceWave;
	public AudioSource sourceUpgrade;
	public AudioSource sourceMoney;
	public AudioSource sourceFire;
	public AudioSource sourceMenu;
	public AudioSource sourceHurt;
	public AudioSource[] sourceHit;
	public AudioSource[] sourceKill;

	// Awake must use here for other object to use the isSoundOn.
	void Awake () {
		if (PlayerPrefs.HasKey ("ButtonSound.isSoundOn")) {
			if (PlayerPrefs.GetInt ("ButtonSound.isSoundOn") == 0) {
				isSoundOn = false;
			} else {
				isSoundOn = true;
			}
		} else {
			isSoundOn = true;
		}
		//PlayBack ();
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
//		if (isSoundOn) {
//			if (sourceWin.isPlaying) {
//				sourceBack.Pause ();
//			} else {
//				if (sourceBack.isPlaying) {
//					sourceBack.UnPause ();
//				} else {
////					sourceBack.Play ();
//				}
//			}
//		}
	}

	public void PlayClick (){
		if (!isSoundOn || sourceClick.isPlaying)
			return;
		sourceClick.Play ();
	}

	public void PlayBack (){
		if (!isSoundOn || sourceBack.isPlaying)
			return;
		sourceBack.Play ();
	}

	public void PlayWin (){
		if (!isSoundOn || sourceWin.isPlaying)
			return;
		sourceWin.Play ();
	}

	public void PlayGameover (){
		if (!isSoundOn || sourceGameover.isPlaying)
			return;
		sourceGameover.Play ();
	}

	public void PlayBomb (){
		if (!isSoundOn || sourceBomb.isPlaying)
			return;
		sourceBomb.Play ();
	}

	public void PlayWave (){
		if (!isSoundOn || sourceWave.isPlaying)
			return;
		sourceWave.Play ();
	}

	public void PlayUpgrade (){
		if (!isSoundOn || sourceUpgrade.isPlaying)
			return;
		sourceUpgrade.Play ();
	}

	public void PlayMoney (){
		if (!isSoundOn)// || sourceMoney.isPlaying)
			return;
		sourceMoney.Play ();
	}

	public void PlayFire (){
		if (!isSoundOn || sourceFire.isPlaying)
			return;
		sourceFire.Play ();
	}

	public void PlayHurt (){
		if (!isSoundOn || sourceHurt.isPlaying)
			return;
		sourceHurt.Play ();
	}

	public void PlayHit (){
		if (!isSoundOn)
			return;
		for (int i=0; i < sourceHit.Length; i++) {
			if (sourceHit [i].isPlaying == false) {
				sourceHit [i].Play ();
				break;
			}
		}
	}

	public void PlayKill (){
		if (!isSoundOn)// || sourceKill.isPlaying)
			return;
		for (int i=0; i < sourceKill.Length; i++) {
			if (sourceKill [i].isPlaying == false) {
				sourceKill [i].Play ();
				break;
			}
		}
	}

	public void PlayMenu (){
		if (!isSoundOn || sourceMenu.isPlaying)
			return;
		sourceMenu.Play ();
	}

	public void SetIsSound (bool setIsSound) {
		isSoundOn = setIsSound;
		if (isSoundOn) {
			PlayerPrefs.SetInt ("ButtonSound.isSoundOn", 1);
			UnPause ();
		} else {
			PlayerPrefs.SetInt ("ButtonSound.isSoundOn", 0);
			Pause ();
		}
	}

	public void Pause (){
		if (sourceClick.isPlaying)
			sourceClick.Stop ();
		if (sourceBack.isPlaying)
			sourceBack.Stop ();
		if (sourceWin.isPlaying)
			sourceWin.Stop ();
		if (sourceGameover.isPlaying)
			sourceGameover.Stop ();
		if (sourceBomb.isPlaying)
			sourceBomb.Stop ();
		if (sourceWave.isPlaying)
			sourceWave.Stop ();
		if (sourceUpgrade.isPlaying)
			sourceUpgrade.Stop ();
		if (sourceMoney.isPlaying)
			sourceMoney.Stop ();
		if (sourceFire.isPlaying)
			sourceFire.Stop ();
		if (sourceMenu.isPlaying)
			sourceMenu.Stop ();
		if (sourceHurt.isPlaying)
			sourceHurt.Stop ();
		for (int i = 0; i < sourceHit.Length; i++) {
			if (sourceHit [i].isPlaying == true)
				sourceHit [i].Stop ();
		}
		for (int i = 0; i < sourceKill.Length; i++) {
			if (sourceKill [i].isPlaying == true)
				sourceKill [i].Stop ();
		}
	}

	public void UnPause (){
		if (gameManager.GetComponent<GameManager> ().IsStateLevel ())
			sourceBack.Play ();
		else
			sourceMenu.Play ();
	}
}
