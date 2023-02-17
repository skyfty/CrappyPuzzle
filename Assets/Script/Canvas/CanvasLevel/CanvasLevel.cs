using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLevel : MonoBehaviour {

	public GameObject gameManager;
	public GameObject player;

	public GameObject imageJoystick;
	public GameObject buttonSkillBomb;
	public GameObject buttonSkillWave;

	public GameObject iconEnemyCurrentGrade;
	public GameObject quantityEnemyCurrentGrade;

	public GameObject iconEnemyCurrentHealth;
	public GameObject quantityEnemyCurrentHealth;

	public GameObject iconEnemyCount;
	public GameObject quantityEnemyCount;

	public GameObject iconArmyCount;
	public GameObject quantityArmyCount;

	public GameObject iconPlayerCurrentHealth;
	public GameObject quantityPlayerCurrentHealth;
//	public GameObject quantityPlayerMaximumHealth;

	public GameObject iconPlayerAttack;
	public GameObject quantityPlayerAttack;

	public GameObject iconPlayerMoney;
	public GameObject quantityPlayerMoney;

	public GameObject imageResult;
	public GameObject textWin;
	public GameObject textFail;
	public GameObject buttonRevive;
	public GameObject buttonFail;

	public GameObject tipIncreaseMoneyPrefab;
    public GameObject tipIncreaseHealthPrefab;
    public GameObject tipReduceHealthPrefab;

    public GameObject imageHurt;

	private int state;
	private const int STATE_SHOW = 0;
	private const int STATE_WIN = 1;
	private const int STATE_FAIL = 2;
	private const int STATE_RESULT = 3;
	private float showTime;
	private const float SHOW_TIME = 3.0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		showTime = SHOW_TIME;
		state = STATE_SHOW;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_SHOW:
			if (player.GetComponent<PlayerShoot> ().eye.GetComponent<Eye> ().GetNearestEnemy () != null) {
				quantityEnemyCurrentGrade.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().eye.GetComponent<Eye> ().GetNearestEnemy ().GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Grade));
				quantityEnemyCurrentHealth.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().eye.GetComponent<Eye> ().GetNearestEnemy ().GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.CurrentHealth));
//				quantityEnemyMaximumHealth.GetComponent<Quantity> ().SetNumber (player.GetComponent<Player> ().eye.GetComponent<Eye> ().GetNearestEnemy ().GetComponent<EnemyBase> ().GetData (EnemyBase.GetType.MaximumHealth));
			}
			quantityPlayerCurrentHealth.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Health));
//			quantityPlayerMaximumHealth.GetComponent<Quantity> ().SetNumber (player.GetComponent<Player> ().gradeData [player.GetComponent<Player> ().GetData (Player.GetType.CurrentGradeHealth)].health);
			quantityPlayerAttack.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().gradeData [player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.CurrentGradeAttack)].attack);
			quantityPlayerMoney.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Money));
			quantityArmyCount.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().currentActArmyCount);
//			quantityArmyMaximumCount.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().army.Length);
			quantityEnemyCount.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().enemy.transform.childCount);
			break;
		case STATE_WIN:
			quantityPlayerCurrentHealth.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Health));
			showTime -= Time.deltaTime;
			if (showTime < 0.0f) {
				gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().SaveData ();
				state = STATE_RESULT;
			}
			break;
		case STATE_FAIL:
			quantityPlayerCurrentHealth.GetComponent<Quantity> ().SetNumber (player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Health));		//show player health is 0.
			/*
			showTime -= Time.deltaTime;
			if (showTime < 0.0f) {
				gameManager.GetComponent<GameManager> ().player.GetComponent<Player> ().SaveData ();
				state = STATE_RESULT;
			}
			*/
			break;
		case STATE_RESULT:
			break;
		}
		if (imageHurt.activeSelf == true)
			imageHurt.SetActive (false);
	}

	public void Reset () {
		showTime = SHOW_TIME;
		imageResult.SetActive (false);
		textWin.SetActive (false);
		textFail.SetActive (false);
		buttonRevive.SetActive (false);
		buttonFail.SetActive (false);
		state = STATE_SHOW;
	}

	public bool IsCanToResult (){
		if (state == STATE_RESULT)
			return true;
		return false;
	}

	public void ShowHurt () {
		if (imageHurt.activeSelf == false)
			imageHurt.SetActive (true);
	}

	public void ShowWin () {
		imageResult.SetActive (true);
		textWin.SetActive (true);
		state = STATE_WIN;
	}

	public void ShowFail () {
		imageResult.SetActive (true);
		textFail.SetActive (true);
		buttonRevive.SetActive (true);
		buttonFail.SetActive (true);
		state = STATE_FAIL;
	}

	public void ToResult (){
		if (state == STATE_FAIL) {
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().SaveData ();
			state = STATE_RESULT;
		}
	}

	public void ShowTipIncreaseMoney (GameObject setEnemy, long setValue)
    {
		GameObject tempTipIncreaseMoney = Instantiate (tipIncreaseMoneyPrefab) as GameObject;
		tempTipIncreaseMoney.transform.SetParent (this.transform);

        tempTipIncreaseMoney.GetComponent<RectTransform> ().localPosition = new Vector3 (
			(gameManager.GetComponent<GameManager>().camera.GetComponent<Camera>().WorldToViewportPoint (setEnemy.gameObject.transform.position).x - 0.5f) * gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform> ().rect.width,
			(gameManager.GetComponent<GameManager>().camera.GetComponent<Camera>().WorldToViewportPoint (setEnemy.gameObject.transform.position).y - 0.5f) * gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform> ().rect.height + 100f,
			0);
		tempTipIncreaseMoney.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (Vector3.zero);
		tempTipIncreaseMoney.GetComponent<RectTransform> ().localScale = Vector3.one;
		tempTipIncreaseMoney.SetActive (true);
		tempTipIncreaseMoney.GetComponent<Tip> ().SetTargetAndValue (setEnemy, setValue);
	}

    public void ShowTipIncreaseHealth(GameObject setEnemy, long setValue)
    {
        GameObject tempTipIncreaseHealth = Instantiate(tipIncreaseHealthPrefab) as GameObject;
        tempTipIncreaseHealth.transform.SetParent(this.transform);

        tempTipIncreaseHealth.GetComponent<RectTransform>().localPosition = new Vector3(
            (gameManager.GetComponent<GameManager>().camera.GetComponent<Camera>().WorldToViewportPoint(setEnemy.gameObject.transform.position).x - 0.5f) * gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.width,
            (gameManager.GetComponent<GameManager>().camera.GetComponent<Camera>().WorldToViewportPoint(setEnemy.gameObject.transform.position).y - 0.5f) * gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.height + 100f,
            0);
        tempTipIncreaseHealth.GetComponent<RectTransform>().localRotation = Quaternion.Euler(Vector3.zero);
        tempTipIncreaseHealth.GetComponent<RectTransform>().localScale = Vector3.one;
        tempTipIncreaseHealth.SetActive(true);
        tempTipIncreaseHealth.GetComponent<Tip>().SetTargetAndValue(setEnemy, setValue);
    }
}
