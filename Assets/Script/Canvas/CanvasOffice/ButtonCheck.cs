using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class ButtonCheck : MonoBehaviour
{
    public GameObject inputField;
    public GameObject buttonPlay;
    public GameObject match;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckLevel()
    {
        MatchManager mm = match.GetComponent<MatchManager>();
        mm.SetLevelName(inputField.GetComponent<InputField>().text);
        mm.LoadLevel(new LoadAssetCallbacks(
               (assetName, asset, duration, userData) =>
               {
                   GetLevelDataResponse e2 = (GetLevelDataResponse)asset;
                   mm.LoadLevel(e2.data);
                   buttonPlay.GetComponent<ButtonPlay>().LoadInputLevel();
               },
               (assetName, status, errorMessage, userData) =>
               {
                   Log.Error("���عܿ�ʧ��");
               }));
    }
}
