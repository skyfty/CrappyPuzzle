using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRush : MonoBehaviour {

	private GameObject gameManager;

	//aboout rush
	public float rushDistance;
	public float rushWaitTime;
	private float currentRushWaitTime;
	public float rushSpeed;
	public float rushTime;
	private float currentRushTime;
	private Vector3 rushDirection;

	private int state;
	private const int STATE_MOVE = 0;
	private const int STATE_RUSH = 1;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		state = STATE_MOVE;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<EnemyBase> ().IsAlive ()) {
			switch (state) {
			case STATE_MOVE:
				this.GetComponent<CharacterController> ().Move ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).normalized * Time.deltaTime * this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Speed));
				this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
				if ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude < rushDistance) {
					currentRushWaitTime = rushWaitTime;
					currentRushTime = rushTime;
					rushDirection = (gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).normalized;
					state = STATE_RUSH;
				}
				break;
			case STATE_RUSH:
				currentRushWaitTime -= Time.deltaTime;
				if (currentRushWaitTime < 0.0f) {
					this.GetComponent<CharacterController> ().Move (rushDirection * Time.deltaTime * rushSpeed);
					currentRushTime -= Time.deltaTime;
					if (currentRushTime < 0.0f)
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
