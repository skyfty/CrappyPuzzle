using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
    [System.Serializable]
    public class Item {
        public int type;
        public bool isOpen;
        public int count;
        public int price;
    }*/

    public string id;
    public int gold;
    public int diamond;
    public int star;
    public int levelBaseNumber;         //next level to play
    [System.Serializable]
    public class ItemBag {
        public int id;
        public int count;
    }
    public ItemBag[] itemBag;

    public GameObject gameManager;
    public UserData userData;

    public int storyValue;

    // Start is called before the first frame update
    void Start()
    {
        userData = gameManager.GetComponent<User>().PlayerData;
        id = userData.id;
        gold = userData.gold;
        diamond = userData.diamond;
        star = userData.star;
        levelBaseNumber = userData.levelBaseNumber;
        //Debug.Log("userData.id "+userData.id);
        //Debug.Log("userData.levelBaseNumber "+userData.levelBaseNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BaseLevelWin() {
        levelBaseNumber = levelBaseNumber + 1;
        userData.levelBaseNumber = levelBaseNumber;
        gameManager.GetComponent<User>().Save();
    }

    public void AddItem(int tempId, int tempCount) {
        for (int i=0;i<itemBag.Length;i++) {
            if (itemBag[i].count==0) {
                itemBag[i].id = tempId;
                itemBag[i].count = tempCount;
                break;
            }
        }
    }

    public bool ReduceItem(int tempId, int tempCount) {
        for (int i=0;i<itemBag.Length;i++) {
            if (itemBag[i].id == tempId) {
                itemBag[i].count -= tempCount;
                if (itemBag[i].count==0) {
                    itemBag[i].id = -1;
                    return true;
                } else return false;
            }
        }
        return false;
    }

    public void ChangeStoryValue(int tempStoryValue) {
        storyValue = tempStoryValue;
    }
}
