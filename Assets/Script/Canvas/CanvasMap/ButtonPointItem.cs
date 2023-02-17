using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPointItem : MonoBehaviour
{
    public bool isOpen;
    public int itemId;
    public int itemCount;
    public int keyItemId;
    public bool isKeyItemDestroy;
    public string lockText;
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
            canvasMatch.GetComponent<CanvasMatch>().GetItem(this.gameObject);
        } else {
            if (canvasMatch.GetComponent<CanvasMatch>().selectItem!=null) {
                if (keyItemId==canvasMatch.GetComponent<CanvasMatch>().selectItem.GetComponent<Item>().id) {
                    isOpen = true;
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

    public Sprite GetShowSpriteByItemNumber() {
        return canvasMatch.GetComponent<CanvasMatch>().gameManager.GetComponent<GameManager>().itemManager.GetComponent<ItemManager>().GetShowSpriteByItemNumber(itemId);
    }

    public void Switch() {
        if (isOpen) {
            isOpen = false;
            //this.GetComponent<Image>().sprite = closeSprite;
        } else {
            isOpen = true;
            //this.GetComponent<Image>().sprite = openSprite;
        }
    }
}
