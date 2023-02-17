using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetItemPrefabNumberById(int tempId) {
        for (int i=0;i<itemPrefab.Length;i++) {
            if (itemPrefab[i].GetComponent<Item>().id==tempId) {
                return i;
            }
        }
        return -1;
    }

    public GameObject GetItemPrefabById(int tempId) {
        for (int i=0;i<itemPrefab.Length;i++) {
            if (itemPrefab[i].GetComponent<Item>().id==tempId) {
                return itemPrefab[i];
            }
        }
        return null;
    }

    public Sprite GetShowSpriteByItemNumber(int tempId) {
        for (int i=0;i<itemPrefab.Length;i++) {
            if (itemPrefab[i].GetComponent<Item>().id==tempId) {
                return itemPrefab[i].GetComponent<Item>().showSprit;
            }
        }
        return null;
    }

}
