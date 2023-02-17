using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour {

	private GameObject gameManager;

	//aboout shoot
	public GameObject bombPrefab;
	private GameObject tempBomb;
	public float throwDistance;
	public float awayDistance;
	public float throwWaitTime;
	private float currentThrowWaitTime;
	public float throwSpeed;
	public float throwHeight;
	private const float THROW_POSITION_HEIGHT = 3.0f;

	private int state;
	private const int STATE_MOVE = 0;
	private const int STATE_THROW = 1;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		state = STATE_MOVE;
	}

	// Update is called once per frame
	void Update () {
		if (this.GetComponent<EnemyBase> ().IsAlive ()) {
			this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
			switch (state) {
			case STATE_MOVE:
				if ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude >= throwDistance)
					this.GetComponent<CharacterController> ().Move ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).normalized * Time.deltaTime * this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Speed));
				if ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude <= awayDistance)
					this.GetComponent<CharacterController> ().Move ((this.transform.position - gameManager.GetComponent<GameManager> ().player.transform.position).normalized * Time.deltaTime * this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Speed));
				if ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude < throwDistance &&
				    (gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude > awayDistance) {
					currentThrowWaitTime = throwWaitTime;
					state = STATE_THROW;
				}
				break;
			case STATE_THROW:
				currentThrowWaitTime -= Time.deltaTime;
				if (currentThrowWaitTime < 0.0f) {
					tempBomb = Instantiate (bombPrefab) as GameObject;
					tempBomb.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().bullet.transform);
					tempBomb.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + THROW_POSITION_HEIGHT, this.transform.position.z);
					tempBomb.GetComponent<EnemyBomb> ().SetAttack (this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Attack));
					tempBomb.GetComponent<Rigidbody> ().velocity = this.transform.gameObject.transform.rotation * new Vector3 (0, throwHeight, 1) * throwSpeed;		//because no gun, so use this.gameobject.transform
					state = STATE_MOVE;
				}
				break;
			}
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit setHit){
		if (setHit.gameObject.name == "Player") {
			this.GetComponent<EnemyBase> ().Change (EnemyBase.ChangeType.ReduceHealth, this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.MaximumHealth));
			setHit.gameObject.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceHealth, this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Attack));		//hurt player
		}
	}
}
