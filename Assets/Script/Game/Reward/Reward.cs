using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {

	private GameObject gameManager;
	private GameObject canvasManager;
	private const float GRAVITY_VALUE = -5.0f;
	public PlayerShoot.ChangeType changeType;
	public int changeValue;			//long

	private int state;
	private const int STATE_INITIAL = 0;
	private const int STATE_FALL = 1;
	private const int STATE_WAIT = 2;

	private const float MINIMUM_VELOCITY_Y = 0.1f;

	// Use this for initialization
	void Start () {
		this.GetComponent<BoxCollider>().enabled = false;
		this.GetComponent<CharacterController>().enabled = true;
		state = STATE_INITIAL;
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_INITIAL:
			this.GetComponent<CharacterController> ().Move (new Vector3 (0, GRAVITY_VALUE, 0));
//			print ("this.GetComponent<CharacterController> ().velocity.y "+this.GetComponent<CharacterController> ().velocity.y+" state "+state);
			if (Mathf.Abs(this.GetComponent<CharacterController> ().velocity.y) > MINIMUM_VELOCITY_Y)
				state = STATE_FALL;
			break;
		case STATE_FALL:
			this.GetComponent<CharacterController> ().Move (new Vector3 (0, GRAVITY_VALUE, 0));
			if (Mathf.Abs(this.GetComponent<CharacterController> ().velocity.y) < MINIMUM_VELOCITY_Y) {
				this.GetComponent<BoxCollider>().enabled = true;
				this.GetComponent<CharacterController>().enabled = false;
				state = STATE_WAIT;
			}
			break;
		case STATE_WAIT:
			break;
		}
        this.gameObject.transform.Rotate(0, 1, 0);
	}

	//can't make collision event when only use CharacterController, so must add another trigger to use OnTriggerEnter.
	void OnTriggerEnter(Collider collider) {
		//print (collider.transform.tag);
		//print (collider.transform.name);
		if (collider.transform.tag == "Ground") {
//			Destroy (this.GetComponent("CharacterController"));
			this.GetComponent<BoxCollider>().enabled = true;
			this.GetComponent<CharacterController>().enabled = false;
			state = STATE_WAIT;
		}
		if (collider.transform.name == "Player") {
			Destroy (this.gameObject);
			switch (changeType) {
			case PlayerShoot.ChangeType.IncreaseHealth:
                gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<CanvasManager> ().canvasLevel.GetComponent<CanvasLevel> ().ShowTipIncreaseHealth (this.gameObject, changeValue);
				gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayUpgrade ();     //no playhealth
				break;
			case PlayerShoot.ChangeType.ReduceHealth:
				break;
			case PlayerShoot.ChangeType.IncreaseMoney:
				gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<CanvasManager> ().canvasLevel.GetComponent<CanvasLevel> ().ShowTipIncreaseMoney (this.gameObject, changeValue);
				gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayMoney ();
				break;
			case PlayerShoot.ChangeType.ReduceMoney:
				break;
			}
			gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().Change (changeType, changeValue);
		}
	}

//	private void OnCollisionEnter(Collision collision){
//		print (collision.gameObject.tag);
//		print (collision.gameObject.name);
//		if (collision.gameObject.name == "Player") {
//			Destroy (this.gameObject);
//			player.GetComponent<Player> ().Change (changeType, changeValue);
//		}
//	}

//	private void OnControllerColliderHit(ControllerColliderHit setHit){
//		print (setHit.gameObject.tag);
//		print (setHit.gameObject.name);
//		if (setHit.gameObject.name == "Player") {
//			Destroy (this.gameObject);
//			player.GetComponent<Player> ().Change (changeType, changeValue);
//		}
//	}
}
