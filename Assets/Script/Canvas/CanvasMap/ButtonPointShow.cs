using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPointShow : MonoBehaviour
{
    [System.Serializable]
    public enum ButtonPointShowType {
        KeepShow,
        ShowToHide,
        HideToShow,
    }
    public ButtonPointShowType buttonPointShowType;
    public int showStoryValue;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        player = this.transform.GetComponent<ButtonPoint>().player.GetComponent<Player>();  //gameManager.GetComponent<GameManager>().
        //Debug.Log(player.storyValue);
        switch(buttonPointShowType) {
            case ButtonPointShowType.KeepShow:
                this.gameObject.SetActive(true);
                break;
            case ButtonPointShowType.ShowToHide:
                if (player.storyValue < showStoryValue) this.gameObject.SetActive(true);
                else this.gameObject.SetActive(false);
                break;
            case ButtonPointShowType.HideToShow:
                if (player.storyValue < showStoryValue) this.gameObject.SetActive(false);
                else this.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
