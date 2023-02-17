using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<ParticleSystem> ().isStopped)
			Destroy (this.gameObject);
	}
}
