using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class ButtonPlay : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject canvasManager;
    public GameObject mainCamera;
    public GameObject match;
    public GameObject office;
    public GameObject text;
    public GameObject player;
    public int currentBaseLevelNumber;
    private string currentBaseLevelName;

    // Start is called before the first frame update
    void Start()
    {
        currentBaseLevelNumber = 1;
        text.GetComponent<Text>().text = "Play "+currentBaseLevelNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void ToMatch() {
        gameManager.GetComponent<GameManager>().SetIsMatch(true);
        LoadBaseLevel();
        //this.transform.parent.gameObject.GetComponent<CanvasOffice>().ToMatch();
        //mainCamera.transform.position = new Vector3(0,10,-2);
        //mainCamera.transform.rotation = Quaternion.Euler(80.0f,0.0f,0.0f);
        //match.SetActive(true);
        //office.SetActive(false);
    }*/

    public void LoadInputLevel() {
        gameManager.GetComponent<GameManager>().SetIsMatch(true);
    }

    public void LoadBaseLevel()
    {
        currentBaseLevelNumber = player.GetComponent<Player>().levelBaseNumber;
        if (currentBaseLevelNumber<10) currentBaseLevelName = "Base.B0000"+(currentBaseLevelNumber+1).ToString();
        else if (currentBaseLevelNumber<100) currentBaseLevelName = "Base.B000"+(currentBaseLevelNumber+1).ToString();
        else if (currentBaseLevelNumber<1000) currentBaseLevelName = "Base.B00"+(currentBaseLevelNumber+1).ToString();
        else if (currentBaseLevelNumber<10000) currentBaseLevelName = "Base.B0"+(currentBaseLevelNumber+1).ToString();
        else if (currentBaseLevelNumber<100000) currentBaseLevelName = "Base.B"+(currentBaseLevelNumber+1).ToString();

        Debug.Log(currentBaseLevelName);
        MatchManager mm = match.GetComponent<MatchManager>();
        
        mm.SetLevelName(currentBaseLevelName);
        mm.LoadLevel(new LoadAssetCallbacks(
               (assetName, asset, duration, userData) =>
               {
                   GetLevelDataResponse e2 = (GetLevelDataResponse)asset;
                   mm.LoadLevel(e2.data);
                   //buttonPlay.GetComponent<ButtonPlay>().ToMatch();
                   gameManager.GetComponent<GameManager>().SetIsMatch(true);
               },
               (assetName, status, errorMessage, userData) =>
               {
                   Log.Error("���عܿ�ʧ��");
               }));
               
    }
}
