using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class FrameworkText : MonoBehaviour
{
    public string key;
    public GameObject languageManager;
    // Start is called before the first frame update
    void Start()
    {
        Framework.InitBuiltinComponents();
        Framework.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
        Framework.Event.Subscribe(LoadDictionaryUpdateEventArgs.EventId, OnLoadDictionarySuccess);
        if (Framework.Localization.HasRawString(key))
        {
            Framework.Event.Fire(this, new LoadDictionarySuccessEventArgs());
        } 
    }


    private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
    {
        if (key.Length > 0 && Framework.Localization.HasRawString(key))
        {
            this.GetComponent<Text>().text = Framework.Localization.GetString(key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
