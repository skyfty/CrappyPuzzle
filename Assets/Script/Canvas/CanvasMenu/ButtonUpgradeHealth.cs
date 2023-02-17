using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpgradeHealth : MonoBehaviour {

	public GameObject gameManager;
	public GameObject imageHealth;
	public GameObject quantityHealthGrade;
	public GameObject imageMoney;
	public GameObject quantityGradeMoney;
	private long playerCurrentGradeHealth;

	// Use this for initialization
	void Start () {
		UpdataPlayerData ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdataPlayerData ();
	}

	public void Upgrade () {
		if (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Money) >= gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeHealth].money) {
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceMoney, gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeHealth].money);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.IncreaseCurrentGradeHealth, 1);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().SaveData ();
		} else {
			
		}
		UpdataPlayerData ();
	}

	public void UpdataPlayerData () {
		playerCurrentGradeHealth = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.CurrentGradeHealth);
		quantityHealthGrade.GetComponent<Quantity> ().SetNumber (playerCurrentGradeHealth);
		if (playerCurrentGradeHealth == gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData.Length - 1) {
			imageMoney.SetActive (true);
			quantityGradeMoney.SetActive (false);
			this.gameObject.SetActive (true);
		} else {
			quantityGradeMoney.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData[playerCurrentGradeHealth].money);
		}
	}
}
