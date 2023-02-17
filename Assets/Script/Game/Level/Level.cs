using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	public GameObject map;
	public GameObject enemy;
	public GameObject bullet;
	public GameObject explosion;
	public GameObject reward;
	public Army[] army;
	public Prop[] prop;
	private GameObject gameManager;
	public float currentTime;
	public int currentActArmyCount;
	private GameObject tempEnemy;
	private GameObject tempReward;

	public float maxmiumDistanceToPlayer;

	[System.Serializable]
	public class Army {
		public bool isAct = false;
		public float time;
		public GameObject enemyPrefab;
		public int grade;
		public int count;
	}
	[System.Serializable]
	public class Prop {
		public bool isAct = false;
		public float time;
		public GameObject rewardPrefab;
		public int grade;
		public int count;
	}

	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_FIGHT = 1;
	private const int STATE_PAUSE = 2;
	private const int STATE_TIP = 3;
	private const int STATE_FINISH = 4;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		currentActArmyCount = 0;
		currentTime = 0f;
		state = STATE_WAIT;
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			currentTime = 0f;
			state = STATE_FIGHT;
			break;
		case STATE_FIGHT:
			currentTime += Time.deltaTime;
			for (int i = 0; i < army.Length; i++) {
				if (army [i].isAct == false) {
					if (currentTime > army [i].time) {
						//					print (currentTime+"  "+army [i].time+"  "+i);
						for (int j = 0; j < army [i].count; j++) {
							tempEnemy = Instantiate (army [i].enemyPrefab) as GameObject;
							tempEnemy.transform.parent = enemy.transform;
							tempEnemy.transform.position = map.GetComponent<Map> ().GetRightPosition (maxmiumDistanceToPlayer);
							tempEnemy.GetComponent<EnemyBase> ().SetGrade (army [i].grade);
						}
						army [i].isAct = true;
						currentActArmyCount += 1;
					}
				}
			}
			for (int i = 0; i < prop.Length; i++) {
				if (prop [i].isAct == false) {
					if (currentTime > prop [i].time) {
						//					print (currentTime+"  "+army [i].time+"  "+i);
						for (int j = 0; j < prop [i].count; j++) {
							tempReward = Instantiate (prop [i].rewardPrefab) as GameObject;
							tempReward.transform.parent = reward.transform;
							tempReward.transform.position = map.GetComponent<Map> ().GetRightPosition (maxmiumDistanceToPlayer);
						}
						prop [i].isAct = true;
					}
				}
			}
//			print (enemy.transform.childCount + "  " + currentActArmyCount + "  " + army.Length + "  " + state);
			if ((enemy.transform.childCount == 0) && (currentActArmyCount == army.Length)) {
				gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().SetSelectLevelWin (true);
				gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<CanvasManager> ().canvasLevel.GetComponent<CanvasLevel> ().ShowWin ();
				gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayWin ();
				state = STATE_TIP;
			}
			break;
		case STATE_PAUSE:
			break;
		case STATE_TIP:
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().levelTime = (long)currentTime;
			state = STATE_FINISH;
			break;
		case STATE_FINISH:
			print ("Win");
			break;
		}
	}

	public bool IsFinish () {
		if (state == STATE_FINISH)
			return true;
		return false;
	}

	public void Pause() {
		if (state == STATE_FIGHT) state = STATE_PAUSE;
	}

	public void Resume() {
		if (state == STATE_PAUSE) state = STATE_FIGHT;
	}
}