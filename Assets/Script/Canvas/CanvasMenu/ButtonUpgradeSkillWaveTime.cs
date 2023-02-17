using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpgradeSkillWaveTime : MonoBehaviour {

	public GameObject gameManager;
	public GameObject imageSkillWaveTime;
	public GameObject quantitySkillWaveTimeGrade;
	public GameObject imageMoney;
	public GameObject quantityGradeMoney;
	private long playerCurrentGradeSkillWaveTime;

	// Use this for initialization
	void Start () {
		UpdataPlayerData ();
	}

	// Update is called once per frame
	void Update () {
		UpdataPlayerData ();
	}

	public void Upgrade () {
		if (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Money) >= gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeSkillWaveTime].money) {
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceMoney, gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData [playerCurrentGradeSkillWaveTime].money);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.IncreaseCurrentGradeSkillWaveTime, 1);
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().SaveData ();
		} else {

		}
		UpdataPlayerData ();
	}

	public void UpdataPlayerData () {
		playerCurrentGradeSkillWaveTime = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.CurrentGradeSkillWaveTime);
		quantitySkillWaveTimeGrade.GetComponent<Quantity> ().SetNumber (playerCurrentGradeSkillWaveTime);
		if (playerCurrentGradeSkillWaveTime == gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData.Length - 1) {
			imageMoney.SetActive (true);
			quantityGradeMoney.SetActive (false);
			this.gameObject.SetActive (true);
		} else {
			quantityGradeMoney.GetComponent<Quantity> ().SetNumber (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().gradeData[playerCurrentGradeSkillWaveTime].money);
		}
	}
}
