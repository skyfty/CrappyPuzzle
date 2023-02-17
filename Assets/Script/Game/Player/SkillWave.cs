using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWave : MonoBehaviour {

	public GameObject gameManager;
	public GameObject wavePrefab;
	public float currentTime;
	public float fireTime;
	private GameObject tempWave;

	// Use this for initialization
	void Start () {
		currentTime = 0.0f;
		fireTime = gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().GetData (PlayerShoot.DataType.SkillWaveTime);
	}

	// Update is called once per frame
	void Update () {
		currentTime -= Time.deltaTime;
		if (currentTime < 0.0f)
			currentTime = 0.0f;
	}

	public void Fire () {
		gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayWave ();
		if (currentTime != 0.0f)		//
			return;
		tempWave = Instantiate (wavePrefab) as GameObject;
		tempWave.transform.SetParent (gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().bullet.transform);
		tempWave.transform.position = gameManager.GetComponent<GameManager> ().player.transform.position;			// (this.transform.position.x, this.transform.position.y + 5.0f, this.transform.position.z);
//		tempWave.GetComponent<Rigidbody> ().velocity = this.transform.parent.gameObject.transform.rotation * new Vector3 (0, height, 1) * speed;		//1 mean vector3.forward
//		currentBomb.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.one));
//		bombPrefab.GetComponent<Bullet> ().Move (speed, this.transform.parent.gameObject.transform.rotation * Vector3.forward);
		currentTime = fireTime;
	}

	public void Reset() {
		currentTime = 0.0f;
	}
}
