using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public GameObject gameManager;
	public GameObject bulletPrefab;
	private GameObject tempBullet;

	public float interval;
	private float currentInterval;
	public float speed;

	public GameObject currentLevelEnemy;	//to be parent

	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_FIRE = 1;

	// Use this for initialization
	void Start () {
		state = STATE_WAIT;
		//Fire ();
		//currentLevelEnemy = gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().enemy;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			break;
		case STATE_FIRE:
			currentInterval += Time.deltaTime;
			if (currentInterval > interval) {
				tempBullet = Instantiate (bulletPrefab) as GameObject;
				tempBullet.transform.position = this.transform.position;
				tempBullet.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().bullet.transform);
				tempBullet.GetComponent<PlayerBullet> ().Move (speed, this.transform.parent.gameObject.transform.rotation * Vector3.forward);
				currentInterval = 0.0f;
			}
			break;
		}
	}

	public void Fire () {
		if (state == STATE_WAIT) {
			currentInterval = interval;
			state = STATE_FIRE;
		}
	}

	public void Wait () {
		state = STATE_WAIT;
	}
}
