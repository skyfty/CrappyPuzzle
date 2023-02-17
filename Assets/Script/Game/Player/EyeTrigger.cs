using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrigger : MonoBehaviour {

	public float speed;
	public float maximumDistance;

	private GameObject nearestEnemy;

	private int state;
	private const int STATE_FIND = 0;
	private const int STATE_LOCK = 1;


	// Use this for initialization
	void Start () {
		nearestEnemy = null;
		this.GetComponent<SphereCollider> ().radius = 0.0f;
		state = STATE_FIND;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_FIND:
			this.GetComponent<SphereCollider> ().radius += speed;
			if (this.GetComponent<SphereCollider> ().radius > maximumDistance) {
				this.GetComponent<SphereCollider> ().radius = 0.0f;
				nearestEnemy = null;
			}
			break;
		case STATE_LOCK:
			this.GetComponent<SphereCollider> ().radius = 0.0f;
			state = STATE_FIND;
			break;
		}
//		this.GetComponent<SphereCollider> ().radius = 1.0f;
	}

	public GameObject GetNearestEnemy () {
		return nearestEnemy;
	}

	void OnTriggerStay(Collider collider) {
		if (collider.transform.tag == "Enemy" || collider.transform.parent.tag == "Enemy") {
			nearestEnemy =  (collider.gameObject);
			state = STATE_LOCK;
		}
	}
}