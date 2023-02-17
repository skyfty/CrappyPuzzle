using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

	private GameObject gameManager;
	public GameObject explosionPrefab;
	private GameObject tempExplosion;

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
//		//if is pause will return and pause the animator speed
//		if (levelManager.GetComponent<LevelManager> ().IsPause ()) {
//			this.GetComponent<Animator> ().speed = 0.0f;
//			return;
//		} else {
//			this.GetComponent<Animator> ().speed = 1.0f;
//		}

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

//		if (this.transform.position.x > MAX_SCREEN_X || this.transform.position.x < -MAX_SCREEN_X || this.transform.position.y > MAX_SCREEN_Y || this.transform.position.y < -MAX_SCREEN_Y) {
//			Destroy (this.gameObject);
//		}
	}

	public void Move(float setSpeed, Vector3 setAngle){
		speed = setSpeed;
		angle = setAngle;
		state = STATE_MOVE;
	}

	void OnTriggerEnter(Collider collider) {
//		print (collider.gameObject.name);
//		print (collider.gameObject.transform.parent.gameObject.tag);
		if (collider.gameObject.tag == "Enemy") {
			collider.gameObject.GetComponent<EnemyBase> ().Change (EnemyBase.ChangeType.ReduceHealth, gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.Attack));
			//Destroy (collider.gameObject);
		}
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Wall") {
			tempExplosion = Instantiate (explosionPrefab) as GameObject;
			tempExplosion.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().explosion.transform);
			tempExplosion.transform.position = this.transform.position;
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayHit ();
			Destroy (this.gameObject);
		}
	}
}
