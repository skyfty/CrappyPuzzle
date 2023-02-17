using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPointPassword : MonoBehaviour
{
    public GameObject pad;
    public string rightPassword;
    public GameObject taget;
    public ButtonPoint.ButtonPointType buttonPointType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click() {
        pad.SetActive(true);
        pad.GetComponent<CanvasPassword>().Initial(this.gameObject,rightPassword);
    }

    public void Switch() {
        
        switch (buttonPointType) {
            case ButtonPoint.ButtonPointType.Door:
                taget.transform.GetComponent<ButtonPointDoor>().Switch();
                Debug.Log("sdsd");
                break;
            case ButtonPoint.ButtonPointType.Tip:
                //this.transform.GetComponent<ButtonPointTip>().Switch();
                break;
            case ButtonPoint.ButtonPointType.Item:
                taget.transform.GetComponent<ButtonPointItem>().Switch();
                break;
            case ButtonPoint.ButtonPointType.Switch:
                //
                break;
            case ButtonPoint.ButtonPointType.Password:
                //taget.transform.GetComponent<ButtonPointPassword>().Switch();
                break;
            default:
                break;
        }
    }
}
