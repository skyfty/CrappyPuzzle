using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour {

	private GameObject gameManager;
	public GameObject explosionPrefab;
	private GameObject tempExplosion;
	public float distance;
	public float gravity;
	private const float ROTATE_SPEED = 2.0f;

	private long attack;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.down * gravity);
		this.gameObject.transform.Rotate (ROTATE_SPEED, ROTATE_SPEED, ROTATE_SPEED, Space.Self);
	}

	public void  SetAttack (long setAttack) {
		attack = setAttack;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "Enemy"){
			tempExplosion = Instantiate (explosionPrefab) as GameObject;
			tempExplosion.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().explosion.transform);
			tempExplosion.transform.position = this.transform.position;
			if ((gameManager.GetComponent<GameManager> ().player.transform.position - this.transform.position).magnitude < distance)
				gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (PlayerShoot.ChangeType.ReduceHealth, attack);
			Destroy (this.gameObject);
		}
	}
}
