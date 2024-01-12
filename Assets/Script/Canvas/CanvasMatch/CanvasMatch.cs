using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CanvasMatch : MonoBehaviour
{
    public GameObject match;
    public GameObject buttonAddCard;
    public GameObject effectCongratulate;
    public GameObject canvasFail;
    public GameObject canvasWin;
    public GameObject canvasTip;
    public GameObject canvasItem;
    public GameObject canvasStory;
    public GameObject buttonFail;

    public GameObject player;
    public GameObject goldText;
    public GameObject diamondText;


    public GameObject content;
    public GameObject[] item;
    //public GameObject flyItemPrefab;
    public GameObject flyItem;
    private int addItemId;
    private int addItemCount;
    private GameObject clickButtonPoint;
    public GameObject gameManager;
    private int tempBuildingNumber;
    public GameObject selectItem;
    public GameObject selectBuilding;
    public GameObject effectSelectItem;
    public GameObject effectSelectBuilding;
    public GameObject textCount;
    public GameObject textUsed;
    private const int maximumItemCount = 100;

    private Tweener publicTweener;
    private Sequence publicSequence;

    public enum CanvasMatchState {
        Story,
        Action,
    }
    public CanvasMatchState canvasMatchState;
    // Start is called before the first frame update
    void Start()
    {
        SetItem();
        canvasMatchState = CanvasMatchState.Action;
    }

    // Update is called once per frame
    void Update()
    {
        if (player!=null) {
            goldText.GetComponent<Text>().text = player.GetComponent<Player>().gold.ToString();
            diamondText.GetComponent<Text>().text = player.GetComponent<Player>().diamond.ToString();
        }
        switch (canvasMatchState) {
            case CanvasMatchState.Story:
                StoryUpdate();
                break;
            case CanvasMatchState.Action:
                break;
            default:
                break;
        }
    }

    public void ShowAddCard (bool tempIsShow) {
        buttonAddCard.SetActive(tempIsShow);
        buttonFail.SetActive(tempIsShow);
    }

    public void AddHideCard() {
        match.GetComponent<MatchManager>().AddHideCard();
        buttonAddCard.SetActive(false);
        buttonFail.SetActive(false);
    }

    public void Congratulate() {
        effectCongratulate.GetComponent<ParticleSystem>().Play();
    }

    public void ShowFail(bool tempIsShow) {
        canvasFail.SetActive(tempIsShow);
    }

    public void ExitMatch() {
        buttonAddCard.SetActive(false);
        buttonFail.SetActive(false);
        canvasFail.SetActive(false);
        canvasWin.SetActive(false);
        this.gameObject.SetActive(false);
        match.GetComponent<MatchManager>().ToOffice();
    }

    public void RestartMatch() {
        match.GetComponent<MatchManager>().Restart();
    }

    public void ShowWin(bool tempIsShow) {
        canvasWin.SetActive(tempIsShow);
    }

    public void Reset() {
        buttonAddCard.SetActive(false);
        buttonFail.SetActive(false);
        canvasFail.SetActive(false);
        canvasWin.SetActive(false);
    }

    public void SetItem() {
        gameManager = GameObject.Find ("GameManager");
        //item = new GameObject[gameManager.GetComponent<GameManager>().player.GetComponent<Player>().itemBag.Length + 1];
        item = new GameObject[maximumItemCount];
        int tempItemNumber = 0;
        for (int i=0;i<gameManager.GetComponent<GameManager>().player.GetComponent<Player>().itemBag.Length;i++) {
            if (gameManager.GetComponent<GameManager>().player.GetComponent<Player>().itemBag[i].count!=0) {
                item[tempItemNumber] = GameObject.Instantiate(gameManager.GetComponent<GameManager>().itemManager.GetComponent<ItemManager>().itemProject[gameManager.GetComponent<GameManager>().player.GetComponent<Player>().itemBag[i].id]);
                item[tempItemNumber].transform.SetParent(content.transform);
                item[tempItemNumber].transform.localPosition = Vector3.zero;
                item[tempItemNumber].transform.localRotation = Quaternion.Euler(0.0f,0.0f,0.0f);
                item[tempItemNumber].transform.localScale = Vector3.one;
                //tempBuildingNumber = gameManager.GetComponent<GameManager>().buildingManager.GetComponent<BuildingManager>().GetBuildingPrefabNumberById(gameManager.GetComponent<GameManager>().player.GetComponent<Player>().buildingBag[i].id);
                //if (tempBuildingNumber==-1) return;
                item[tempItemNumber].GetComponent<Item>().Initial(gameManager.GetComponent<GameManager>().player.GetComponent<Player>().itemBag[i].count);
                item[tempItemNumber].transform.GetChild(0).GetComponent<Text>().text = item[i].GetComponent<Item>().count.ToString();
                tempItemNumber += 1;
            }
        }
    }

    public void ItemClick(GameObject tempItem) {
        if (tempItem.GetComponent<Item>().isSelect == true) {
            selectItem = null;
            tempItem.GetComponent<Item>().isSelect = false;
            effectSelectItem.SetActive(false);
            //land.GetComponent<Land>().ShowFieldEffect(false);
        } else {
            selectItem = tempItem;
            effectSelectItem.SetActive(true);
            //Debug.Log(tempItem.transform.position);
            effectSelectItem.transform.SetParent(tempItem.transform);
            effectSelectItem.transform.localPosition = tempItem.transform.GetChild(0).transform.position;
            effectSelectItem.transform.localRotation = Quaternion.Euler(0.0f,0.0f,0.0f);
            effectSelectItem.transform.localScale = Vector3.one;
            //effectSelect.GetComponent<RectTransform>().anchoredPosition = tempItem.GetComponent<RectTransform>().anchoredPosition;
            for (int i=0;i<item.Length;i++) {
                if (item[i]!=null) item[i].GetComponent<Item>().isSelect = false;
            }
            selectItem.GetComponent<Item>().isSelect = true;
            //land.GetComponent<Land>().ShowFieldEffect(true);
            //buttonBuyGold.transform.GetChild(0).GetComponent<Text>().text = selectItem.GetComponent<Item>().priceGold.ToString();
            //buttonBuyDiamond.transform.GetChild(0).GetComponent<Text>().text = selectItem.GetComponent<Item>().priceDiamond.ToString();
            //Debug.Log(selectItem.GetComponent<Item>().model.GetComponent<Building>().id);
            //textCount.GetComponent<Text>().text = gameManager.GetComponent<GameManager>().player.GetComponent<Player>().GetBuildingCount(selectItem.GetComponent<Item>().model.GetComponent<Building>().id).ToString();
            //textUsed.GetComponent<Text>().text = gameManager.GetComponent<GameManager>().player.GetComponent<Player>().GetBuildingUsed(selectItem.GetComponent<Item>().model.GetComponent<Building>().id).ToString();
        }
        //ReleaseBuilding();
    }

    public void ReduceItem(int tempId, int tempCount) {
        if (gameManager.GetComponent<GameManager>().player.GetComponent<Player>().ReduceItem(tempId,tempCount)) {
            effectSelectItem.SetActive(false);
            effectSelectItem.transform.SetParent(this.transform);
            DestroyImmediate(selectItem);
            selectItem = null;
        }
    }

    public void AddItem(int tempId, int tempCount) {
        gameManager.GetComponent<GameManager>().player.GetComponent<Player>().AddItem(addItemId,addItemCount);
        for (int i=0;i<item.Length;i++) {
            if (item[i]==null) {
                item[i] = GameObject.Instantiate(gameManager.GetComponent<GameManager>().itemManager.GetComponent<ItemManager>().GetItemPrefabById(tempId));
                //item[i] = GameObject.Instantiate(gameManager.GetComponent<GameManager>().itemManager.GetComponent<ItemManager>().itemPrefab[gameManager.GetComponent<GameManager>().itemManager.GetComponent<ItemManager>().GetItemPrefabNumberById(tempId)]);
                item[i].transform.SetParent(content.transform);
                item[i].transform.SetAsFirstSibling();
                item[i].transform.localPosition = Vector3.zero;
                item[i].transform.localRotation = Quaternion.Euler(0.0f,0.0f,0.0f);
                item[i].transform.localScale = Vector3.one;
                item[i].GetComponent<Item>().Initial(tempCount);
                break;
            }
        }
    }

    public void GetItemComplete() {
        flyItem.SetActive(false);
        this.AddItem(addItemId,addItemCount);
    }

    public void GetItem(GameObject tempButtonPoint) {
        addItemId = tempButtonPoint.GetComponent<ButtonPointItem>().itemId;
        addItemCount = tempButtonPoint.GetComponent<ButtonPointItem>().itemCount;
        flyItem.transform.position = tempButtonPoint.transform.position;
        flyItem.GetComponent<Image>().sprite = tempButtonPoint.GetComponent<ButtonPointItem>().GetShowSpriteByItemNumber();
        flyItem.GetComponent<Image>().SetNativeSize();
        float tempScale;
        if (flyItem.GetComponent<Image>().sprite.texture.width>flyItem.GetComponent<Image>().sprite.texture.height) tempScale = 256/flyItem.GetComponent<Image>().sprite.texture.height;
        else tempScale = 256.0f/flyItem.GetComponent<Image>().sprite.texture.width;
        flyItem.transform.localScale = new Vector3(tempScale,tempScale,1.0f);
        flyItem.SetActive(true);
        tempButtonPoint.SetActive(false);   /*-------------                  yes or no?                ------------------------*/
        publicSequence = DOTween.Sequence();
        publicSequence.Append(flyItem.transform.DOMove(flyItem.transform.position, 0.5f, false));
        publicSequence.Join(flyItem.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 0.5f));
        Debug.Log(content.transform.parent.parent.GetComponent<RectTransform>().anchoredPosition);
        Vector3 tempPosition = new Vector3(
            content.transform.parent.parent.GetComponent<RectTransform>().anchoredPosition.x,
            content.transform.parent.parent.GetComponent<RectTransform>().anchoredPosition.y+256.0f+256.0f/2,
            0.0f
        );
        publicSequence.Append(flyItem.transform.DOLocalMove(tempPosition, 0.5f, false));
        publicSequence.Join(flyItem.transform.DOScale(new Vector3(tempScale,tempScale,1.0f), 0.5f));
        publicSequence.OnComplete(GetItemComplete);
    }

    public void UseItemComplete() {
        flyItem.SetActive(false);
        ReduceItem(addItemId,addItemCount);
        switch (clickButtonPoint.GetComponent<ButtonPoint>().buttonPointType) {
            case ButtonPoint.ButtonPointType.Door:
                clickButtonPoint.GetComponent<ButtonPointDoor>().Open();
                break;
            case ButtonPoint.ButtonPointType.Item:
                GetItem(clickButtonPoint);
                break;
            default:
                break;
        }
        
    }

    public void UseItem(GameObject tempButtonPoint) {
        if (selectItem!=null) {
            addItemId = tempButtonPoint.GetComponent<ButtonPointItem>().itemId;
            addItemCount = tempButtonPoint.GetComponent<ButtonPointItem>().itemCount;
            clickButtonPoint = tempButtonPoint;
            flyItem.transform.position = selectItem.transform.position;
            flyItem.GetComponent<Image>().sprite = selectItem.GetComponent<Item>().showSprit;
            flyItem.GetComponent<Image>().SetNativeSize();
            /*
            float tempScale;
            if (flyItem.GetComponent<Image>().sprite.texture.width>flyItem.GetComponent<Image>().sprite.texture.height) tempScale = 256/flyItem.GetComponent<Image>().sprite.texture.height;
            else tempScale = 256.0f/flyItem.GetComponent<Image>().sprite.texture.width;
            flyItem.transform.localScale = new Vector3(tempScale,tempScale,1.0f);
            */
            flyItem.SetActive(true);
            publicSequence = DOTween.Sequence();
            publicSequence.Append(flyItem.transform.DOMove(tempButtonPoint.transform.position, 0.5f, false));
            /*
            publicSequence.Join(flyItem.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 0.5f));
            Vector3 tempPosition = new Vector3(
                content.transform.parent.parent.GetComponent<RectTransform>().anchoredPosition.x+content.transform.parent.parent.GetComponent<RectTransform>().sizeDelta.x/2,
                content.transform.parent.parent.GetComponent<RectTransform>().anchoredPosition.y+content.transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y/2,
                0.0f
            );
            */
            publicSequence.Append(flyItem.transform.DORotate(new Vector3(0.0f,0.0f,30.0f), 0.1f));
            publicSequence.Append(flyItem.transform.DORotate(new Vector3(0.0f,0.0f,-30.0f), 0.1f));
            publicSequence.Append(flyItem.transform.DORotate(new Vector3(0.0f,0.0f,30.0f), 0.1f));
            publicSequence.Append(flyItem.transform.DORotate(new Vector3(0.0f,0.0f,-30.0f), 0.1f));
            publicSequence.Append(flyItem.transform.DORotate(new Vector3(0.0f,0.0f,0.0f), 0.1f));
            //publicSequence.Join(flyItem.transform.DOScale(new Vector3(tempScale,tempScale,1.0f), 0.5f));
            publicSequence.OnComplete(UseItemComplete);
        }
    }

    public void ToStoryState(GameObject tempStoryPoint) {
        if (canvasMatchState==CanvasMatchState.Action) {
            canvasItem.SetActive(false);
            canvasStory.GetComponent<CanvasStory>().ShowStory(tempStoryPoint,true);
            canvasMatchState = CanvasMatchState.Story;
        }
    }
    public void ToActionState(GameObject tempStoryPoint) {
        if (canvasMatchState==CanvasMatchState.Story) {
            canvasItem.SetActive(true);
            canvasStory.GetComponent<CanvasStory>().ShowStory(tempStoryPoint,false);
            player.GetComponent<Player>().ChangeStoryValue(tempStoryPoint.GetComponent<StoryPoint>().changeStoryValue);
            canvasMatchState = CanvasMatchState.Action;
        }
    }

    public void StoryUpdate() {
        if (Input.GetMouseButtonDown (0)){//} && !EventSystem.current.IsPointerOverGameObject()) {
            canvasStory.GetComponent<CanvasStory>().Touch();
        }
    }
