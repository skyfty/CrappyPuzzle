using UnityEngine;

public class DataManager : MonoBehaviour {

    public GameObject anchorButtonSkillBomb;
    public GameObject anchorButtonSkillWave;

    // Use this for initialization
    void Start () {
        anchorButtonSkillBomb.GetComponent<RectTransform>().position = GetButtonSkillBombRectTransformPosition();
        anchorButtonSkillWave.GetComponent<RectTransform>().position = GetButtonSkillWaveRectTransformPosition();
        SetButtonSkillBombDefaultRectTransformPosition();
        SetButtonSkillWaveDefaultRectTransformPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //Bomb default
    public void SetButtonSkillBombDefaultRectTransformPosition()
    {
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillBombDefaultRectTransformPosition.x", anchorButtonSkillBomb.GetComponent<RectTransform>().position.x);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillBombDefaultRectTransformPosition.y", anchorButtonSkillBomb.GetComponent<RectTransform>().position.y);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillBombDefaultRectTransformPosition.z", anchorButtonSkillBomb.GetComponent<RectTransform>().position.z);
    }

    public Vector3 GetButtonSkillBombDefaultRectTransformPosition()
    {
        return new Vector3(
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillBombDefaultRectTransformPosition.x"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillBombDefaultRectTransformPosition.y"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillBombDefaultRectTransformPosition.z"));
    }
    //Bomb record
    public void SetButtonSkillBombRectTransformPosition ()
    {
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.x", anchorButtonSkillBomb.GetComponent<RectTransform>().position.x);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.y", anchorButtonSkillBomb.GetComponent<RectTransform>().position.y);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.z", anchorButtonSkillBomb.GetComponent<RectTransform>().position.z);
    }

    public Vector3 GetButtonSkillBombRectTransformPosition() {
        if (!PlayerPrefs.HasKey("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.x") ||
            !PlayerPrefs.HasKey("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.y") ||
            !PlayerPrefs.HasKey("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.z"))
            SetButtonSkillBombRectTransformPosition();
        return new Vector3(
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.x"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.y"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillBombRectTransformPosition.z"));
    }
    //Wave default
    public void SetButtonSkillWaveDefaultRectTransformPosition()
    {
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillWaveDefaultRectTransformPosition.x", anchorButtonSkillWave.GetComponent<RectTransform>().position.x);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillWaveDefaultRectTransformPosition.y", anchorButtonSkillWave.GetComponent<RectTransform>().position.y);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillWaveDefaultRectTransformPosition.z", anchorButtonSkillWave.GetComponent<RectTransform>().position.z);
    }

    public Vector3 GetButtonSkillWaveDefaultRectTransformPosition()
    {
        return new Vector3(
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillWaveDefaultRectTransformPosition.x"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillWaveDefaultRectTransformPosition.y"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillWaveDefaultRectTransformPosition.z"));
    }
    //Wave record
    public void SetButtonSkillWaveRectTransformPosition()
    {
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.x", anchorButtonSkillWave.GetComponent<RectTransform>().position.x);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.y", anchorButtonSkillWave.GetComponent<RectTransform>().position.y);
        PlayerPrefs.SetFloat("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.z", anchorButtonSkillWave.GetComponent<RectTransform>().position.z);
    }

    public Vector3 GetButtonSkillWaveRectTransformPosition()
    {
        if (!PlayerPrefs.HasKey("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.x") ||
            !PlayerPrefs.HasKey("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.y") ||
            !PlayerPrefs.HasKey("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.z"))
            SetButtonSkillWaveRectTransformPosition();
        return new Vector3(
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.x"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.y"),
                PlayerPrefs.GetFloat("GameManager.CanvasManager.ButtonSkillWaveRectTransformPosition.z"));
    }
}
