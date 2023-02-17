using Assets.Script.Data;
using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using DG.Tweening;
using GameFramework.Event;
using GameFramework;

public class GetLevelDataResponse : PokeResponse
{
    public Occasion data;
};

public class GetLevelDataEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(GetLevelDataEventArgs).GetHashCode();

    public Occasion Data
    {
        get;
        private set;
    }

    public static GetLevelDataEventArgs Create(GetLevelDataResponse e)
    {
        GetLevelDataEventArgs args = ReferencePool.Acquire<GetLevelDataEventArgs>();
        args.Data = e.data;
        return args;
    }

    /// <summary>
    /// 获取 Web 请求成功事件编号。
    /// </summary>
    public override int Id
    {
        get
        {
            return EventId;
        }
    }

    public override void Clear()
    {
    }
}

public class MatchManager : MonoBehaviour
{
    public GameObject[] prefabCard;
    public GameObject freeCardGroup;
    public GameObject hideCardGroup;
    public GameObject currentCardGroup;
    public GameObject gameManager;
    public GameObject player;
    public GameObject[] prefabEffect;

    private const int MATCH_TYPE_NORMAL = 0;
    private const int MATCH_TYPE_BOSS = 1;
    private int matchType = 0;
    private int matchState = 0;
    private const int MATCH_STATE_LOAD = 0;
    private const int MATCH_STATE_INITIAL = 1;
    private const int MATCH_STATE_PLAY = 2;
    private const int MATCH_STATE_LOADING_LEVEL = 3;
    private const int MATCH_STATE_ENTRY_LEVEL = 4;
    private const int MATCH_STATE_CONGRATULATE = 8;
    private const int MATCH_STATE_RESULT = 5;
    private const int MATCH_STATE_QUIT = 6;
    private const int MATCH_STATE_END = 7;
    private int levelNumber;

    private GameObject currentCard;
    //private int currentCardType;
    //private int currentCardNumber;

    private GameObject[] hideCard = new GameObject[16];

    private GameObject clickCard;
    private bool isClickCard;
    //private int clickCardType;
    //private int clickCardNumber;
    private int clickCardGroup;
    private bool clickCardIsHide;

    public GameObject canvasWin;
    public GameObject canvasFail;
    public GameObject canvasMatch;
    private const float congratulateTime = 1.0f;
    private float currentCongratulateTime;

    private const int MAX_HEIGHT = 1080;

    private const float pokeWidth = 2f;
    private const float pokeSpan = pokeWidth / 2;
    private const float pokeHeight = 15f;
    private Vector3 currentStandardPosition = new Vector3(3.72f, 0.0f, -4.2f);

    private Tweener publicTweener;
    public Camera mainCamera;

