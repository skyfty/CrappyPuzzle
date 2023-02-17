using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quantity : MonoBehaviour {

	public GameObject quantityPosition;
	public int validPlaceCount;
	[System.Serializable]
	public enum StayDirection
	{
		Left = 0,
		Center = 1,
		Right = 2,
	}
	public StayDirection stayDirection;
	public GameObject[] place;
	public Sprite[] numberSprit;
	public float placePositionXInterval;

	public long currentNumber;

	public bool isEnlarge;
	public float enlargeSpeed;
	public float enlargeMaximum;
	private float currentEnlarge;
	public float enlargeKeepTime;
	private float currentEnlargeKeepTime;

	private Vector3 targetPosition;

	private const int STATE_WAIT = 0;
	private const int STATE_SET_NUMBER = 1;
	private const int STATE_ADD_NUMBER = 2;
	private const int STATE_REDUCE_NUMBER = 3;
	private const int STATE_ENLARGE = 4;
	private const int STATE_KEEP = 5;
	private const int STATE_MINIFY = 6;
	private const int STATE_STOP = 7;
	private int state;

	private const long MAX_NUMBER = 999999999999999999;
	private const long MIN_NUMBER = 0;

	// Use this for initialization
	void Start () {
//		PreparePosition ();
//		ResetSprite ();
//		currentEnlarge = 1;
//		currentEnlargeKeepTime = enlargeKeepTime;
		state = STATE_WAIT;
	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case STATE_WAIT:
			break;
		case STATE_SET_NUMBER:
			ResetSprite ();
			PreparePosition ();
			if (isEnlarge) {
				currentEnlarge = 1;
				currentEnlargeKeepTime = enlargeKeepTime;
				state = STATE_ENLARGE;
			} else {
				state = STATE_STOP;
			}
			break;
		case STATE_ADD_NUMBER:
			ResetSprite ();
			PreparePosition ();
			currentEnlarge = 1;
			currentEnlargeKeepTime = enlargeKeepTime;
			state = STATE_ENLARGE;
			break;
		case STATE_REDUCE_NUMBER:
			ResetSprite ();
			PreparePosition ();
			currentEnlarge = 1;
			currentEnlargeKeepTime = enlargeKeepTime;
			state = STATE_ENLARGE;
			break;
		case STATE_ENLARGE:
			currentEnlarge += enlargeSpeed;
			if (currentEnlarge > enlargeMaximum) {
				currentEnlarge = enlargeMaximum;
				state = STATE_KEEP;
			}
			quantityPosition.transform.localScale = new Vector3(currentEnlarge, currentEnlarge, 1);
			break;
		case STATE_KEEP:
			currentEnlargeKeepTime -= Time.deltaTime;
			if (currentEnlargeKeepTime < 0) {
				state = STATE_MINIFY;
			}
			break;
		case STATE_MINIFY:
			currentEnlarge -= enlargeSpeed;
			if (currentEnlarge < 1) {
				currentEnlarge = 1;
				state = STATE_STOP;
			}
			quantityPosition.transform.localScale = new Vector3(currentEnlarge, currentEnlarge, 1);
			break;
		case STATE_STOP:
			state = STATE_WAIT;
			break;
		default:
			break;
		}
	}

	public void SetNumber (long setSetNumber){
		currentNumber = setSetNumber;
		if (currentNumber > MAX_NUMBER) {
			currentNumber = MAX_NUMBER;
		}
		if (currentNumber < MIN_NUMBER) {
			currentNumber = MIN_NUMBER;
		}
		state = STATE_SET_NUMBER;
	}

	public void AddNumber (long setAddNumber){
		currentNumber += setAddNumber;
		if (currentNumber > MAX_NUMBER) {
			currentNumber = MAX_NUMBER;
		}
		state = STATE_ADD_NUMBER;
	}

	public void ReduceNumber (long setReduceNumber){
		currentNumber -= setReduceNumber;
		if (currentNumber < MIN_NUMBER) {
			currentNumber = MIN_NUMBER;
		}
		state = STATE_REDUCE_NUMBER;
	}

	public void ResetSprite (){
		long tempPlaceNumber;
		
		for (int i = 0; i < place.Length; i++) {
			if (i == 0) {
				tempPlaceNumber = currentNumber % 10;
			} else {
				tempPlaceNumber = (currentNumber / (long)System.Math.Pow(10,i)) % 10;
			}
			place [i].GetComponent<Image> ().sprite = numberSprit[tempPlaceNumber];
			place [i].SetActive (true);
		}

		for (int i = place.Length - 1; i > 0; i--) {
			if (place [i].GetComponent<Image> ().sprite != numberSprit [0]) {
				break;
			} else {
				place [i].SetActive (false);
			}
		}
	}

	public void PreparePosition (){
		if (currentNumber == 0) {
			validPlaceCount = 1;
		} else {
//			currentNumber = 1;
//			if (this.name == "QuantityCapital") print ("currentNumber "+currentNumber);
			for (int i = place.Length; i > 0; i--) {
//				if (this.name == "QuantityCapital") print ("i " + i + "  (long)System.Math.Pow(10,i-1)  " + (long)System.Math.Pow (10, i - 1));
//				if (this.name == "QuantityCapital") print ("(currentNumber / (long)System.Math.Pow(10,i-1) "+(currentNumber / (long)System.Math.Pow(10,i-1)));
				if ((currentNumber / (long)System.Math.Pow(10,i-1)) != 0) {
					validPlaceCount = i;
//					if (this.name == "QuantityCapital") print ("validPlaceCount " + validPlaceCount);
					break;
				}
			}
		}
		switch (stayDirection) {
		case StayDirection.Left:
			quantityPosition.transform.localPosition = new Vector3 ((placePositionXInterval/2 * validPlaceCount),0,0);
			for (int i = 0; i < place.Length; i++) {
				place [i].transform.localPosition = new Vector3 (((-placePositionXInterval * i) + (placePositionXInterval/2 * validPlaceCount) + (-placePositionXInterval/2)), 0, 0);
//				print (i + " " + place [i].transform.localPosition.x);
			}
			break;
		case StayDirection.Center:
			quantityPosition.transform.localPosition = Vector3.zero;
			for (int i = 0; i < place.Length; i++) {
				place [i].transform.localPosition = new Vector3 ((-placePositionXInterval * i) - (-placePositionXInterval/2 * validPlaceCount) + (-placePositionXInterval/2), 0, 0);
			}
			break;
		case StayDirection.Right:
			quantityPosition.transform.localPosition = new Vector3 (-(placePositionXInterval/2 * validPlaceCount),0,0);
			for (int i = 0; i < place.Length; i++) {
				place [i].transform.localPosition = new Vector3 (-((placePositionXInterval * i) - (placePositionXInterval/2 * validPlaceCount) + (placePositionXInterval/2)), 0, 0);
			}
			break;
		default:
			break;
		}
	}

    public float GetWidth()
    {
        return placePositionXInterval * validPlaceCount;
    }
}
