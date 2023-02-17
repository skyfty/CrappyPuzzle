using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPassword : MonoBehaviour
{
    public string rightPassword;
    public string currrentPassword;
    public GameObject text;
    private float errorTime;
    private GameObject targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        currrentPassword = "";
        text.GetComponent<Text>().text = currrentPassword;
    }

    // Update is called once per frame
    void Update()
    {
        if (errorTime > 0.0f) {
            errorTime -= Time.deltaTime;
            if (errorTime < 0.0f) text.GetComponent<Text>().text = currrentPassword;
        }
    }

    public void Initial(GameObject tempPoint, string tempRightPassword) {
        targetPoint = tempPoint;
        rightPassword = tempRightPassword;
    }

    public void InputNumber(int tempNumber) {
        if (currrentPassword.Length < 6) {
            currrentPassword = currrentPassword + tempNumber.ToString();
            text.GetComponent<Text>().text = currrentPassword;
        }
    }

    public void Delete() {
        if (currrentPassword.Length==0) {

        } else {
            currrentPassword = currrentPassword.Remove(currrentPassword.Length-1);
            text.GetComponent<Text>().text = currrentPassword;
            //if (currrentPassword==0) text.GetComponent<Text>().text = "";
            //else text.GetComponent<Text>().text = currrentPassword.ToString();
        }
    }

    public void Return(){
        this.gameObject.SetActive(false);
    }

    public void Enter() {
        if (string.Compare(currrentPassword,rightPassword)==0) {
            Debug.Log("df");
            this.gameObject.SetActive(false);
            if (targetPoint!=null) targetPoint.GetComponent<ButtonPointPassword>().Switch();
        } else {
            text.GetComponent<Text>().text = "Error!";
            errorTime = 1.0f;
        }
    }

}
