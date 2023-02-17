using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroType : MonoBehaviour {

	public GameObject ufoHead;
	public GameObject ufoBody;

	public GameObject[] gun;

	public int maximumLevel;
	public int cost;			//play can pay this cost to get the ufo type
	public int[] health;
	public float[] speed;
	public int[] attack;
	public int[] defend;
	public int[] healthCost;
	public int[] speedCost;
	public int[] attackCost;
	public int[] defendCost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
