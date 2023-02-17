using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBomb : MonoBehaviour {

	public GameObject gameManager;
	public GameObject bombPrefab;
	public float currentTime;
	public float fireTime;
	private GameObject tempBomb;
	public float speed;
	public float height;
	private const float FIRE_HEIGHT = 5.0f;

	// Use this for initialization
	void Start () {
		currentTime = 0.0f;
//		fireTime = gameManager.GetComponent<GameManager> ().player.GetComponent<Player> ().Get (Player.GetType.SkillBombLevel);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if (currentTime < 0.0f)
			currentTime = 0.0f;
	}

	public void Fire () {
		if (currentTime != 0.0f)		//
			return;
		tempBomb = Instantiate (bombPrefab) as GameObject;
		tempBomb.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().bullet.transform);
		tempBomb.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + FIRE_HEIGHT, this.transform.position.z);
		tempBomb.GetComponent<Rigidbody> ().velocity = this.transform.parent.gameObject.transform.rotation * new Vector3 (0, height, 1) * speed;		//1 mean vector3.forward
//		currentBomb.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.one));
//		bombPrefab.GetComponent<Bullet> ().Move (speed, this.transform.parent.gameObject.transform.rotation * Vector3.forward);
		currentTime = fireTime;
	}

	public void Reset() {
		currentTime = 0.0f;
	}
}
