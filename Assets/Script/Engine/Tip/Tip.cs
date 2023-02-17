using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour {

	public float riseTime;
	public float riseSpeed;
	public float height;

	public GameObject icon;
	public GameObject sign;
	public GameObject quantity;

	public GameObject tipPosition;
	private GameObject gameManager;
	private Vector3 targetWorldPosition;

	private int riseCount;
    private float tempTotalWidth;

//	public enum IconType {
//		Health = 0,
//		Money = 1
////		IncreaseMoney = 2,
////		ReduceMoney = 3
//	}
//
//	public enum SignType {
//		Plus = 0,
//		Minus = 1,
//		Multiplication = 2,
//		Division = 3
//	}

    private int state;
	private const int STATE_WAIT = 0;
	private const int STATE_VALUE = 1;
	private const int STATE_APPEAR = 2;
	private const int STATE_RISE = 3;
	private const int STATE_DISAPPEAR = 4;

	void Awake () {
		gameManager = GameObject.Find ("GameManager");
		state = STATE_WAIT;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		print (state);
		switch (state) {
		case STATE_WAIT:
			break;
		case STATE_VALUE:
			//this.gameObject.SetActive (true);			//can't active itself
			state = STATE_APPEAR;
			break;
		case STATE_APPEAR:
			this.GetComponent<RectTransform> ().localPosition = new Vector3 (
				(gameManager.GetComponent<GameManager> ().camera.GetComponent<Camera> ().WorldToViewportPoint (targetWorldPosition).x - 0.5f) * gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<RectTransform> ().rect.width,
				(gameManager.GetComponent<GameManager> ().camera.GetComponent<Camera> ().WorldToViewportPoint (targetWorldPosition).y - 0.5f) * gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<RectTransform> ().rect.height + height,
				0);
			riseCount = 0;
			state = STATE_RISE;
			break;
		case STATE_RISE:
			this.GetComponent<RectTransform> ().localPosition = new Vector3 (
				(gameManager.GetComponent<GameManager> ().camera.GetComponent<Camera> ().WorldToViewportPoint (targetWorldPosition).x - 0.5f) * gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<RectTransform> ().rect.width,
				(gameManager.GetComponent<GameManager> ().camera.GetComponent<Camera> ().WorldToViewportPoint (targetWorldPosition).y - 0.5f) * gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<RectTransform> ().rect.height + height,
				0);
			riseCount++;
			this.GetComponent<RectTransform> ().localPosition = new Vector3 (this.GetComponent<RectTransform> ().localPosition.x, this.GetComponent<RectTransform> ().localPosition.y + riseSpeed * riseCount, this.GetComponent<RectTransform> ().localPosition.z);
			riseTime -= Time.deltaTime;
			if (riseTime < 0.0f)
				state = STATE_DISAPPEAR;
			break;
		case STATE_DISAPPEAR:
			Destroy (this.gameObject);
			break;
		}
	}

	public void SetTargetAndValue (GameObject setTarget, long setValue) {
		if (state != STATE_WAIT) {
			print ("The state is not STATE_WAIT!");
			return;
		}
		targetWorldPosition = setTarget.transform.position;		//recond the world position of target.
		quantity.GetComponent<Quantity> ().SetNumber(setValue);
		quantity.GetComponent<Quantity> ().ResetSprite ();
		quantity.GetComponent<Quantity> ().PreparePosition ();		//to prepare the validPlaceCount.
        tempTotalWidth = icon.GetComponent<RectTransform>().sizeDelta.x + sign.GetComponent<RectTransform>().sizeDelta.x + quantity.GetComponent<Quantity>().GetWidth();
        tipPosition.GetComponent<RectTransform>().localPosition = new Vector3(- tempTotalWidth / 2, 0, 0);
        icon.GetComponent<RectTransform>().localPosition = new Vector3(icon.GetComponent<RectTransform>().sizeDelta.x / 2, 0, 0);
        sign.GetComponent<RectTransform>().localPosition = new Vector3(icon.GetComponent<RectTransform>().sizeDelta.x + sign.GetComponent<RectTransform>().sizeDelta.x / 2, 0, 0);
        quantity.GetComponent<RectTransform>().localPosition = new Vector3(icon.GetComponent<RectTransform>().sizeDelta.x + sign.GetComponent<RectTransform>().sizeDelta.x + quantity.GetComponent<Quantity>().GetWidth() / 2, 0, 0);
        state = STATE_VALUE;
	}
}