    public bool isLevelCheck;
    public bool isLevelHave;
    public string levelName;
    public string levelId;
    private float positionXZScale = 72.0f;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        levelNumber = 0;
        matchState = MATCH_STATE_LOAD;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(matchState);
        switch (matchState)
        {
            case MATCH_STATE_LOAD:
                if (levelNumber == 0) matchType = MATCH_TYPE_NORMAL;
                else matchState = MATCH_TYPE_BOSS;
                matchState = MATCH_STATE_INITIAL;
                break;
            case MATCH_STATE_INITIAL:
                switch (matchType)
                {
                    case MATCH_TYPE_NORMAL:
                        break;
                    case MATCH_TYPE_BOSS:
                        break;
                }
                matchState = MATCH_STATE_LOADING_LEVEL;
                break;
            case MATCH_STATE_LOADING_LEVEL:
                {
                    matchState = MATCH_STATE_ENTRY_LEVEL;
                    break;
                }
            case MATCH_STATE_ENTRY_LEVEL:
                {
                    matchState = MATCH_STATE_PLAY;
                    break;
                }
            case MATCH_STATE_PLAY:
                if (isClickCard==true) {
                    
                } else {
                    if (Input.GetMouseButtonDown(0)) {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo)) {
                            Debug.Log(hitInfo.transform.name);
                            if (hitInfo.transform.GetComponent<Card>() != null) {
                                clickCard = hitInfo.transform.gameObject;
                                isClickCard = true;
                                clickCardGroup = hitInfo.transform.GetComponent<Card>().GetCardGroup();
                                clickCardIsHide = hitInfo.transform.GetComponent<Card>().GetCardIsHide();
                                Debug.Log("hit");
                            }
                        }
                    }
                    if (isClickCard) {
                        Debug.Log("clickCardGroup "+clickCardGroup);
                        switch (clickCardGroup) {
                            case 0: //current
                                isClickCard = false;
                                break;
                            case 1: //hide
                                if (hideCard[0]!=null) {
                                    for (int i=0;i<hideCard.Length-1;i++) {
                                        if (hideCard[i+1]!=null) hideCard[i+1].transform.DOLocalMove(hideCard[i].transform.localPosition, 0.5f);
                                        else break;
                                    }
                                    hideCard[0].transform.DOLocalRotate(Vector3.zero, 0.5f);
                                    publicTweener = hideCard[0].transform.DOLocalMove(currentStandardPosition, 0.5f);
                                    publicTweener.OnComplete(ChangeCurrentCardToHideCard);
                                }
                                break;
                            case 2: //free
                                bool isMove = false;
                                if (clickCard.GetComponent<Card>().GetCardIsPack()) {
                                    if (clickCard.GetComponent<Card>().packLockCount==0) {
                                        clickCard.GetComponent<Card>().UnpackCard();
                                    }
                                    isClickCard = false;
                                    break;
                                }
                                if (clickCardIsHide==true) {
                                    
                                } else {
                                    Debug.Log("clickCardNumber "+clickCard.GetComponent<Card>().GetCardNumber());
                                    Debug.Log("currentCardNumber "+currentCard.GetComponent<Card>().GetCardNumber());
                                    Debug.Log("Mathf.Abs(clickCardNumber - currentCardNumber) "+Mathf.Abs(clickCard.GetComponent<Card>().GetCardNumber() - currentCard.GetComponent<Card>().GetCardNumber()));
                                    switch(currentCard.GetComponent<Card>().GetCardType()) {
                                        case 0:
                                        case 1:
                                        case 2:
                                        case 3:
                                            switch (clickCard.GetComponent<Card>().GetCardType()) {
                                                case 0:
                                                case 1:
                                                case 2:
                                                case 3:
                                                    if (   (Mathf.Abs(clickCard.GetComponent<Card>().GetCardNumber() - currentCard.GetComponent<Card>().GetCardNumber()) % 12 == 1)
                                                        || (Mathf.Abs(clickCard.GetComponent<Card>().GetCardNumber() - currentCard.GetComponent<Card>().GetCardNumber()) == 12))
                                                        isMove = true;
                                                    break;
                                                case 4:
                                                    if (currentCard.GetComponent<Card>().GetCardType()==clickCard.GetComponent<Card>().GetCardNumber())
                                                        isMove = true;
                                                    if (clickCard.GetComponent<Card>().GetCardNumber()==4)
                                                        isMove = true;
                                                    break;
                                            }
                                            break;
                                        case 4:
                                            switch (clickCard.GetComponent<Card>().GetCardType()) {
                                                case 0:
                                                case 1:
                                                case 2:
                                                case 3:
                                                    if (currentCard.GetComponent<Card>().GetCardNumber()==clickCard.GetComponent<Card>().GetCardType())
                                                        isMove = true;
                                                    if (currentCard.GetComponent<Card>().GetCardNumber()==4 || clickCard.GetComponent<Card>().GetCardNumber()==4)
                                                        isMove = true;
                                                    break;
                                                case 4:
                                                    if (currentCard.GetComponent<Card>().GetCardNumber()==clickCard.GetComponent<Card>().GetCardNumber())
                                                        isMove = true;
                                                    if (currentCard.GetComponent<Card>().GetCardNumber()==4 || clickCard.GetComponent<Card>().GetCardNumber()==4)
                                                        isMove = true;
                                                    break;
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                if (isMove == true) {
                                    Instantiate(prefabEffect[0],clickCard.transform.position,Quaternion.Euler(Vector3.zero));
                                    publicTweener = clickCard.transform.DOLocalMove(currentStandardPosition, 0.5f);
                                    publicTweener.OnComplete(ChangeCurrentCardToClickCard);
                                } else {
                                    float tempY = clickCard.transform.localPosition.y;      //连击导致位置上升
                                    publicTweener = clickCard.transform.DOLocalMove(new Vector3(clickCard.transform.localPosition.x,2.0f,clickCard.transform.localPosition.z), 0.5f);
                                    publicTweener = clickCard.transform.DOLocalMove(new Vector3(clickCard.transform.localPosition.x,tempY,clickCard.transform.localPosition.z), 0.5f);
                                    isClickCard = false;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (freeCardGroup.transform.childCount == 0)
                {
                    Debug.Log("Win");
                    currentCongratulateTime = 0.0f;
                    canvasMatch.GetComponent<CanvasMatch>().Congratulate();
                    player.GetComponent<Player>().BaseLevelWin();
                    matchState = MATCH_STATE_CONGRATULATE;
                }
                if (hideCardGroup.transform.childCount == 0)
                {
                    //Debug.Log("Fail");
                    //canvasFail.SetActive(true);
                    canvasMatch.GetComponent<CanvasMatch>().ShowAddCard(true);
                    //matchState = MATCH_STATE_RESULT;
                }
                break;
            case MATCH_STATE_CONGRATULATE:
                currentCongratulateTime += Time.deltaTime;
                if (currentCongratulateTime>congratulateTime) {
                    canvasMatch.GetComponent<CanvasMatch>().ShowWin(true);
                    matchState = MATCH_STATE_RESULT;
                }
                break;
            case MATCH_STATE_RESULT:
                break;
            case MATCH_STATE_QUIT:
                Quit();
                gameManager.GetComponent<GameManager>().MatchToOffice();
                matchState = MATCH_STATE_END;
                break;
            case MATCH_STATE_END:
                levelNumber = 0;
                matchState = MATCH_STATE_LOAD;
                break;
            default:
                break;
        }
    }



    private void ChangeCurrentCardToClickCard()
    {
        Destroy(currentCard);
        currentCard = clickCard;
        currentCard.GetOrAddComponent<Card>().SetCard(0,false);
        clickCard.transform.parent = currentCardGroup.transform;
        //currentCardType = clickCardType;
        //currentCardNumber = clickCardNumber;
        isClickCard = false;
        foreach (Transform card in freeCardGroup.transform) {
            card.gameObject.GetComponent<Card>().UpdatePackCard();
            card.gameObject.GetComponent<Card>().UpdateVarietyCard();
        }
        currentCard.GetOrAddComponent<Card>().CleanCard();
    }

    private void ChangeCurrentCardToHideCard()
    {
        Destroy(currentCard);
        currentCard = hideCard[0];
        currentCard.GetOrAddComponent<Card>().SetCard(0,false);
        hideCard[0].transform.parent = currentCardGroup.transform;
        //currentCardType = hideCard[0].GetOrAddComponent<Card>().GetCardType();
        //currentCardNumber = hideCard[0].GetOrAddComponent<Card>().GetCardNumber();
        hideCard[0] = null;
        for (int i=0;i<hideCard.Length-1;i++) {
            if (hideCard[i+1]!=null) {
                hideCard[i] = hideCard[i+1];
                hideCard[i+1] = null;
            } else {
                break;
            }
        }
        isClickCard = false;
        foreach (Transform card in freeCardGroup.transform) {
            card.gameObject.GetComponent<Card>().UpdateVarietyCard();
        }
        currentCard.GetOrAddComponent<Card>().CleanCard();
    }

    public void ToOffice()
    {
        matchState = MATCH_STATE_QUIT;
    }

    public void Restart()
    {
        matchState = MATCH_STATE_LOAD;
    }

    public void SetLevelName(string tempLevelName)
    {
        levelName = tempLevelName;
    }

    private void Quit()
    {
        
        Debug.Log(hideCardGroup.transform.childCount);
        for (int i = 0; i < currentCardGroup.transform.childCount; i++) Destroy(currentCardGroup.transform.GetChild(i).gameObject);
        for (int i = 0; i < freeCardGroup.transform.childCount; i++) Destroy(freeCardGroup.transform.GetChild(i).gameObject);
        for (int i = 0; i < hideCardGroup.transform.childCount; i++) Destroy(hideCardGroup.transform.GetChild(i).gameObject);
    }

    private int ToColorNumber(string color)
    {
        switch (color)
        {
            case "suitspades":
                {
                    return 2;
                }
            case "suithearts":
                {
                    return 0;
                }
            case "suitclubs":
                {
                    return 3;
                }
            case "suitdiamonds":
                {
                    return 1;
                }
        }
        return 4;
    }

    static string pokeNameContainter = "A,2,3,4,5,6,7,8,9,10,J,Q,K";
    static string[] pokeColorArr = { "suitspades", "suithearts", "suitclubs", "suitdiamonds" };
    static List<string> pokeColorContainter = new List<string>(pokeColorArr);
    class RandPokeScope
    {
        public List<string> nameList = new List<string>();
        public List<string> colorList = new List<string>();
    }
    
    private GameObject GetRandPoke(string v)
    {
        List<RandPokeScope> randPokeList = new List<RandPokeScope>();

        string[] rand_numbers =  v.Split(",");
        for(int i = 0; i < rand_numbers.Length; ++i)
        {
            RandPokeScope newScope = new RandPokeScope();
            string tt = rand_numbers[i].Trim();
            int idxbegin = tt.IndexOf("[");
            if (idxbegin != -1)
            {
                int idxend = tt.IndexOf("]", idxbegin);
                if (idxend == -1)
                {
                    continue;
                }
                string []colorScope = tt.Substring(idxbegin + 1, idxend - idxbegin - 1).Split("|");
                newScope.colorList.AddRange(colorScope);
                if (!newScope.colorList.TrueForAll(v => pokeColorContainter.IndexOf(v) != -1))
                {
                    continue;
                }
                tt = tt.Substring(0, idxbegin);
            }
            else
            {
                newScope.colorList.AddRange(pokeColorContainter);
            }

            int idx = tt.IndexOf("-");
            if (idx > 0)
            {
                if (idx == tt.Length)
                {
                    tt = tt.Substring(0, idx).Trim();
                    int i2 = pokeNameContainter.IndexOf(tt);
                    if (i2 != -1)
                    {
                        tt = pokeNameContainter.Substring(i2).Trim();
                        newScope.nameList.AddRange(tt.Split(","));
                    }
                } 
                else
                {
                    string[] tt2 = tt.Split("-");
                    int i21 = pokeNameContainter.IndexOf(tt2[0]);
                    int i22 = pokeNameContainter.IndexOf(tt2[1]);
                    if (i21 != -1 && i22 != -1)
                    {
                        tt = pokeNameContainter.Substring(i21, i22 - i21 + 1).Trim();
                        newScope.nameList.AddRange(tt.Split(","));
                    }
                }
            } 
            else if (idx == -1)
            {
                if (pokeNameContainter.IndexOf(tt) != -1)
                {
                    newScope.nameList.Add(tt);
                }
            }
            else
            {
                tt = tt.Substring(1).Trim();
                int i2 = pokeNameContainter.IndexOf(tt);
                if (i2  != -1)
                {
                    tt = pokeNameContainter.Substring(0, i2).Trim();
                    newScope.nameList.AddRange(tt.Split(","));
                }
            }
            randPokeList.Add(newScope);
        }

        if (randPokeList.Count < 1)
        {
            RandPokeScope newScope = new RandPokeScope();
            newScope.colorList.AddRange(pokeColorArr);
            newScope.nameList.AddRange(pokeNameContainter.Split(","));
            randPokeList.Add(newScope);
        }
        RandPokeScope pokeScope = randPokeList[Random.Range(0, randPokeList.Count - 1)];
        return GetPoke(pokeScope.colorList[Random.Range(0, pokeScope.colorList.Count - 1)], pokeScope.nameList[Random.Range(0, pokeScope.nameList.Count - 1)]);

    }

    private GameObject GetWanPoke()
    {
        GameObject temp = null;
        temp = Instantiate(prefabCard[52], this.transform);
        return temp;
    }

    private GameObject GetChromyPoke(string chromy)
    {
        GameObject temp = null;
        switch (chromy)
        {
            case "suitspades":
                {
                    temp = Instantiate(prefabCard[52], this.transform);
                    break;
                }
            case "suithearts":
                {
                    temp = Instantiate(prefabCard[53], this.transform);
                    break;
                }
            case "suitclubs":
                {
                    temp = Instantiate(prefabCard[54], this.transform);
                    break;
                }
            case "suitdiamonds":
                {
                    temp = Instantiate(prefabCard[55], this.transform);
                    break;
                }
            case "suitwan":
                {
                    temp = Instantiate(prefabCard[56], this.transform);
                    break;
                }
        }
        return temp;
    }

    private GameObject GetPoke(string color, string name)
    {
        GameObject temp = null;

        switch (color)
        {
            case "suitspades": //����
                {
                    switch (name)
                    {
                        case "A":
                            {
                                temp = Instantiate(prefabCard[0], this.transform);
                                break;
                            }
                        case "2":
                            {
                                temp = Instantiate(prefabCard[1], this.transform);

                                break;
                            }
                        case "3":
                            {
                                temp = Instantiate(prefabCard[2], this.transform);

                                break;
                            }
                        case "4":
                            {
                                temp = Instantiate(prefabCard[3], this.transform);

                                break;
                            }
                        case "5":
                            {
                                temp = Instantiate(prefabCard[4], this.transform);

                                break;
                            }
                        case "6":
                            {
                                temp = Instantiate(prefabCard[5], this.transform);
                                break;
                            }
                        case "7":
                            {
                                temp = Instantiate(prefabCard[6], this.transform);
                                break;
                            }
                        case "8":
                            {
                                temp = Instantiate(prefabCard[7], this.transform);
                                break;
                            }
                        case "9":
                            {
                                temp = Instantiate(prefabCard[8], this.transform);
                                break;
                            }
                        case "10":
                            {
                                temp = Instantiate(prefabCard[9], this.transform);
                                break;
                            }
                        case "J":
                            {
                                temp = Instantiate(prefabCard[10], this.transform);
                                break;
                            }
                        case "Q":
                            {
                                temp = Instantiate(prefabCard[11], this.transform);
                                break;
                            }
                        case "K":
                            {
                                temp = Instantiate(prefabCard[12], this.transform);
                                break;
                            }

                    }
                    break;
                }
            case "suithearts": //����
                {
                    switch (name)
                    {
                        case "A":
                            {
                                temp = Instantiate(prefabCard[13], this.transform);
                                break;
                            }
                        case "2":
                            {
                                temp = Instantiate(prefabCard[14], this.transform);

                                break;
                            }
                        case "3":
                            {
                                temp = Instantiate(prefabCard[15], this.transform);

                                break;
                            }
                        case "4":
                            {
                                temp = Instantiate(prefabCard[16], this.transform);

                                break;
                            }
                        case "5":
                            {
                                temp = Instantiate(prefabCard[17], this.transform);

                                break;
                            }
                        case "6":
                            {
                                temp = Instantiate(prefabCard[18], this.transform);
                                break;
                            }
                        case "7":
                            {
                                temp = Instantiate(prefabCard[19], this.transform);
                                break;
                            }
                        case "8":
                            {
                                temp = Instantiate(prefabCard[20], this.transform);
                                break;
                            }
                        case "9":
                            {
                                temp = Instantiate(prefabCard[21], this.transform);
                                break;
                            }
                        case "10":
                            {
                                temp = Instantiate(prefabCard[22], this.transform);
                                break;
                            }
                        case "J":
                            {
                                temp = Instantiate(prefabCard[23], this.transform);
                                break;
                            }
                        case "Q":
                            {
                                temp = Instantiate(prefabCard[24], this.transform);
                                break;
                            }
                        case "K":
                            {
                                temp = Instantiate(prefabCard[25], this.transform);
                                break;
                            }
                    }
                    break;
                }
            case "suitclubs": //��
                {
                    switch (name)
                    {
                        case "A":
                            {
                                temp = Instantiate(prefabCard[26], this.transform);
                                break;
                            }
                        case "2":
                            {
                                temp = Instantiate(prefabCard[27], this.transform);

                                break;
                            }
                        case "3":
                            {
                                temp = Instantiate(prefabCard[28], this.transform);

                                break;
                            }
                        case "4":
                            {
                                temp = Instantiate(prefabCard[29], this.transform);

                                break;
                            }
                        case "5":
                            {
                                temp = Instantiate(prefabCard[30], this.transform);

                                break;
                            }
                        case "6":
                            {
                                temp = Instantiate(prefabCard[31], this.transform);
                                break;
                            }
                        case "7":
                            {
                                temp = Instantiate(prefabCard[32], this.transform);
                                break;
                            }
                        case "8":
                            {
                                temp = Instantiate(prefabCard[33], this.transform);
                                break;
                            }
                        case "9":
                            {
                                temp = Instantiate(prefabCard[34], this.transform);
                                break;
                            }
                        case "10":
                            {
                                temp = Instantiate(prefabCard[35], this.transform);
                                break;
                            }
                        case "J":
                            {
                                temp = Instantiate(prefabCard[36], this.transform);
                                break;
                            }
                        case "Q":
                            {
                                temp = Instantiate(prefabCard[37], this.transform);
                                break;
                            }
                        case "K":
                            {
                                temp = Instantiate(prefabCard[38], this.transform);
                                break;
                            }
                    }
                    Log.Debug("suitdiamonds");

                    break;
                }
            case "suitdiamonds": //��
                {
                    switch (name)
                    {
                        case "A":
                            {
                                temp = Instantiate(prefabCard[39], this.transform);
                                break;
                            }
                        case "2":
                            {
                                temp = Instantiate(prefabCard[40], this.transform);

                                break;
                            }
                        case "3":
                            {
                                temp = Instantiate(prefabCard[41], this.transform);

                                break;
                            }
                        case "4":
                            {
                                temp = Instantiate(prefabCard[42], this.transform);

                                break;
                            }
                        case "5":
                            {
                                temp = Instantiate(prefabCard[43], this.transform);

                                break;
                            }
                        case "6":
                            {
                                temp = Instantiate(prefabCard[44], this.transform);
                                break;
                            }
                        case "7":
                            {
                                temp = Instantiate(prefabCard[45], this.transform);
                                break;
                            }
                        case "8":
                            {
                                temp = Instantiate(prefabCard[46], this.transform);
                                break;
                            }
                        case "9":
                            {
                                temp = Instantiate(prefabCard[47], this.transform);
                                break;
                            }
                        case "10":
                            {
                                temp = Instantiate(prefabCard[48], this.transform);
                                break;
                            }
                        case "J":
                            {
                                temp = Instantiate(prefabCard[49], this.transform);
                                break;
                            }
                        case "Q":
                            {
                                temp = Instantiate(prefabCard[50], this.transform);
                                break;
                            }
                        case "K":
                            {
                                temp = Instantiate(prefabCard[51], this.transform);
                                break;
                            }
                    }
                    break;
                }
        }
        return temp;
    }

    private GameObject GetPoke(Assets.Script.Data.Card card)
    {
        GameObject temp = null;

        if (card.components.face.name == "R")           //随机
        {
            // free.rand; 随机参数
            temp = GetRandPoke(card.components.rand);

        }
        else if (card.components.face.name == "∀")       //万能
        {
            temp = GetWanPoke();
        }
        else if (card.components.face.name == "C")      //单色
        {
            temp = GetChromyPoke(card.components.chromy);
        }
        else
        {
            temp = GetPoke(card.components.face.color, card.components.face.name);
        }
        return temp;
    }

    private void InstanceLevel(Occasion e2)
    {
        GameObject temp = null;

        foreach (Assets.Script.Data.Card free in e2.level.composition.cards)
        {
            temp = GetPoke(free);
            Card card = temp.GetComponent<Card>();
            card.SetCard(2, free.IsHide());
            card.SetCardId(free.id);

            if (free.components.IsMulch())      //压牌id
            {
                card.SetMulchIds(free.components.GetMulchIds());
            }
            if (free.components.IsPack())
            {
                card.SetPackIds(free.components.GetPackIds(),free.components.pack.cnt);
            }
            if (free.components.IsVariety())
            {
                if (free.components.variety == "plus") card.SetVariety(true);
                else if (free.components.variety == "minus") card.SetVariety(false);
            }
            if (free.IsShow()) card.SetShow(true);
            else card.SetShow(false);

            temp.transform.parent = freeCardGroup.transform;
            Vector3 pos = new Vector3(free.components.position.x/positionXZScale, free.components.position.z/100.0f, -free.components.position.y/positionXZScale);
            temp.transform.localPosition = pos;
        }

        if (e2.level.composition.underpans.Count > 0)
        {
            int currentIdx = 0;
            Assets.Script.Data.Card current = e2.level.composition.underpans[currentIdx];
            temp = GetPoke(current);
            temp.GetComponent<Card>().SetCard(0, false);
            temp.GetComponent<Card>().SetCardId(current.id);

            temp.transform.localPosition = currentStandardPosition;
            temp.transform.parent = currentCardGroup.transform;
            currentCard = temp;
            //currentCardType = 0;
            //currentCardNumber = currentCard.GetComponent<Card>().GetCardNumber();


            Vector3 hideStandardPosition = currentStandardPosition;
            hideStandardPosition.x -= pokeWidth;
            ////hideStandardPosition.x += pokeWidth * currentIdx;

            for (int i = 1; i < e2.level.composition.underpans.Count; ++i)
            {
                Assets.Script.Data.Card hide = e2.level.composition.underpans[i];
                temp = GetPoke(hide);

                Card card = temp.GetComponent<Card>();
                card.SetCard(1, true);
                card.SetCardId(hide.id);
                temp.transform.parent = hideCardGroup.transform;
                temp.transform.localPosition = new Vector3(hideStandardPosition.x - pokeSpan * i, 0.0f, hideStandardPosition.z);
                hideCard[i-1] = temp;
            }
        }
        foreach (Transform card in freeCardGroup.transform) {
            Debug.Log(card);
            card.gameObject.GetComponent<Card>().SetMulchCard(freeCardGroup);
            card.gameObject.GetComponent<Card>().SetPackCard(freeCardGroup);
        }
    }

    public GameObject[] GetMulchGameObjects(string[] ids)
    {
        if (ids.Length == 0)
        {
            return null;
        }
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (string id in ids)
        {
            for (int i = 0; i < freeCardGroup.transform.childCount; i++)
            {
                GameObject go = freeCardGroup.transform.GetChild(i).gameObject;
                Card card = go.GetComponent<Card>();
                if (card.cardId == id)
                {
                    gameObjects.Add(go);
                }
            }
        }
        return gameObjects.ToArray();
    }

    public void AddHideCard() {
        GameObject temp = null;
        Vector3 hideStandardPosition = currentStandardPosition;
        hideStandardPosition.x -= pokeWidth;
        for (int i=0;i<5;i++) {
            temp = Instantiate(prefabCard[Random.Range(0,52)], this.transform);
            temp.GetComponent<Card>().SetCard(1, true);
            temp.transform.parent = hideCardGroup.transform;
            temp.transform.localPosition = new Vector3(hideStandardPosition.x - pokeSpan * i, 0.0f, hideStandardPosition.z);
            hideCard[i] = temp;
        }
    }

    public void OnGetLevelDataResponse(GetLevelDataResponse e2)
    {

    }

    public void ResetLevel()
    {
        for (int i = 0; i < freeCardGroup.transform.childCount; i++)
        {
            Destroy(freeCardGroup.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < hideCardGroup.transform.childCount; i++)
        {
            Destroy(hideCardGroup.transform.GetChild(i).gameObject);
        }
        levelId = "";
    }

    public void LoadLevel(Occasion e2)
    {
        ResetLevel();
        InstanceLevel(e2);
        levelId = e2.id;
        matchState = MATCH_STATE_ENTRY_LEVEL;
    }


    public void LoadLevel()
    {
        LoadLevel(new LoadAssetCallbacks(
               (assetName, asset, duration, userData) =>
               {
                   GetLevelDataResponse e2 = (GetLevelDataResponse)asset;
                   LoadLevel(e2.data);
               },
               (assetName, status, errorMessage, userData) =>
               {
                   Log.Error("加载管卡失败");
               }));

        //Framework.Resource.LoadAsset("Assets/Levels/test.json", new LoadAssetCallbacks(
        //       (assetName, asset, duration, userData) =>
        //       {
        //           TextAsset json = (TextAsset)asset;
        //           Occasion e2 = GameFramework.Utility.Json.ToObject<Occasion>(json.text);
        //           InstanceLevel(e2);
        //           matchState = MATCH_STATE_ENTRY_LEVEL;
        //       },
        //       (assetName, status, errorMessage, userData) =>
        //       {
        //           Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", "sldjf", assetName, errorMessage);
        //       }));
    }

    public void LoadLevel(LoadAssetCallbacks loadAssetCallbacks)
    {
        gameManager.GetComponent<GameManager>().LoadLevel(levelName, loadAssetCallbacks);
    }

}
