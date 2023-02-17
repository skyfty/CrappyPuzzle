using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlReturn : MonoBehaviour {

    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject imageBack;
    public GameObject anchorButtonSkillBomb;
    public GameObject anchorButtonSkillWave;
    public GameObject buttonControlReturn;
    public GameObject buttonControlReset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideControl()
    {
        if (!gameManager.GetComponent<GameManager>().IsMainStateMenuShow())
            return;
        imageBack.SetActive(false);
        anchorButtonSkillBomb.SetActive(false);
        anchorButtonSkillWave.SetActive(false);
        buttonControlReturn.SetActive(false);
        buttonControlReset.SetActive(false);
        soundManager.GetComponent<SoundManager>().PlayClick();
    }
}
