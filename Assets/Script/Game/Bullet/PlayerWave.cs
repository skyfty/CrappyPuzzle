using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWave : MonoBehaviour {

	private GameObject gameManager;
	public float acceleration;
	public float power;
	private float speed = 0.0f;
	private float distance = 0.0f;
	private const float MAXIMUM_DISTANCE = 32.0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		speed += acceleration;
		distance += speed;
		this.transform.localScale = new Vector3 (distance, distance / 2, distance);
		foreach (Transform child in gameManager.GetComponent<GameManager>().levelManager.GetComponent<LevelManager>().selectLevel.GetComponent<Level>().enemy.transform) {
			if ((child.transform.position - this.transform.position).magnitude < distance /2) {
				child.GetComponent<CharacterController> ().Move ((child.transform.position - this.transform.position).normalized * power);
			}
		}
		if (distance > MAXIMUM_DISTANCE)
			Destroy (this.gameObject);
	}
}