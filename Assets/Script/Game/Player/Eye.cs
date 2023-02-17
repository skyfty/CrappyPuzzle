using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

	private GameObject gameManager;
	private GameObject nearestEnemy;
	public float maximumDistance;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.GetComponent<GameManager> ().IsStateLevel ())		//not work if not at level
			return;
		nearestEnemy = null;
		foreach (Transform child in gameManager.GetComponent<GameManager>().levelManager.GetComponent<LevelManager>().selectLevel.GetComponent<Level>().enemy.transform) {
			if ((child.transform.position - this.transform.position).magnitude < maximumDistance) {
				if (nearestEnemy == null) {
					nearestEnemy = child.gameObject;
				} else {
					if ((child.transform.position - this.transform.position).magnitude < (nearestEnemy.transform.position - this.transform.position).magnitude)
						nearestEnemy = child.gameObject;
				}
			}
		}
	}

	public GameObject GetNearestEnemy () {
		return nearestEnemy;
	}
}
