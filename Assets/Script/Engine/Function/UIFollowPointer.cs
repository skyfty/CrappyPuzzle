using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowPointer : MonoBehaviour {

    public GameObject canvasManager;
    private float localPositionZBeforeDrag;

    // Use this for initialization
    void Start () {
        localPositionZBeforeDrag = this.gameObject.GetComponent<RectTransform>().transform.localPosition.z;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FollowPointer () {
        this.gameObject.GetComponent<RectTransform>().transform.localPosition = new Vector3(
            canvasManager.GetComponent<RectTransform>().rect.width * (canvasManager.GetComponent<Canvas>().worldCamera.ScreenToViewportPoint(Input.mousePosition).x - 0.5f),
            canvasManager.GetComponent<RectTransform>().rect.height * (canvasManager.GetComponent<Canvas>().worldCamera.ScreenToViewportPoint(Input.mousePosition).y - 0.5f),
            localPositionZBeforeDrag);
    }
}
