using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCity : MonoBehaviour
{
    public GameObject[] view;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetView(int tempViewNumber) {
        for (int i=0;i<view.Length;i++) {
            view[i].SetActive(false);
        }
        view[tempViewNumber].SetActive(true);
    }
}
