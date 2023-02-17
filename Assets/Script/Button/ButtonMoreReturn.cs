using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoreReturn : MonoBehaviour {

    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject imageBack;
    public GameObject imageCrappyMaze;
    public GameObject textCrappyMaze;
    public GameObject buttonMoreReturn;
    public GameObject buttonMoreGet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideMore()
    {
        if (!gameManager.GetComponent<GameManager>().IsMainStateMenuShow())
            return;
        imageBack.SetActive(false);
        textCrappyMaze.SetActive(false);
        imageCrappyMaze.SetActive(false);
        buttonMoreReturn.SetActive(false);
        buttonMoreGet.SetActive(false);
        soundManager.GetComponent<SoundManager>().PlayClick();
    }
}
