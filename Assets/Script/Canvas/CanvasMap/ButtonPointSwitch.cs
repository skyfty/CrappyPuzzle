using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPointSwitch : MonoBehaviour
{
    public bool isOpen;
    public GameObject target;
    public Sprite openSprite;
    public Sprite closeSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click() {
        if (isOpen) {
            isOpen = false;
            this.GetComponent<Image>().sprite = closeSprite;
            target.GetComponent<ButtonPoint>().Switch();
        } else {
            isOpen = true;
            this.GetComponent<Image>().sprite = openSprite;
            target.GetComponent<ButtonPoint>().Switch();
        }
    }

}
