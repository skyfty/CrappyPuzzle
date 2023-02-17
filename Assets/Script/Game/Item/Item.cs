using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //[System.Serializable]
    public int id;
    public int count;
    public Sprite iconSprit;
    public Sprite showSprit;
    //public GameObject button;
    //public GameObject imageIcon;
    //public GameObject textCount;
    public bool isSelect;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initial() {
        
    }

    public void Select() {
        //this.GetComponent<Button>().interactable = false;
    }

    public void Release() {

    }

    public void Click() {
        //if (isSelect==false) {
            //isSelect = true;
            this.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<CanvasMatch>().ItemClick(this.gameObject);
        //} else {

        //}
    }

    public void Initial(int tempCount) {
        count = tempCount;
    }
    /*
    public void Initial(int tempId, Sprite tempIconSprit, Sprite tempShowSprit) {
        id = tempId;
        iconSprit = tempIconSprit;
        showSprit = tempShowSprit;
        this.GetComponent<Image>().sprite = iconSprit;
        this.GetComponent<Image>().SetNativeSize();
        gameManager = GameObject.Find ("GameManager");
        isSelect = false;
    }

    public void Reset() {
        isSelect = false;
    }
    */
}
