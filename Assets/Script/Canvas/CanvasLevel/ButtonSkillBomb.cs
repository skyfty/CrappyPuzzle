using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSkillBomb : MonoBehaviour {

	public GameObject gameManager;
	public GameObject currentTimeQuantity;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().skillBomb.GetComponent<SkillBomb> ().currentTime != 0.0f) {
            currentTimeQuantity.GetComponent<Quantity>().SetNumber((long)gameManager.GetComponent<GameManager>().player.GetComponent<PlayerShoot>().skillBomb.GetComponent<SkillBomb>().currentTime);
            currentTimeQuantity.SetActive (true);
		} else {
			currentTimeQuantity.SetActive (false);
		}
	}

    private void OnEnable()
    {
        this.GetComponent<RectTransform>().position = gameManager.GetComponent<GameManager>().dataManager.GetComponent<DataManager>().GetButtonSkillBombRectTransformPosition();
    }

    public void Fire () {
		gameManager.GetComponent<GameManager> ().player.GetComponent<PlayerShoot> ().skillBomb.GetComponent<SkillBomb> ().Fire ();
	}
}
