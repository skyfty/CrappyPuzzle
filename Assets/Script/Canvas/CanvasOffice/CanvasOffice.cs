using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOffice : MonoBehaviour
{

	public GameObject gameManager;
	public GameObject levelManager;
	public GameObject buttonLevelManager;
	public GameObject quantityMoney;

	public bool isFinish;
	public bool isFinishShow;
	public GameObject imageBack;
	public GameObject textFinish;
	public GameObject buttonFinishReturn;


	public GameObject inputField;
	public GameObject button;
	public GameObject text;

	private int state;
	private const int STATE_SHOW = 0;
	private const int STATE_LEVEL = 1;

	public GameObject player;
    public GameObject goldText;
    public GameObject diamondText;
	public GameObject starText;
	public GameObject levelText;

	// Use this for initialization
	void Start () {
		state = STATE_SHOW;
		isFinishShow = false;
	}

	void OnEnable() {
        /*
		if (!isFinishShow) {
			if (levelManager.GetComponent<LevelManager> ().GetFinishLevelNumber () == buttonLevelManager.GetComponent<ButtonLevelManager> ().buttonLevel.Length - 1) {
				imageBack.SetActive (true);
				textFinish.SetActive (true);
				buttonFinishReturn.SetActive (true);
				isFinishShow = true;
			}
		}
        */
	}
	
	// Update is called once per frame
	void Update () {
		if (player!=null) {
            goldText.GetComponent<Text>().text = player.GetComponent<Player>().gold.ToString();
            diamondText.GetComponent<Text>().text = player.GetComponent<Player>().diamond.ToString();
			starText.GetComponent<Text>().text = player.GetComponent<Player>().star.ToString();
			levelText.GetComponent<Text>().text = (player.GetComponent<Player>().levelBaseNumber + 1).ToString();
        }
        /*
		switch (state) {
		case STATE_SHOW:
			quantityMoney.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().player.GetComponent<Player> ().GetData (Player.DataType.Money));
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
*/
	}

	public void Reset () {
		imageBack.SetActive(false);
		inputField.SetActive(false);
		button.SetActive(false);
		text.SetActive(false);
		state = STATE_SHOW;
	}

	public bool IsCanToMatch (){
		if (state == STATE_LEVEL)
			return true;
		return false;
	}

	public void ToMatch () {
		state = STATE_LEVEL;
	}

	public void SetLevelNumber (int setLevelNumber) {
		levelManager.GetComponent<LevelManager> ().SetSelectLevelNumber (setLevelNumber);
		state = STATE_LEVEL;
	}
}
