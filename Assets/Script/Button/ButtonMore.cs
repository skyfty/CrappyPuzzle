using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMore : MonoBehaviour {

    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject imageBack;
    public GameObject imageCrappyMaze;
    public GameObject textCrappyMaze;
    public GameObject buttonMoreReturn;
    public GameObject buttonMoreGet;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMore()
    {
        if (!gameManager.GetComponent<GameManager>().IsMainStateMenuShow())
            return;
        imageBack.SetActive(true);
        textCrappyMaze.SetActive(true);
        imageCrappyMaze.SetActive(true);
        buttonMoreReturn.SetActive(true);
        buttonMoreGet.SetActive(true);
        soundManager.GetComponent<SoundManager>().PlayClick();
    }
}
