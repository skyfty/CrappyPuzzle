using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCity : MonoBehaviour
{
    public int id;
    public GameObject[] room;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToRoom(int tempRoomNumber) {
        if (tempRoomNumber<room.Length) {
            for (int i=0;i<room.Length;i++) {
                if (room[i].activeSelf==true) room[i].SetActive(false);
            }
            room[tempRoomNumber].SetActive(true);
        }
    }
}
