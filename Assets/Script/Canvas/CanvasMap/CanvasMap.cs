using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMap : MonoBehaviour
{
    public GameObject[] city;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToCityRoom(int tempCityNumber, int tempRoomNumber) {
        if (tempCityNumber<city.Length) {
            for (int i=0;i<city.Length;i++) {
                if (city[i].activeSelf==true) city[i].SetActive(false);
            }
            city[tempCityNumber].SetActive(true);
        }
        city[tempCityNumber].GetComponent<CanvasCity>().ToRoom(tempRoomNumber);
    }
}
