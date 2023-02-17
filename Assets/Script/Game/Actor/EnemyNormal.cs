using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : MonoBehaviour {

	private GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<EnemyBase> ().IsAlive () && !gameManager.GetComponent<GameManager>().player.GetComponent<PlayerShoot>().IsDead()) {
			this.GetComponent<CharacterController> ().Move ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).normalized * Time.deltaTime * this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Speed));
			this.transform.LookAt (gameManager.GetComponent<GameManager> ().player.transform);
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit setHit){
		if (setHit.gameObject.name == "Player") {
			this.GetComponent<EnemyBase> ().Change (EnemyBase.ChangeType.ReduceHealth, this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.MaximumHealth));
			setHit.gameObject.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceHealth, this.GetComponent<EnemyBase> ().GetData (EnemyBase.DataType.Attack));		//hurt player
		}
	}
}
