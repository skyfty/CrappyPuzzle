using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpgradeAttack : MonoBehaviour {

	public GameObject gameManager;
	public GameObject imageAttack;
	public GameObject quantityAttackGrade;
	public GameObject imageMoney;
	public GameObject quantityGradeMoney;
	private long playerCurrentGradeAttack;

	// Use this for initialization
	void Start () {
		UpdataPlayerData ();
	}

	// Update is called once per frame
	void Update () {
		UpdataPlayerData ();
	}

	public void Upgrade () {
		if (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Money) >= gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeAttack].money) {
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceMoney, gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeAttack].money);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.IncreaseCurrentGradeAttack, 1);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().SaveData ();
		} else {

		}
		UpdataPlayerData ();
	}

	public void UpdataPlayerData () {
		playerCurrentGradeAttack = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.CurrentGradeAttack);
		quantityAttackGrade.GetComponent<Quantity> ().SetNumber (playerCurrentGradeAttack);
		if (playerCurrentGradeAttack == gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData.Length - 1) {
			imageMoney.SetActive (true);
			quantityGradeMoney.SetActive (false);
			this.gameObject.SetActive (true);
		} else {
			quantityGradeMoney.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData[playerCurrentGradeAttack].money);
		}
	}
}
