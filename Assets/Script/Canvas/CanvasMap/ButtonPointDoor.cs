using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPointDoor : MonoBehaviour
{
    public bool isOpen;
    public int cityId;
    public int roomId;
    public int keyItemId;
    public bool isKeyItemDestroy;
    public string lockText;
    public Sprite openSprite;
    public Sprite closeSprite;
    private GameObject canvasMatch;
    // Start is called before the first frame update
    void Start()
    {
        canvasMatch = this.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<CanvasManager>().canvasMatch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click() {
        if (isOpen) {
            this.transform.parent.transform.parent.transform.parent.GetComponent<CanvasMap>().ToCityRoom(cityId,roomId);
        } else {
            if (canvasMatch.GetComponent<CanvasMatch>().selectItem!=null) {
                if (keyItemId==canvasMatch.GetComponent<CanvasMatch>().selectItem.GetComponent<Item>().id) {
                    canvasMatch.GetComponent<CanvasMatch>().UseItem(this.gameObject);
                    
                    if (isKeyItemDestroy==true) {
                        canvasMatch.GetComponent<CanvasMatch>().ReduceItem(canvasMatch.GetComponent<CanvasMatch>().selectItem.GetComponent<Item>().id,1);
                    }
                } else {
                    canvasMatch.GetComponent<CanvasMatch>().canvasTip.GetComponent<CanvasTip>().ShowText(lockText);
                }
            } else {
                canvasMatch.GetComponent<CanvasMatch>().canvasTip.GetComponent<CanvasTip>().ShowText(lockText);
            }
        }
    }

    public void Open() {
        isOpen = true;
        this.GetComponent<Image>().sprite = openSprite;
    }

    public void Switch() {
        Debug.Log("1");
        if (isOpen) {
            isOpen = false;
            this.GetComponent<Image>().sprite = closeSprite;
            Debug.Log("2");
        } else {
            isOpen = true;
            this.GetComponent<Image>().sprite = openSprite;
            Debug.Log("3");
        }
    }
}
