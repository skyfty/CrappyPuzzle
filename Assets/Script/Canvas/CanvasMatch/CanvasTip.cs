using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTip : MonoBehaviour
{   
    public GameObject tipImageBack;
    public GameObject tipText;
    public float showTimeInitial;
    private float showTimeCurrent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showTimeCurrent > 0.0f) {
            showTimeCurrent -= Time.deltaTime;
            if (showTimeCurrent < 0.0f) {
                tipImageBack.SetActive(false);
                tipText.SetActive(false);
            } else {

            }
        }
    }

    public void ShowText(string tempText) {
        tipText.GetComponent<Text>().text = tempText;
        tipImageBack.SetActive(true);
        tipText.SetActive(true);
        showTimeCurrent = showTimeInitial;
    }
}
