using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFail : MonoBehaviour
{
    public GameObject gameManager;
	public GameObject soundManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToResult() {
        gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<CanvasManager> ().canvasLevel.GetComponent<CanvasLevel> ().ToResult ();
        soundManager.GetComponent<SoundManager> ().PlayClick ();
    }
}
