using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPoint : MonoBehaviour
{
    public GameObject canvasMatch;
    public GameObject player;
    public int storyValue;
    public int changeStoryValue;
    public string[] text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (storyValue == player.GetComponent<Player>().storyValue) {
            canvasMatch.GetComponent<CanvasMatch>().ToStoryState(this.gameObject);
            ShowStory(true);
        }
    }

    public void ShowStory(bool tempIsShow) {
        if (tempIsShow) {

        } else {

        }
    }

    public void ChangeStoryValue() {

    }

    public string GetText (int tempTextId) {
        if (tempTextId < text.Length) {
            return text[tempTextId];
        }else return null;
    }
}
