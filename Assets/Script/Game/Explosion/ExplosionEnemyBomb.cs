using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnemyBomb : MonoBehaviour {

	public float scaleXZ;
	private float scaleY = 0.0f;
	private const float ACCELERATION_SCALE_Y = 0.5f;
	private const float MAXIMUM_SCALE_Y = 8.0f;
	private float speed = 0.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		speed += ACCELERATION_SCALE_Y;
		scaleY += speed;
		this.transform.localScale = new Vector3 (scaleXZ, scaleY, scaleXZ);
		if (scaleY > MAXIMUM_SCALE_Y)
			Destroy (this.gameObject);
	}
}
