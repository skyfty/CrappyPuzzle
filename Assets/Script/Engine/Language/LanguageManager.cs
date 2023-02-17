using GameFramework.Localization;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

public class LanguageManager : MonoBehaviour {

	private int state;
	private const int STATE_NOT_SET = 0;
	private const int STATE_SETTED = 1;
	private const int STATE_SAVED = 2;

	public GameObject[] languageImageList;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetLanguage (Language language) {
		for (int i = 0; i < languageImageList.Length; i++) {
			languageImageList [i].GetComponent<LanguageImage> ().SetLanguage ();
		}
		state = STATE_SETTED;
        Framework.Localization.Language = language;
        Framework.Localization.RemoveAllRawStrings();
        Framework.Localization.ReadData(AssetUtility.GetDictionaryAsset("Default", false), this);
        Framework.InitCurrentVariant();
    }

    public void SaveLanguage (){
		if (state != STATE_SETTED) {
			print ("Save language wrong, the state is not STATE_SETTED.");
			return;
		}
        Framework.Setting.SetString(Framework.LanguageConfigName, Framework.Localization.Language.ToString());
        Framework.Setting.Save();
        state = STATE_SAVED;
	}

	public bool IsSaved () {
		if (state == STATE_SAVED) {
			return true;
		}
		return false;
	}

	public void NotSet () {
		if (state != STATE_SETTED) {
			print ("Not Set language wrong, the state is not STATE_SETTED.");
			return;
		}
		state = STATE_NOT_SET;
	}

}
