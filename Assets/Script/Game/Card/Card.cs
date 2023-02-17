using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public Texture[] cardFrontTexture;
    public Texture[] cardBackTexture;
    public Texture[] cardCountTexture;
    public Texture[] cardVarietyTexture;
    public int cardType;
    public int cardNumber;
    public int cardGroup;
    public bool cardIsHide;
    public string cardId;
    public string[] mulchCardId;    //覆盖
    public GameObject[] mulchCard;
    public string[] packCardId;     //生成
    public GameObject[] packCard;
    public bool isPack;
    public int packLockCount;
    public Vector3 recordLocalPosition;

    private Tweener publicTweener;

    public bool isVariety;
    public bool isVarietyUp;

    void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetComponent<Renderer> ().materials[0].mainTexture = cardBackTexture[0];
        //Debug.Log(cardType+" "+cardNumber+" "+this.name);
        this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[cardType*13+cardNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (cardIsHide==true && this.transform.parent.name == "FreeCardGroup") {
            cardIsHide = false;
            for (int i=0;i< mulchCard.Length;i++) {
                if (mulchCard[i] != null && mulchCard[i].transform.parent.name == "FreeCardGroup") {
                    cardIsHide = true;
                    break;
                }
            }
            if (cardIsHide==false) {
                this.transform.DOLocalRotate(Vector3.zero, 0.2f);
                publicTweener = this.transform.DOLocalMove(new Vector3(this.transform.localPosition.x,4.0f,this.transform.localPosition.z), 0.5f);
                publicTweener = this.transform.DOLocalMove(new Vector3(this.transform.localPosition.x,0.0f,this.transform.localPosition.z), 0.5f);
                //this.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
            }
        }
    }

    public void SetMulchIds(string[] ids)
    {
        mulchCardId = ids;
    }


    public void SetPackIds(string[] ids, int tempPackLockCount)
    {
        packCardId = ids;
        isPack = true;
        packLockCount = tempPackLockCount;
        this.transform.GetChild(3).gameObject.SetActive(true);
        this.transform.GetChild(3).GetComponent<Renderer> ().materials[0].mainTexture = cardCountTexture[packLockCount];
    }

    public void SetVariety(bool tempIsVarietyUp)
    {
        isVariety = true;
        isVarietyUp = tempIsVarietyUp;
        this.transform.GetChild(2).gameObject.SetActive(true);
        if (isVarietyUp) this.transform.GetChild(2).GetComponent<Renderer> ().materials[0].mainTexture = cardVarietyTexture[0];
        else this.transform.GetChild(2).GetComponent<Renderer> ().materials[0].mainTexture = cardVarietyTexture[1];
    }

    public void SetShow(bool tempIsShow) {
        this.gameObject.SetActive(tempIsShow);
    }

    public void SetMulchCard(GameObject tempFree)
    {
        if (cardIsHide==true) {
            mulchCard = new GameObject[mulchCardId.Length];
            for (int i=0;i< mulchCard.Length;i++) {
                foreach (Transform child in tempFree.transform){
                    if (child.GetComponent<Card>().cardId == mulchCardId[i]) {
                        mulchCard[i] = child.gameObject;
                        break;
                    }
                }
            }
        }
    }

    public void SetPackCard(GameObject tempFree)
    {
        packCard = new GameObject[packCardId.Length];
        for (int i=0;i< packCard.Length;i++) {
            foreach (Transform child in tempFree.transform){
                if (child.GetComponent<Card>().cardId == packCardId[i]) {
                    packCard[i] = child.gameObject;
                    packCard[i].GetComponent<Card>().recordLocalPosition = packCard[i].transform.localPosition;
                    break;
                }
            }
        }
    }

    public void UpdatePackCard() {
        if (packLockCount > 0) {
            this.transform.GetChild(3).gameObject.SetActive(true);
            packLockCount -= 1;
            if (packLockCount==0) {
                this.transform.GetChild(3).GetComponent<Renderer> ().materials[0].mainTexture = null;
                this.transform.GetChild(3).gameObject.SetActive(false);
            } else this.transform.GetChild(3).GetComponent<Renderer> ().materials[0].mainTexture = cardCountTexture[packLockCount];
        }
    }
    public void UpdateVarietyCard() {
        if (isVariety) {
            this.transform.GetChild(2).gameObject.SetActive(true);
            if (isVarietyUp) {
                cardNumber += 1;
                if (cardNumber==13) cardNumber = 0;
            } else {
                cardNumber -= 1;
                if (cardNumber==-1) cardNumber = 12;
            }
            this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[cardType*13+cardNumber];
        }
    }

    public void UnpackCard() {
        for (int i=0;i< packCard.Length;i++) {
            packCard[i].transform.localPosition = this.transform.localPosition;
            packCard[i].SetActive(true);
            packCard[i].transform.DOLocalMove(packCard[i].GetComponent<Card> ().recordLocalPosition, 0.5f);
        }
        Destroy(this.gameObject);
    }
    public void CleanCard() {
        this.isPack = false;
        this.isVariety = false;
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void SetCard(int tempType, int tempNumber) {
        //type值0黑1红2花3片
        //type值4特殊牌，number值0黑全1红全2花全3片全4全全，number值5正变6负变。
        cardType = tempType;
        cardNumber = tempNumber;
        this.transform.GetChild(0).GetComponent<Renderer> ().materials[0].mainTexture = cardBackTexture[0];
        if (cardType <= 3) {
            int idx = cardType * 13 + cardNumber;
            if (idx >= cardFrontTexture.Length)
            {
                idx = 0;
            }
            this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[idx];
        } else if (cardType==4) {
            if (cardNumber==0) this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[52];
            if (cardNumber==1) this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[53];
            if (cardNumber==2) this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[54];
            if (cardNumber==3) this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[55];
            if (cardNumber==4) this.transform.GetChild(1).GetComponent<Renderer> ().materials[0].mainTexture = cardFrontTexture[56];
        }
    }

    public void SetCard(int tempGroup, bool tempIsHide) {
        cardGroup = tempGroup;
        cardIsHide = tempIsHide;
        if (cardIsHide) this.transform.rotation = Quaternion.Euler(0.0f,0.0f,180.0f);
        else this.transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
    }

    public void SetCardId(string id)
    {
        cardId = id;
    }

    public int GetCardType() {
        return cardType;
    }

    public int GetCardNumber() {
        return cardNumber;
    }

    public int GetCardGroup() {
        return cardGroup;
    }

    public bool GetCardIsHide() {
        return cardIsHide;
    }

    public bool GetCardIsPack() {
        return isPack;
    }
}
