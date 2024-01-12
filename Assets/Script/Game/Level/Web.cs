using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
    public GameObject canvasMap;
    public GameObject canvasMatch;
    public GameObject prefabCanvasCity;
    public GameObject prefabCanvasRoom;
    public GameObject prefabImageBack;
    public GameObject prefabButtonPoint;
    //public string webUri1 = "http://8.141.89.131/CrazyAdventure/indexRoom.php";
    //public string webUri1 = "http://8.141.89.131/PuzzleDesignServer/data.php";
    private string roomUrl = "http://8.141.89.131/indexProject.php";
    private string equipmentUrl = "http://8.141.89.131/indexEquipment.php";
    private string bagUrl = "http://8.141.89.131/getData.php";
    private string itemUrl = "http://8.141.89.131/indexCrop.php";

    private string fbxGetDataURI = "http://8.141.89.131/indexProject.php";
    //"id": "79",
    //"room": "two",
    //"city_id": "64",
    //"canvas_id": "79"
    private int initialProjectId = 79;
    private int initialCityId = 64;
    private int initialRoomId = 81;

    //public string JsonTest = "{\"id\": \"79\",\"city_id\": \"64\",\"canvas_id\": \"79\"}";      // \"room\": \"two\",
    //public List<ButtonPointData> buttonpoints;
    private void OnEnable()
    {
        BuildBag();
        //BuildEquipment();
        
        //EnterRoom(initialProjectId,initialCityId,initialRoomId);
        
        /*
        WWWForm roomForm = new WWWForm();
        //form.AddField("id", "79");
        roomForm.AddField("project_id", "79");
        roomForm.AddField("city_id", "64");
        roomForm.AddField("room_id", "81");
        //RenderPig();
        StartCoroutine(Post(roomUrl, roomForm, () => {
            Debug.Log("Web�����" + receiveContent);
            //��˸��Ľӿ���[]��json���������ˣ���Ҫɾ��
            receiveContent = receiveContent.Remove(0, 1);
            receiveContent = receiveContent.Remove(receiveContent.Length - 1, 1);
            //end
            ProjectData projectData = JsonUtility.FromJson<ProjectData>(receiveContent);
            BuildProject(projectData,canvasMap);
            
            Debug.Log("id:" + projectData);
            Debug.Log("id:" + projectData.id);
            Debug.Log("id:" + projectData.project);
            Debug.LogError(JsonUtility.ToJson(projectData.city[0].room[0],true));
            //Debug.Log("" + rc.city[0].room[0].children[1].buttonpoint[]);
            RenderButtonpoint(projectData.city[0].room[0].content[0].buttonPoint[0]);

            //StartCoroutine(readPictureIE(headURI + rc.city[0].room[0].children[1].buttonpoint[0].chartlet));
            StartCoroutine(DownloadModel());


            //RenderPig();
            //buttonpoints = projectData.city[0].room[0].content[0].buttonPoint;
            foreach (ButtonPointData item in projectData.city[0].room[0].content[0].buttonPoint)
            {
                Debug.Log("name:" + item.name);
                Debug.Log("x" + item.x);
                Debug.Log("y" + item.y);
                Debug.Log("picture:" + item.picture);
                Debug.Log("chartlet" + item.chartlet);
                Debug.Log("---------------------------------");
            }
            
        }));*/
    }
    public string headURI = "http://8.141.89.131";
    
    public void RenderButtonpoint(ButtonPointData item) {
        //var audioClip = Resources.Load<AudioClip>("Audio/audioClip01");
        var GOfbx = Resources.Load<GameObject>(headURI+item.picture);

    }
    
    private string modelUrl = "http://8.141.89.131/CrazyAdventure/resource/AK-74.fbx";
    private string imageUrl = "http://8.141.89.131/";
    private string savePath = "Assets/Resources/";
    private string fbxSavePath = "Assets/Resources/Models/AK-74.fbx";

    public void BuildBag() {
        WWWForm cropForm = new WWWForm();
        cropForm.AddField("project_id", "79");
        //cropForm.AddField("name", "79");
        //cropForm.AddField("id", "59");
        bagUrl = "http://8.141.89.131/API/getCrop.php";
        StartCoroutine(Post(bagUrl, cropForm, () => {
            Debug.Log("Web�����" + receiveContent);
            //��˸��Ľӿ���[]��json���������ˣ���Ҫɾ��
            receiveContent = receiveContent.Remove(0, 1);
            receiveContent = receiveContent.Remove(receiveContent.Length - 1, 1);
            BagData bagData = JsonUtility.FromJson<BagData>(receiveContent);
            Debug.Log("bagData" + bagData.column_quantity);
            Debug.Log("bagData" + bagData.data.Count);
            BuildItem(bagData.data[0].crop_id);
            //Screen.SetResolution(equipmentData.width/2,equipmentData.height/2,false);
            //canvasMap.transform.parent.gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(equipmentData.width,equipmentData.height);
        }));
    }
    public void BuildItem(int tempItemId) {
        WWWForm itemForm = new WWWForm();
        itemForm.AddField("project_id", "79");
        itemForm.AddField("name","crop");
        itemForm.AddField("id", tempItemId.ToString());
        //bagUrl = "http://8.141.89.131/API/getCrop.php";
        StartCoroutine(Post(itemUrl, itemForm, () => {
            Debug.Log("Web�����" + receiveContent);
            //��˸��Ľӿ���[]��json���������ˣ���Ҫɾ��
            receiveContent = receiveContent.Remove(0, 1);
            receiveContent = receiveContent.Remove(receiveContent.Length - 1, 1);
            //BagData bagData = JsonUtility.FromJson<BagData>(receiveContent);
            //Debug.Log("bagData" + bagData.column_quantity);
            //Debug.Log("bagData" + bagData.data.Count);
            canvasMatch.GetComponent<CanvasMatch>().AddItem(tempItemId,1);
            canvasMatch.GetComponent<CanvasMatch>().SetItem();
            Debug.Log("++++++++++++++++++++++++++++++++ "+tempItemId+" ++++++++++++++++++++++++++++++");
            //Screen.SetResolution(equipmentData.width/2,equipmentData.height/2,false);
            //canvasMap.transform.parent.gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(equipmentData.width,equipmentData.height);
        }));
    }
    public void BuildEquipment() {
        WWWForm equipmentForm = new WWWForm();
        equipmentForm.AddField("project_id", "79");
        StartCoroutine(Post(equipmentUrl, equipmentForm, () => {
            Debug.Log("Web�����" + receiveContent);
            //��˸��Ľӿ���[]��json���������ˣ���Ҫɾ��
            receiveContent = receiveContent.Remove(0, 1);
            receiveContent = receiveContent.Remove(receiveContent.Length - 1, 1);
            EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(receiveContent);
            Screen.SetResolution(equipmentData.width/2,equipmentData.height/2,false);
            canvasMap.transform.parent.gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(equipmentData.width,equipmentData.height);
        }));
    }

    public void EnterRoom(int tempProjectId, int tempCityId, int tempRoomId) {
        WWWForm roomForm = new WWWForm();
        roomForm.AddField("project_id", tempProjectId.ToString());
        roomForm.AddField("city_id", tempCityId.ToString());
        roomForm.AddField("room_id", tempRoomId.ToString());
        StartCoroutine(Post(roomUrl, roomForm, () => {
            Debug.Log("Web�����" + receiveContent);
            //��˸��Ľӿ���[]��json���������ˣ���Ҫɾ��
            receiveContent = receiveContent.Remove(0, 1);
            receiveContent = receiveContent.Remove(receiveContent.Length - 1, 1);
            //end
            ProjectData projectData = JsonUtility.FromJson<ProjectData>(receiveContent);
            BuildProject(projectData,canvasMap);
            
            Debug.Log("id:" + projectData);
            Debug.Log("id:" + projectData.id);
            Debug.Log("id:" + projectData.project);
            Debug.LogError(JsonUtility.ToJson(projectData.city[0].room[0],true));
        }));
        ShowRoom(tempProjectId,tempCityId,tempRoomId);
    }

    public void ShowRoom(int tempProjectId, int tempCityId, int tempRoomId) {
        foreach(Transform city in canvasMap.transform){
            if (city.GetComponent<CanvasCity>() != null) {
                if (city.GetComponent<CanvasCity>().id == tempCityId) {
                    city.gameObject.SetActive(true);
                    foreach(Transform room in city.transform){
                        if (room.GetComponent<CanvasRoom>() != null) {
                            if (room.GetComponent<CanvasRoom>().id == tempRoomId) {
                                room.gameObject.SetActive(true);
                            } else room.gameObject.SetActive(false);
                        }
                    }
                } else city.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator DownloadModel()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(modelUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                System.IO.File.WriteAllBytes(fbxSavePath, www.downloadHandler.data);
                Debug.Log("Model downloaded successfully!");
            }
        }
    }

    IEnumerator DownloadImage(GameObject tempImage, string picture) {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl+picture)) {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success) {
                Debug.LogError(www.error);
            } else {
                Texture2D texture2D = DownloadHandlerTexture.GetContent(www) as Texture2D;
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                tempImage.GetComponent<Image>().sprite = sprite;
                System.IO.File.WriteAllBytes(savePath+picture, www.downloadHandler.data);
                Debug.Log("Image downloaded successfully!");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Response<T>
    {
        public List<T> list;
    }

    [Serializable]
    public class FbxDatas { 
        public FBXData[] data;
    }
    [Serializable]
    public class FBXData {
        public int id;
        public int fbx_id;
        public string name;
        public string value;
        public string rules;
        public int role_id;
        public bool isScene;
    }

    public class ProjectData {
        public int id;
        public string project;
        public int user_id;
        //[SerializeField]public Equipment equipment;
        public List<CityData> city;
    }
    [Serializable]
    public class EquipmentData {
        public int id;
        public string name;
        public int width;
        public int height;
        public int x;
        public int y;
        public string color;
        public int project_id;
    
    }

    [Serializable]
    public class CityData{
        public int id;
        public string city;
        public int project_id;
        public List<RoomData> room;
    }
    [Serializable]
    public class RoomData{
        public int id;
        public string room;
        public int city_id;
        public int project_id;
        public List<ContentData> content;
        //public List<Content> content;
    }
    [Serializable]
    public class ContentData {
        public int id;
        public string children;
        public List<ImageBackData> imageBack;
        public List<ButtonPointData> buttonPoint;
        public List<StoryPointData> storyPoint;
        public List<HintPointData> hintPoint;
    }
    [Serializable]
    public class ImageBackData {
        public int id;
        //public string children;
        public int room_id;
        public int x;
        public int y;
        public string picture;
        public string width;
        public string height;
    }
    [Serializable]
    public class ButtonPointData {
        public int id;
        public int role_id;
        public int room_id;
        public int x;
        public int y;
        public float width;
        public float height;
        public string picture;
        public string chartlet;
        public string name;
        public string type;
        public string tipContent;
        public int doorCityId;
        public int doorRoomId;
        public bool isOpen;
        public int keyItem;
        public bool isKeyItemDestroy;
        public string LockText;
    }
    [Serializable]
    public class StoryPointData {
        public int id;
        public int role_id;
        public int room__id;
        public int x;
        public int y;
        public string picture;
        public string chartlet;
        public string name;
    }
    [Serializable]
    public class HintPointData {
        public int id;
        public int role_id;
        public int room__id;
        public int x;
        public int y;
        public string picture;
        public string chartlet;
        public string name;
    }
    [Serializable]
    public class BagData{
        public int id;
        public int project_id;
        public int column_quantity;
        public List<ItemData> data;
    }
    [Serializable]
    public class ItemData{
        public int id;
        public int crop_id;
        public string name;
        public string filepath;
    }
//    #region Request
    [TextArea(5,10)]

    public string receiveContent;
    IEnumerator Post(string jsonUrl, WWWForm wwwForm, UnityAction UA = null)
    {
        UnityWebRequest request = UnityWebRequest.Post(jsonUrl, wwwForm);
        Debug.Log("wwwForm "+wwwForm);
        request.timeout = 5;

        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError) {
            Debug.LogError(request.error);
        } else {
            receiveContent = request.downloadHandler.text;
            if (UA != null) {
                UA.Invoke();
            }
            Debug.Log("--------------------------------------------------------------------------");
            //JsonParse(receiveContent);
        }
        #region ERROR
        //using (UnityWebRequest www = UnityWebRequest.Post(jsonUrl, "{ \"x\": 1, \"y\": 4 }", "application/json"))
        //{
        //    yield return www.SendWebRequest();

        //    if (www.result != UnityWebRequest.Result.Success)
        //    {
        //        Debug.Log(www.error);
        //    }
        //    else
        //    {
        //        string receiveContent = www.downloadHandler.text;
        //        JsonParseOnce(receiveContent);
        //        Debug.Log("--------------------------------------------------------------------------");
        //    }
        //}
        #endregion
    }

    public void BuildProject(ProjectData tempProjectData, GameObject tempCanvasMap) {
        Debug.Log("====================   1");
        GameObject tempCanvasCity = null;
        if (tempProjectData.city.Count>0) {
            foreach(Transform city in canvasMap.transform){
                if (city.GetComponent<CanvasCity>() != null) {
                    Debug.Log("====================   1        "+tempProjectData.city[0].id);
                    if (city.GetComponent<CanvasCity>().id == tempProjectData.city[0].id) {
                        tempCanvasCity = city.gameObject;
                        break;
                    }
                }
            }
            Debug.Log("====================   2");
            if (tempCanvasCity == null) {
                Debug.Log("====================   2        "+tempProjectData.city[0].id);
                tempCanvasCity = Instantiate(prefabCanvasCity, tempCanvasMap.transform);
                tempCanvasCity.GetComponent<CanvasCity>().id = tempProjectData.city[0].id;
            }
            CityData cityData = tempProjectData.city[0];
            //Debug.LogError(JsonUtility.ToJson(cityData,true));
            BuildCity(cityData,tempCanvasCity);
        }
    }

    public void BuildCity(CityData tempCityData, GameObject tempCanvasCity) {
        //Debug.Log("city id "+tempCityData.id);
        Debug.Log("====================   3");
        GameObject tempCanvasRoom = null;
        //for (int i=0;i<tempCityData.room.Count;i++) {
        if (tempCityData.room.Count>0) {
            foreach(Transform room in canvasMap.transform){
                if (room.GetComponent<CanvasRoom>() != null) {
                    if (room.GetComponent<CanvasRoom>().id == tempCityData.room[0].id) {
                        tempCanvasRoom = room.gameObject;
                        break;
                    }
                }
            }
            Debug.Log("====================   4");
            Debug.Log("bbbb  "+tempCanvasRoom);
            if (tempCanvasRoom == null) {
                tempCanvasRoom = Instantiate(prefabCanvasRoom, tempCanvasCity.transform);
                tempCanvasRoom.GetComponent<CanvasRoom>().id = tempCityData.room[0].id;
                Debug.Log("cccc  "+tempCanvasRoom);
            }
            RoomData roomData = tempCityData.room[0];
            //Debug.LogError(JsonUtility.ToJson(cityData,true));
            Debug.Log("dddd  "+tempCanvasRoom);
            BuildRoom(roomData,tempCanvasRoom);
        }
    }

    public void BuildRoom(RoomData tempRoomData, GameObject tempCanvasRoom) {
        Debug.Log("eeee  "+tempCanvasRoom);
        /*
        Debug.Log("imageBack id "+tempRoomData.content[0].imageBack.Count);//[0].imageBack[0].id);
        Debug.Log("imageBack id "+tempRoomData.content[1].imageBack.Count);
        Debug.Log("imageBack id "+tempRoomData.content[2].imageBack.Count);
        Debug.Log("imageBack id "+tempRoomData.content[3].imageBack.Count);
        Debug.Log("imageBack id "+tempRoomData.content[0].buttonPoint.Count);//[0].imageBack[0].id);
        Debug.Log("imageBack id "+tempRoomData.content[1].buttonPoint.Count);
        Debug.Log("imageBack id "+tempRoomData.content[2].buttonPoint.Count);
        Debug.Log("imageBack id "+tempRoomData.content[3].buttonPoint.Count);
        */
        bool isHave = false;
        //imageBack
        Debug.Log("====================   5");
        foreach(Transform content in tempCanvasRoom.transform){
            if (content.GetComponent<ImageBack>() != null) {
                if (content.GetComponent<ImageBack>().id == tempRoomData.content[0].imageBack[0].id) {
                    isHave = true;
                    break;
                }
            }
        }
        Debug.Log("====================   6");
        if (isHave == false) {
            GameObject tempImageBack = Instantiate(prefabImageBack, tempCanvasRoom.transform);
            StartCoroutine(DownloadImage(tempImageBack, tempRoomData.content[0].imageBack[0].picture));
            tempImageBack.GetComponent<ImageBack>().id = tempRoomData.content[0].imageBack[0].id;
            tempImageBack.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(tempRoomData.content[0].imageBack[0].x,tempRoomData.content[0].imageBack[0].y,0.0f),Quaternion.Euler(0.0f,0.0f,0.0f));
            tempImageBack.GetComponent<RectTransform>().sizeDelta = new Vector2(float.Parse(tempRoomData.content[0].imageBack[0].width),float.Parse(tempRoomData.content[0].imageBack[0].height));
            
            
            //tempImageBack.GetComponent<Image>().SetNativeSize();
        }
        //buttonPoint
        for (int i=0;i<tempRoomData.content[1].buttonPoint.Count;i++) {
            isHave = false;
            foreach(Transform content in tempCanvasRoom.transform){
                if (content.GetComponent<ButtonPoint>() != null) {
                    if (content.GetComponent<ButtonPoint>().id == tempRoomData.content[1].buttonPoint[i].id) {
                        isHave = true;
                        break;
                    }
                }
            }
            if (isHave == false) {
                GameObject tempButtonPoint = Instantiate(prefabButtonPoint, tempCanvasRoom.transform);
                StartCoroutine(DownloadImage(tempButtonPoint, tempRoomData.content[1].buttonPoint[i].picture));
                tempButtonPoint.GetComponent<ButtonPoint>().id = tempRoomData.content[1].buttonPoint[i].id;
                tempButtonPoint.GetComponent<ButtonPoint>().canvasMatch = canvasMatch;
                tempButtonPoint.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(tempRoomData.content[1].buttonPoint[i].x,tempRoomData.content[1].buttonPoint[i].y,0.0f),Quaternion.Euler(0.0f,0.0f,0.0f));
                tempButtonPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(tempRoomData.content[1].buttonPoint[i].width,tempRoomData.content[1].buttonPoint[i].height);
                switch (tempRoomData.content[1].buttonPoint[i].type) {
                    case "Door":
                        tempButtonPoint.GetComponent<ButtonPoint>().buttonPointType = ButtonPoint.ButtonPointType.Door;
                        tempButtonPoint.GetComponent<ButtonPointDoor>().enabled = true;
                        tempButtonPoint.GetComponent<ButtonPointDoor>().cityId = tempRoomData.content[1].buttonPoint[i].doorCityId;
                        tempButtonPoint.GetComponent<ButtonPointDoor>().roomId = tempRoomData.content[1].buttonPoint[i].doorRoomId;
                        break;
                    case "Tip":
                        tempButtonPoint.GetComponent<ButtonPoint>().buttonPointType = ButtonPoint.ButtonPointType.Tip;
                        tempButtonPoint.GetComponent<ButtonPointTip>().enabled = true;
                        tempButtonPoint.GetComponent<ButtonPointTip>().tipTextKey = tempRoomData.content[1].buttonPoint[i].tipContent;
                        break;
                    default:
                        break;
                }
            }
        }
        //storyPoint
        //hintPoint
    }

}