/*
    private void UpdateDrag() {
        if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) {
			recordMousePosition = Input.mousePosition;
            //Debug.Log(cameraAxis.transform.eulerAngles);
            recordCameraAxisEulerAngles = cameraAxis.transform.eulerAngles;
			//canvasManager.GetComponent<CanvasManager> ().imageJoystick.GetComponent<RectTransform> ().transform.localPosition = new Vector3 (
			//	canvasManager.GetComponent<RectTransform> ().rect.width * (canvasManager.GetComponent<Canvas> ().worldCamera.ScreenToViewportPoint(Input.mousePosition).x - 0.5f),
			//	canvasManager.GetComponent<RectTransform> ().rect.height * (canvasManager.GetComponent<Canvas> ().worldCamera.ScreenToViewportPoint(Input.mousePosition).y - 0.5f),
			//	0);
			//canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (true);
			isDown = true;
            isDrag = false;
		}
		if (isDown) {
			currentMousePosition = Input.mousePosition;
			if (Vector3.Distance (currentMousePosition, recordMousePosition) > minimumDistance) {
                isDrag = true;
				//Debug.Log((currentMousePosition-recordMousePosition).x);
				currentCameraAxisEulerAngles = new Vector3(
					recordCameraAxisEulerAngles.x+(currentMousePosition-recordMousePosition).y*dragSpeed,recordCameraAxisEulerAngles.y+(currentMousePosition-recordMousePosition).x*dragSpeed,0
				);
				if (currentCameraAxisEulerAngles.x>maximumAxisEulerAnglesX) currentCameraAxisEulerAngles = new Vector3(maximumAxisEulerAnglesX,currentCameraAxisEulerAngles.y,currentCameraAxisEulerAngles.z);
				if (currentCameraAxisEulerAngles.x<minimumAxisEulerAnglesX) currentCameraAxisEulerAngles = new Vector3(minimumAxisEulerAnglesX,currentCameraAxisEulerAngles.y,currentCameraAxisEulerAngles.z);
				//this.transform.Translate (new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * speed * Time.deltaTime);
				//this.GetComponent<CharacterController>().Move(new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * Time.deltaTime * speed);
				//moveTarget.transform.position = new Vector3 (this.transform.localPosition.x + (currentMousePosition-recordMousePosition).x,0,this.transform.localPosition.z + (currentMousePosition-recordMousePosition).y);
                cameraAxis.transform.eulerAngles = currentCameraAxisEulerAngles;
            }
		}
		if (Input.GetMouseButtonUp (0) && !EventSystem.current.IsPointerOverGameObject()) {
            if ((isDown==true) && (isDrag==false)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo)) {
                    if (hitInfo.transform.tag=="Field") {
                        if ((selectItem!=null) && (hitInfo.transform.parent.GetComponent<Field>().building==null)) {
                            selectBuilding = GameObject.Instantiate(selectItem.GetComponent<Item>().model);
                            hitInfo.transform.parent.GetComponent<Field>().building = selectBuilding;
                            //Debug.Log(selectItem.GetComponent<Item>().model.transform.name);
                            selectBuilding.transform.SetParent(hitInfo.transform.parent);
                            selectBuilding.transform.localPosition = Vector3.zero;
                            selectBuilding.transform.localRotation = Quaternion.Euler(0.0f,0.0f,0.0f);
                            //selectBuilding.transform.localRotation = Quaternion.Euler(0.0f,cameraAxis.transform.eulerAngles.y,0.0f);
                            selectBuilding.transform.localScale = Vector3.one;
                        } else {
                            if (hitInfo.transform.parent.GetComponent<Field>().building!=null) {
                                selectBuilding = hitInfo.transform.parent.GetComponent<Field>().building;
                                SelectBuilding(selectBuilding);
                                land.GetComponent<Land>().ShowFieldEffect(false);
                                //Debug.Log("already have one.");
                            }
                        }
                    } else {
                        if (selectBuilding!=null) ReleaseBuilding();
                    }
                }
            }
            isDown = false;
			isDrag = false;
			//canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (false);
		}
    }
*/
}
