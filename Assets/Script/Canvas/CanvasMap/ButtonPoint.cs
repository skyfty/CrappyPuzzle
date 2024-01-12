using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPoint : MonoBehaviour
{
    public GameObject canvasMatch;
    public GameObject player;
    public GameObject web;
    [System.Serializable]
    public enum ButtonPointType {
        Door,
        Tip,
        Item,
        Switch,
        Password,
    }
    public ButtonPointType buttonPointType;

    public int id;
    /*
    public bool isActive;
    public bool isInitialShow;
    public int showStoryValue;
    public GameObject targetObject;
    public int targetValue;
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isActive==false) {
            this.gameObject.SetActive(false);
        } else {
            if (isInitialShow) {
                if (player.GetComponent<Player>().storyValue>showStoryValue) this.gameObject.SetActive(false);
                else this.gameObject.SetActive(true);
            } else {
                if (player.GetComponent<Player>().storyValue>showStoryValue) this.gameObject.SetActive(true);
                else this.gameObject.SetActive(false);
            }
            switch (buttonPointType) {
                case ButtonPointType.Door:
                    break;
                case ButtonPointType.Tip:
                    break;
                case ButtonPointType.Item:
                    break;
                case ButtonPointType.Switch:
                    break;
                default:
                    break;
            }
        }
        */
    }

    public void Click() {

            switch (buttonPointType) {
                case ButtonPointType.Door:
                    this.transform.GetComponent<ButtonPointDoor>().Click();
                    break;
                case ButtonPointType.Tip:
                    this.transform.GetComponent<ButtonPointTip>().Click();
                    break;
                case ButtonPointType.Item:
                    this.transform.GetComponent<ButtonPointItem>().Click();
                    break;
                case ButtonPointType.Switch:
                    
                    break;
                case ButtonPointType.Password:
                    this.transform.GetComponent<ButtonPointPassword>().Click();
                    break;
                default:
                    break;
            }
        

    }
    public void Switch() {

            switch (buttonPointType) {
                case ButtonPointType.Door:
                    this.transform.GetComponent<ButtonPointDoor>().Switch();
                    break;
                case ButtonPointType.Tip:
                    //this.transform.GetComponent<ButtonPointTip>().Switch();
                    break;
                case ButtonPointType.Item:
                    this.transform.GetComponent<ButtonPointItem>().Switch();
                    break;
                case ButtonPointType.Switch:
                    
                    break;
                case ButtonPointType.Password:
                    break;
                default:
                    break;
            }
    }
}