using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpgradeSkillBombAttack : MonoBehaviour {

	public GameObject gameManager;
	public GameObject imageSkillBombAttack;
	public GameObject quantitySkillBombAttackGrade;
	public GameObject imageMoney;
	public GameObject quantityGradeMoney;
	private long playerCurrentGradeSkillBombAttack;

	// Use this for initialization
	void Start () {
		UpdataPlayerData ();
	}

	// Update is called once per frame
	void Update () {
		UpdataPlayerData ();
	}

	public void Upgrade () {
		if (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Money) >= gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeSkillBombAttack].money) {
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceMoney, gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeSkillBombAttack].money);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.IncreaseCurrentGradeSkillBombAttack, 1);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().SaveData ();
		} else {

		}
		UpdataPlayerData ();
	}

	public void UpdataPlayerData () {
		playerCurrentGradeSkillBombAttack = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.CurrentGradeSkillBombAttack);
		quantitySkillBombAttackGrade.GetComponent<Quantity> ().SetNumber (playerCurrentGradeSkillBombAttack);
		if (playerCurrentGradeSkillBombAttack == gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData.Length - 1) {
			imageMoney.SetActive (true);
			quantityGradeMoney.SetActive (false);
			this.gameObject.SetActive (true);
		} else {
			quantityGradeMoney.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData[playerCurrentGradeSkillBombAttack].money);
		}
	}
}
