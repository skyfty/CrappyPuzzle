using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResult : MonoBehaviour {

	private GameObject gameManager;

	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_SHOW = 1;
	private const int STATE_CONFIRM = 2;

	public GameObject textTime;
	public GameObject textDamage;
	public GameObject textKill;
	public GameObject textMoney;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		state = STATE_WAIT;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			state = STATE_SHOW;
			break;
		case STATE_SHOW:
			if (Input.GetMouseButtonUp (0))
				state = STATE_CONFIRM;
			break;
		case STATE_CONFIRM:
			break;
		}
	}

	public void Reset () {
		state = STATE_WAIT;
	}

	public bool IsCanToMenu () {
		if (state == STATE_CONFIRM)
			return true;
		return false;
	}

	public void GetLevelResult () {
		textTime.GetComponent<Text> ().text = "";
		gameManager = GameObject.Find ("GameManager");
		if (gameManager == null)
			print ("123");
		textTime.GetComponent<Text> ().text = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelTime.ToString ();
		textDamage.GetComponent<Text> ().text = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelDamage.ToString ();
		textKill.GetComponent<Text> ().text = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelKill.ToString ();
		textMoney.GetComponent<Text> ().text = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelMoney.ToString ();
	}
}
