using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasStory : MonoBehaviour
{
    public GameObject storyImageBack;
    public GameObject storyText;
    public float showTimeInitial;
    private float showTimeCurrent;
    private GameObject currentStoryPoint;
    private int currentTextId;
    // Start is called before the first frame update
    void Start()
    {
        currentTextId = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStory(GameObject tempStoryPoint, bool tempIsShow) {
        currentStoryPoint = tempStoryPoint;
        if (tempIsShow) {
            storyImageBack.SetActive(true);
            storyText.SetActive(true);
            storyText.GetComponent<Text>().text = currentStoryPoint.GetComponent<StoryPoint>().GetText(currentTextId);
        } else {
            storyImageBack.SetActive(false);
            storyText.SetActive(false);
        }
    }

    public void Touch() {
        currentTextId += 1;
        storyText.GetComponent<Text>().text = currentStoryPoint.GetComponent<StoryPoint>().GetText(currentTextId);
        if (storyText.GetComponent<Text>().text=="") {
            //Debug.Log(this.transform.parent.GetComponent<CanvasMatch>().ToActionState(this.gameObject));
            this.transform.parent.GetComponent<CanvasMatch>().ToActionState(currentStoryPoint);
            currentTextId = 0;
        }
    }
}
