using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPointTip : MonoBehaviour
{
    public string tipTextKey;
    private GameObject canvasMatch;

    // Start is called before the first frame update
    void Start()
    {
        canvasMatch = this.transform.GetComponent<ButtonPoint>().canvasMatch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click() {
        canvasMatch.GetComponent<CanvasMatch>().canvasTip.GetComponent<CanvasTip>().ShowText(tipTextKey);
    }
}
