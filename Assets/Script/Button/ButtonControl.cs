using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {

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

    public void ShowControl()
    {
        GameObject go = GameObject.Find("Match");
        go.GetComponent<MatchManager>().LoadLevel();
        //if (!gameManager.GetComponent<GameManager>().IsMainStateMenuShow())
        //    return;
        //imageBack.SetActive(true);
        //anchorButtonSkillBomb.SetActive(true);
        //anchorButtonSkillWave.SetActive(true);
        //buttonControlReturn.SetActive(true);
        //buttonControlReset.SetActive(true);
        //soundManager.GetComponent<SoundManager>().PlayClick();
    }
}
