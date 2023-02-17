using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlReset : MonoBehaviour {

    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject anchorButtonSkillBomb;
    public GameObject anchorButtonSkillWave;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetControl()
    {
        if (!gameManager.GetComponent<GameManager>().IsMainStateMenuShow())
            return;
        anchorButtonSkillBomb.GetComponent<RectTransform>().position = gameManager.GetComponent<GameManager>().dataManager.GetComponent<DataManager>().GetButtonSkillBombDefaultRectTransformPosition();
        anchorButtonSkillWave.GetComponent<RectTransform>().position = gameManager.GetComponent<GameManager>().dataManager.GetComponent<DataManager>().GetButtonSkillWaveDefaultRectTransformPosition();
        gameManager.GetComponent<GameManager>().dataManager.GetComponent<DataManager>().SetButtonSkillBombRectTransformPosition();
        gameManager.GetComponent<GameManager>().dataManager.GetComponent<DataManager>().SetButtonSkillWaveRectTransformPosition();
        soundManager.GetComponent<SoundManager>().PlayClick();
    }
}
