using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMoveZ : MonoBehaviour {

	void Awake () {
		this.GetComponent<RectTransform> ().localScale = new Vector3 (Random.Range (-10, 10), Random.Range (-10, 10), 1);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
