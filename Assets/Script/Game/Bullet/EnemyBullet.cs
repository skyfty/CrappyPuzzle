using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private GameObject gameManager;
	public GameObject explosionPrefab;
	private GameObject tempExplosion;
	private long attack;

	private const float MAXIMUM_LIFE = 10.0f;
	private float currentLife;
	private float speed;
	private Vector3 angle;
	private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_MOVE = 1;

	// Use this for initialization
	void Start () {
		//state = STATE_WAIT;		//write here will be wrong, because Start() is late than Move()
	}

	void Awake(){
		state = STATE_WAIT;
		currentLife = 0.0f;
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_WAIT:
			break;
		case STATE_MOVE:
			currentLife += Time.deltaTime;
			if (currentLife > MAXIMUM_LIFE) {
				Destroy (this.gameObject);
			}
			this.transform.Translate (angle * speed);
			break;
		}

	}

	public void Move(float setSpeed, Vector3 setAngle){
		speed = setSpeed;
		angle = setAngle;
		state = STATE_MOVE;
	}

	public void  SetAttack (long setAttack) {
		attack = setAttack;
	}

	void OnTriggerEnter(Collider collider) {
				print (collider.gameObject.name);
		//		print (collider.gameObject.transform.parent.gameObject.tag);
		if (collider.gameObject.tag == "Player") {
			collider.gameObject.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceHealth, attack);
		}
		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Wall") {
			tempExplosion = Instantiate (explosionPrefab) as GameObject;
			tempExplosion.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().explosion.transform);
			tempExplosion.transform.position = this.transform.position;
			Destroy (this.gameObject);
		}
	}
}
