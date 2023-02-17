using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour {

	private GameObject gameManager;
	public GameObject explosionPrefab;
	private GameObject tempExplosion;
	public float distance;
	public float gravity;
	private const float ROTATE_SPEED = 2.0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.down * gravity);
		this.gameObject.transform.Rotate (ROTATE_SPEED, ROTATE_SPEED, ROTATE_SPEED, Space.Self);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag != "Player"){
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayBomb ();
			tempExplosion = Instantiate (explosionPrefab) as GameObject;
			tempExplosion.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().explosion.transform);
			tempExplosion.transform.position = this.transform.position;
			foreach (Transform child in gameManager.GetComponent<GameManager>().levelManager.GetComponent<LevelManager>().selectLevel.GetComponent<Level>().enemy.transform) {
				if ((child.transform.position - this.transform.position).magnitude < distance) {
					child.GetComponent<EnemyBase> ().Change (EnemyBase.ChangeType.ReduceHealth, gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.SkillBombAttack));
				}
			}
			Destroy (this.gameObject);
		}
	}
}
