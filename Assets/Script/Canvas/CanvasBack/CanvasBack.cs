using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBack : MonoBehaviour {

    public GameObject prefabBackCell;
    public GameObject[] backCell;

    //private int state;
    private const int STATE_SHOW = 0;
    private const int STATE_LEVEL = 1;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < backCell.Length; i++)
        {
            backCell[i] = Instantiate(prefabBackCell) as GameObject;
            backCell[i].transform.SetParent(this.transform);
            backCell[i].GetComponent<RectTransform>().localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            backCell[i].GetComponent<RectTransform>().localScale = Vector3.one;
        }
        //state = STATE_SHOW;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
