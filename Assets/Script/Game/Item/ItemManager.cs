using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject[] itemProject;
    // Start is called before the first frame update
    void Start()
    {
        //BuildItemPrefab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildItemPrefab(){//(int tempId, Sprite tempIconSprit, Sprite tempShowSprit) {
        GameObject temp = Instantiate(itemPrefab,Vector3.zero,Quaternion.Euler(Vector3.zero));
        itemProject[9] = temp;
    }

    public int GetItemPrefabNumberById(int tempId) {
        for (int i=0;i<itemProject.Length;i++) {
            if (itemProject[i].GetComponent<Item>().id==tempId) {
                return i;
            }
        }
        return -1;
    }

    public GameObject GetItemPrefabById(int tempId) {
        for (int i=0;i<itemProject.Length;i++) {
            if (itemProject[i].GetComponent<Item>().id==tempId) {
                return itemProject[i];
            }
        }
        return null;
    }

    public Sprite GetShowSpriteByItemNumber(int tempId) {
        for (int i=0;i<itemProject.Length;i++) {
            if (itemProject[i].GetComponent<Item>().id==tempId) {
                return itemProject[i].GetComponent<Item>().showSprit;
            }
        }
        return null;
    }

}
