using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	private GameObject gameManager;

	//aboout shoot
	public GameObject bulletPrefab;
	public float bulletSpeed;
	private GameObject tempBullet;
	public float shootDistance;
	public float shootWaitTime;
	private float currentShootWaitTime;
	private const float SHOOT_POSITION_HEIGHT = 3.0f;

	private int state;
	private const int STATE_MOVE = 0;
	private const int STATE_SHOOT = 1;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		//if (state != STATE_MOVE)		//SetGrade will be earlier than Start.
		state = STATE_MOVE;
	}

	// Update is called once per frame
	void Update () {
		if (this.GetComponent<EnemyBase> ().IsAlive ()) {
			switch (state) {
			case STATE_MOVE:
				this.GetComponent<CharacterController> ().Move ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).normalized * Time.deltaTime * this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Speed));
				this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
				if ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude < shootDistance) {
					currentShootWaitTime = shootWaitTime;
					state = STATE_SHOOT;
				}
				break;
			case STATE_SHOOT:
				this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
				currentShootWaitTime -= Time.deltaTime;
				if (currentShootWaitTime < 0.0f) {
					tempBullet = Instantiate (bulletPrefab) as GameObject;
					tempBullet.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().bullet.transform);
					tempBullet.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + SHOOT_POSITION_HEIGHT, this.transform.position.z);
					tempBullet.GetComponent<EnemyBullet> ().SetAttack (this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Attack));
					tempBullet.GetComponent<EnemyBullet> ().Move (bulletSpeed, this.gameObject.transform.rotation * Vector3.forward);		//because no gun, so use this.gameobject.transform
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
