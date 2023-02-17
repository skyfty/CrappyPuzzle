using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkillWave : MonoBehaviour {

	public GameObject gameManager;
	public GameObject currentTimeQuantity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().skillWave.GetComponent<SkillWave> ().currentTime != 0.0f) {
			currentTimeQuantity.SetActive (true);
			currentTimeQuantity.GetComponent<Quantity> ().SetNumber ((long)gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().skillWave.GetComponent<SkillWave> ().currentTime);
		} else {
			currentTimeQuantity.SetActive (false);
		}
	}

    private void OnEnable()
    {
        this.GetComponent<RectTransform>().position = gameManager.GetComponent<GameManager>().dataManager.GetComponent<DataManager>().GetButtonSkillWaveRectTransformPosition();
    }

    public void Fire () {
		gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().skillWave.GetComponent<SkillWave> ().Fire ();
	}
}
