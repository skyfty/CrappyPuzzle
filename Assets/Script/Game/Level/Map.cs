using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public int width;
	public int height;
	public int baseScale;
	public GameObject wallPrefab;
	private GameObject wallLeft;
	private GameObject wallRight;
	private GameObject wallFront;
	private GameObject wallBehind;
	private GameObject wallFrontLeft;
	private GameObject wallFrontRight;
	private GameObject wallBehindLeft;
	private GameObject wallBehindRight;
	private GameObject[] wall;
	private const int WALL_DIVISOR = 16;
	public GameObject grassPrefab;
	private GameObject[] grass;
	private const int GRASS_DIVISOR = 4;
	private const float ENEMY_HEIGHT = 32.0f;

	private GameObject gameManager;
	private const float MINMIUM_MAXMIUM_DISTANCE_TO_PLAYER = 48.0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		PrepareMap ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrepareMap () {
		wallLeft = Instantiate (wallPrefab) as GameObject;
		wallRight = Instantiate (wallPrefab) as GameObject;
		wallFront = Instantiate (wallPrefab) as GameObject;
		wallBehind = Instantiate (wallPrefab) as GameObject;
		wallFrontLeft = Instantiate (wallPrefab) as GameObject;
		wallFrontRight = Instantiate (wallPrefab) as GameObject;
		wallBehindLeft = Instantiate (wallPrefab) as GameObject;
		wallBehindRight = Instantiate (wallPrefab) as GameObject;
		wallLeft.transform.localScale = new Vector3 (baseScale, baseScale, baseScale*height);
		wallRight.transform.localScale = new Vector3 (baseScale, baseScale, baseScale*height);
		wallFront.transform.localScale = new Vector3 (baseScale*width, baseScale, baseScale);
		wallBehind.transform.localScale = new Vector3 (baseScale*width, baseScale, baseScale);
		wallFrontLeft.transform.localScale = new Vector3 (baseScale, baseScale, baseScale);
		wallFrontRight.transform.localScale = new Vector3 (baseScale, baseScale, baseScale);
		wallBehindLeft.transform.localScale = new Vector3 (baseScale, baseScale, baseScale);
		wallBehindRight.transform.localScale = new Vector3 (baseScale, baseScale, baseScale);
		wallLeft.transform.position= new Vector3 (-baseScale*width/2-baseScale/2, 0.0f, 0.0f);
		wallRight.transform.position= new Vector3 (baseScale*width/2+baseScale/2, 0.0f, 0.0f);
		wallFront.transform.position= new Vector3 (0.0f, 0.0f, -baseScale*height/2-baseScale/2);
		wallBehind.transform.position= new Vector3 (0.0f, 0.0f, baseScale*height/2+baseScale/2);
		wallFrontLeft.transform.position= new Vector3 (-baseScale*width/2-baseScale/2, 0.0f, -baseScale*height/2-baseScale/2);
		wallFrontRight.transform.position= new Vector3 (baseScale*width/2+baseScale/2, 0.0f, -baseScale*height/2-baseScale/2);
		wallBehindLeft.transform.position= new Vector3 (-baseScale*width/2-baseScale/2, 0.0f, baseScale*height/2+baseScale/2);
		wallBehindRight.transform.position= new Vector3 (baseScale*width/2+baseScale/2, 0.0f, baseScale*height/2+baseScale/2);
		wallLeft.transform.parent = this.transform;
		wallRight.transform.parent = this.transform;
		wallFront.transform.parent = this.transform;
		wallBehind.transform.parent = this.transform;
		wallFrontLeft.transform.parent = this.transform;
		wallFrontRight.transform.parent = this.transform;
		wallBehindLeft.transform.parent = this.transform;
		wallBehindRight.transform.parent = this.transform;
		wall = new GameObject[width * height / WALL_DIVISOR];
		for (int i = 0; i < wall.Length; i++) {
			wall [i] = Instantiate (wallPrefab) as GameObject;
			wall [i].transform.localScale = new Vector3 (baseScale, baseScale, baseScale);
			do{
				wall [i].transform.position = new Vector3 (baseScale * Random.Range (-width / 2, width / 2), 0.0f, baseScale * Random.Range (-height / 2, height / 2));
			}while(wall [i].transform.position == Vector3.zero);		//the wall can`t put at (0,0,0), because the player is there.
			wall [i].transform.parent = this.transform;
		}
		grass = new GameObject[width * height / GRASS_DIVISOR];
		for (int i = 0; i < grass.Length; i++) {
			grass [i] = Instantiate (grassPrefab) as GameObject;
			grass [i].transform.position = new Vector3 (Random.Range (-baseScale * width / 2, baseScale * width / 2), 0.0f, Random.Range (-baseScale * height / 2, baseScale * height / 2));
			grass [i].transform.parent = this.transform;
		}
	}

	public Vector3 GetRightPosition (float setMaxmiumDistanceToPlayer) {
		if (setMaxmiumDistanceToPlayer < MINMIUM_MAXMIUM_DISTANCE_TO_PLAYER)
			setMaxmiumDistanceToPlayer = MINMIUM_MAXMIUM_DISTANCE_TO_PLAYER;
		Vector3 tempPosition;
		bool tempRight;
		do{
			tempRight = true;
			tempPosition = new Vector3 (Random.Range (-baseScale * width / 2, baseScale * width / 2), ENEMY_HEIGHT, Random.Range (-baseScale * height / 2, baseScale * height / 2));
			for (int i = 0; i < wall.Length; i++) {
				if ((tempPosition.x > wall [i].transform.position.x - baseScale) &&
					(tempPosition.x < wall [i].transform.position.x + baseScale) &&
					(tempPosition.z > wall [i].transform.position.z - baseScale) &&
					(tempPosition.z < wall [i].transform.position.z + baseScale))
					tempRight = false;
			}
			if ((tempPosition.x < wallLeft.transform.position.x + baseScale) ||
				(tempPosition.x > wallRight.transform.position.x - baseScale) ||
				(tempPosition.z < wallFront.transform.position.z + baseScale) ||
				(tempPosition.z > wallBehind.transform.position.z - baseScale))
				tempRight = false;
			if ((tempPosition.x > gameManager.GetComponent<GameManager>().player.transform.position.x - baseScale) &&
				(tempPosition.x < gameManager.GetComponent<GameManager>().player.transform.position.x + baseScale) &&
				(tempPosition.z > gameManager.GetComponent<GameManager>().player.transform.position.z - baseScale) &&
				(tempPosition.z < gameManager.GetComponent<GameManager>().player.transform.position.z + baseScale))
				tempRight = false;
			if ((tempPosition - gameManager.GetComponent<GameManager>().player.transform.position).magnitude > setMaxmiumDistanceToPlayer)
				tempRight = false;
//			if (tempRight)print ((tempPosition - gameManager.GetComponent<GameManager>().player.transform.position).magnitude);
		}while(tempRight == false);
		return tempPosition;
	}
}