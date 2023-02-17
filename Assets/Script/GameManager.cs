using UnityEngine;
using GameFramework.Event;
using GameFramework.Resource;
using UnityGameFramework.Runtime;
using Assets.Script.Data;


public class GameManager : MonoBehaviour
{

    public GameObject canvasManager;
    public GameObject languageManager;
    public GameObject levelManager;
    public GameObject soundManager;
    public GameObject dataManager;
    public GameObject itemManager;

    public GameObject player;
    public GameObject camera;
    public GameObject office;
    public GameObject match;

    private const int STATE_INITIAL = 0;
    private const int STATE_LANGUAGE = 1;
    private const int STATE_TITLE = 2;
    private const int STATE_OFFICE = 3;
    private const int STATE_MATCH = 4;
    private const int STATE_SITE = 5;
    private const int STATE_MENU = 93;

    private const int STATE_LEVEL = 94;

    private const int STATE_RESULT = 95;

    private const int STATE_PAUSE = 95;

    //private bool isLoad;
    private bool isStart;
    private bool isMatch;
    private bool isOffice;

    private int mainState = 0;
    //private int branchState = 0;

    private int currentLevelNumber;
    private int finishLevelNumber = 0;



    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        mainState = STATE_INITIAL;
        Framework.InitBuiltinComponents();
    }


    void Login()
    {
        User user = GetComponent<User>();
        user.Login("18910600417", "123456", new LoginCallbacks((data) =>
        {
            data.omnipotence = 100;
            user.SetData(data);
        },
        (int code, string errorMessage) =>
        {
            Log.Error(errorMessage);
        }));

    }

    // Update is called once per frame
    void Update()
    {
        //print (mainState);
        switch (mainState)
        {
            case STATE_INITIAL:
                //isLoad = false;
                isStart = false;
                isMatch = false;
                isOffice = false;
                canvasManager.GetComponent<CanvasManager>().InitialToTitle();       //must before Framework.Event.Subscribe.
                Framework.Event.Subscribe(WebRequestSuccessEventArgs.EventId, OnWebRequestSuccess);
                Framework.Event.Subscribe(WebRequestFailureEventArgs.EventId, OnWebRequestFailure);
                mainState = STATE_TITLE;
                break;
            case STATE_TITLE:
                if (isStart==true) {
                    TitleToOffice();
                    ResetIsFalse();
                    mainState = STATE_OFFICE;
                }
                break;
            case STATE_OFFICE:
                if (isMatch==true) {
                    OfficeToMatch();
                    ResetIsFalse();
                    mainState = STATE_MATCH;
                }
                break;
            case STATE_MATCH:
                if (isOffice==true) {
                    MatchToOffice();
                    ResetIsFalse();
                    mainState = STATE_OFFICE;
                }
                break;
            case STATE_LANGUAGE:
                if (canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromLanguageToTitle())
                {
                    string confirmText = Framework.Localization.GetString("Dialog.ConfirmButton");


                    canvasManager.GetComponent<CanvasManager>().canvasMenu.GetComponent<CanvasMenu>().buttonLevelManager.GetComponent<ButtonLevelManager>().SetLevel(
                        levelManager.GetComponent<LevelManager>().GetCurrentLevelNumber(),
                        levelManager.GetComponent<LevelManager>().GetFinishLevelNumber());
                    canvasManager.GetComponent<CanvasManager>().canvasMenu.GetComponent<CanvasMenu>().buttonLevelManager.GetComponent<ButtonLevelManager>().SetButtonLevel();
                    canvasManager.GetComponent<CanvasManager>().canvasMenu.GetComponent<CanvasMenu>().buttonLevelManager.GetComponent<ButtonLevelManager>().PrepareButton();
                    mainState = STATE_TITLE;
                    Login();
                }
                break;
            
            
            
            case STATE_MENU:
                if (canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromMenuToLevel())
                {
                    levelManager.GetComponent<LevelManager>().PlaySelectLevel();
                    player.GetComponent<PlayerShoot>().Reset();
                    soundManager.GetComponent<SoundManager>().PlayMenu();
                    mainState = STATE_LEVEL;
                }
                break;
            case STATE_LEVEL:
                if (canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromLevelToResult())
                {
                    canvasManager.GetComponent<CanvasManager>().canvasMenu.GetComponent<CanvasMenu>().buttonLevelManager.GetComponent<ButtonLevelManager>().SetLevel(
                        levelManager.GetComponent<LevelManager>().GetCurrentLevelNumber(),
                        levelManager.GetComponent<LevelManager>().GetFinishLevelNumber());
                    canvasManager.GetComponent<CanvasManager>().canvasMenu.GetComponent<CanvasMenu>().buttonLevelManager.GetComponent<ButtonLevelManager>().SetButtonLevel();
                    canvasManager.GetComponent<CanvasManager>().canvasMenu.GetComponent<CanvasMenu>().buttonLevelManager.GetComponent<ButtonLevelManager>().PrepareButton();
                    levelManager.GetComponent<LevelManager>().DestroySelectLevel();
                    player.GetComponent<PlayerShoot>().Win();
                    GetComponent<User>().Pass("2");
                    mainState = STATE_RESULT;
                }
                break;
            case STATE_RESULT:
                if (canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromResultToMenu())
                {
                    mainState = STATE_MENU;
                }
                break;
            default:
                break;
        }
    }

    public void PlayLevel(int setNumber)
    {
        print("here");
        if (mainState != STATE_MENU)
            return;
        //set canvas
        currentLevelNumber = setNumber;
        canvasManager.GetComponent<CanvasManager>().buttonLevelManager.GetComponent<ButtonLevelManager>().SetLevel(currentLevelNumber, finishLevelNumber);
        //set currentLevel
        levelManager.GetComponent<LevelManager>().PlaySelectLevel();
        if (canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromMenuToLevel())
            mainState = STATE_LEVEL;
    }

    public bool IsStateLevel()
    {
        if (mainState == STATE_LEVEL)
            return true;
        return false;
    }

    public bool IsMainStateMenuShow()
    {
        if (mainState == STATE_MENU)
            return true;
        return false;
    }

    public void LoadLevel(string name, LoadAssetCallbacks loadAssetCallbacks)
    {
        DownloadLevel(name, loadAssetCallbacks);
    }

    public void DownloadLevel(string name, LoadAssetCallbacks loadAssetCallbacks)
    {
        string url = GameFramework.Utility.Text.Format("{0}/level/get?name={1}", Framework.OssApiUrl, name);
        Framework.WebRequest.AddWebRequest(url, (object)loadAssetCallbacks);
    }



    private void OnWebRequestFailure(object sender, GameEventArgs e)
    {
        WebRequestFailureEventArgs args = (WebRequestFailureEventArgs)e;
        if (args.UserData is LoadAssetCallbacks)
        {
            LoadAssetFailureCallback callback = ((LoadAssetCallbacks)args.UserData).LoadAssetFailureCallback;
            callback.Invoke("", LoadResourceStatus.NotExist, "", null);
        }
        else if (args.UserData is ResponseCallbacks)
        {
            ResponseCallbacks callback = (ResponseCallbacks)args.UserData;
            callback.FailureCallback(-1, "web request failure");
        }
    }

    private void OnWebRequestSuccess(object sender, GameEventArgs e)
    {
        WebRequestSuccessEventArgs args = (WebRequestSuccessEventArgs)e;
        string strTemp = GameFramework.Utility.Converter.GetString(args.GetWebResponseBytes());
        if (args.UserData is LoadAssetCallbacks)
        {
            GetLevelDataResponse e2 = GameFramework.Utility.Json.ToObject<GetLevelDataResponse>(strTemp);
            if (e2.code == 1)
            {
                LoadAssetSuccessCallback callback = ((LoadAssetCallbacks)args.UserData).LoadAssetSuccessCallback;
                callback.Invoke("", e2, 0, null);
            }
            else
            {
                LoadAssetFailureCallback callback = ((LoadAssetCallbacks)args.UserData).LoadAssetFailureCallback;
                callback.Invoke("", LoadResourceStatus.NotExist, "", null);
            }

        }
        else if (args.UserData is ResponseCallbacks)
        {
            ResponseCallbacks callback = (ResponseCallbacks)args.UserData;
            callback.SuccessCallback(strTemp);
        }
    }

    public void TitleToOffice() {
        canvasManager.GetComponent<CanvasManager>().TitleToOffice();
        camera.transform.position = new Vector3(0,1,-5);
        camera.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        match.SetActive(false);
        office.SetActive(true);
        canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromMatchToOffice();
    }

    public void OfficeToMatch() {
        canvasManager.GetComponent<CanvasManager>().OfficeToMatch();
        camera.transform.position = new Vector3(0,10,-2);
        camera.transform.rotation = Quaternion.Euler(80.0f,0.0f,0.0f);
        match.SetActive(true);
        office.SetActive(false);
        canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromOfficeToMatch();
    }

    public void MatchToOffice() {
        canvasManager.GetComponent<CanvasManager>().MatchToOffice();
        camera.transform.position = new Vector3(0,1,-5);
        camera.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        match.SetActive(false);
        office.SetActive(true);
        canvasManager.GetComponent<CanvasManager>().ChangeMainStateFromMatchToOffice();
    }

    public void ResetIsFalse() {
        isStart = false;
        //isLoad = false;
        isStart = false;
        isMatch = false;
        isOffice = false;
    }

    public void SetIsStart(bool tempIsStart) {
        isStart = tempIsStart;
    }

    public void SetIsMatch(bool tempIsMatch) {
        isMatch = tempIsMatch;
    }

    public void SetIsOffice(bool tempIsOffice) {
        isOffice = tempIsOffice;
    }

}