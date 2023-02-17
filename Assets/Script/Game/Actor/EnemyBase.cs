using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	
	public GameObject explosionPrefab;
	private GameObject tempExplosion;
	private GameObject gameManager;
	private const float GRAVITY_VALUE = -5.0f;

	private int grade;
	private long currentHealth;
	private float speed = 2.0f;
	private long attack;
	private long money;

	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_ALIVE = 1;
	private const int STATE_DEAD = 2;

	public GradeData[] gradeData;
	[System.Serializable]
	public class GradeData {
		public long health;
		public float speed;
		public long attack;
		public long money;
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		if (state != STATE_ALIVE)		//SetGrade will be earlier than Start.
			state = STATE_WAIT;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			//need set grade.
			break;
		case STATE_ALIVE:
			this.GetComponent<CharacterController> ().Move (new Vector3 (0, GRAVITY_VALUE, 0));
//			this.GetComponent<CharacterController> ().Move ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).normalized * Time.deltaTime * speed);
//			this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
			break;
		case STATE_DEAD:
			tempExplosion = Instantiate (explosionPrefab) as GameObject;
			tempExplosion.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().explosion.transform);
			tempExplosion.transform.position = this.transform.position;
            if (money != 0)
            {
                gameManager.GetComponent<GameManager>().canvasManager.GetComponent<CanvasManager>().canvasLevel.GetComponent<CanvasLevel>().ShowTipIncreaseMoney(this.gameObject, money);
                gameManager.GetComponent<GameManager>().player.GetComponent<PlayerShoot>().Change(PlayerShoot.ChangeType.IncreaseMoney, money);
            }
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayKill ();
			Destroy (this.gameObject);
			break;
		}
	}

	public void SetGrade (int setGrade) {
		grade = setGrade;
		currentHealth = gradeData [grade].health;
		speed = gradeData [grade].speed;
		attack = gradeData [grade].attack;
		money = (long)Random.Range (0, (int)gradeData [grade].money);		//why will be Range(float,float)?       (long) will make 0.001 be 0.
		state = STATE_ALIVE;
	}

	public enum DataType {
		Grade = 0,
		CurrentHealth = 1,
		MaximumHealth = 2,
		Attack = 3,
		Speed = 4,
		Money = 5
	}

	public long GetData (DataType setDataType) {
		switch (setDataType) {
		case DataType.Grade:
			return grade;
		case DataType.CurrentHealth:
			return currentHealth;
		case DataType.MaximumHealth:
			return gradeData [grade].health;
		case DataType.Attack:
			return gradeData [grade].attack;
		case DataType.Speed:
			return (long)gradeData [grade].speed;
		case DataType.Money:
			return gradeData [grade].money;
		}
		return -1;
	}

	public enum ChangeType {
		IncreaseHealth = 0,
		ReduceHealth = 1,
		IncreaseMoney = 2,
		ReduceMoney = 3
	}

	public void Change (ChangeType setChangeType, long setChangeValue) {
		switch (setChangeType) {
		case ChangeType.IncreaseHealth:
			currentHealth += setChangeValue;
			break;
		case ChangeType.ReduceHealth:
			currentHealth -= setChangeValue;
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelDamage += setChangeValue;
			if (currentHealth <= 0) {
				gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelKill += 1;
				state = STATE_DEAD;
			}
			break;
		case ChangeType.IncreaseMoney:
			money += setChangeValue;
			break;
		case ChangeType.ReduceMoney:
			money -= setChangeValue;
			if (money < 0)
				money = 0;
			break;
		}
	}

	public bool IsAlive () {
		if (state == STATE_ALIVE)
			return true;
		return false;
	}
}
