/*
1. 登录获取token
  http访问网址 http://oss.touchmagic.cn/index/login?username=18910600417&password=123456 传入账号和密码
  返回
  {"code":1,"msg":"Login successful","time":"1663732558","data":{"token":"e867f87f-d30c-49f0-a6e4-d73ddcd03cd2","id":1,"username":"18910600417","avatar":"\/assets\/img\/avatar.png"}}
    code: 执行状态
    token: 访问密匙
    id：用户唯一id
    username: 用户名

2. 获取数据
   http访问网址 http://oss.touchmagic.cn/index/get?token=e867f87f-d30c-49f0-a6e4-d73ddcd03cd2 传入访问密匙
   返回：
   {"code":1,"msg":"Login successful","time":"1663732558","data":{"field":1}}
    code: 执行状态
    data:当前用户存储的数据

3. 保存数据
   http访问网址 http://oss.touchmagic.cn/index/set?token=e867f87f-d30c-49f0-a6e4-d73ddcd03cd2 传入访问密匙
   发送内容,可以为任意json数据， 如下
   {"field":23,
    "fensi":12,}
   返回：
   {"code":1,"msg":"Login successful","time":"1663732558"}
*/
using Assets.Script.Data;
using GameFramework;
using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityGameFramework.Runtime;


[Serializable]
public class UserData
{
    public string name = "slkdjf";
    public string id = "34234234";
    public int gold = 0;
    public int diamond = 0;
    public int star = 0;
    public int omnipotence = 0;
    public string last_poke = "";
    public List<string> pokes = null;
    public int levelBaseNumber = 0;         //next level to play
}

public class OssGetResponse : PokeResponse
{
    public UserData data;
};

public class GetDataEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(GetDataEventArgs).GetHashCode();

    public UserData Data
    {
        get;
        private set;
    }

    public static GetDataEventArgs Create(OssGetResponse e)
    {
        GetDataEventArgs args = ReferencePool.Acquire<GetDataEventArgs>();
        args.Data = e.data;
        return args;
    }

    /// <summary>
    /// ��ȡ Web ����ɹ��¼���š�
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

[Serializable]
public class LoginData
{
    public string token;
    public string username;
    public int id;
}

public class OssLoginResponse : PokeResponse
{
    public LoginData data;
}

public delegate void LoginSuccessCallback(UserData data);
public delegate void LoginFailureCallback(int code, string errorMessage);

public sealed class LoginCallbacks
{
    public LoginCallbacks(LoginSuccessCallback loginSuccessCallback)
    {
        LoginSuccessCallback = loginSuccessCallback;
    }
    public LoginCallbacks(LoginSuccessCallback loginSuccessCallback, LoginFailureCallback loginFailureCallback)
    {
        LoginSuccessCallback = loginSuccessCallback;
        LoginFailureCallback = loginFailureCallback;
    }
    //
    // Summary:
    //     ��ȡ������Դ�ɹ��ص�������
    public LoginSuccessCallback LoginSuccessCallback { get; }
    //
    // Summary:
    //     ��ȡ������Դʧ�ܻص�������
    public LoginFailureCallback LoginFailureCallback { get; }
}


public class User : MonoBehaviour
{

    private string token;

    public UserData PlayerData;

    /// <summary>
    /// Web ����ɹ��¼���š�
    /// </summary>
    public static readonly int LoginEventId = typeof(LoginData).GetHashCode();


    // Start is called before the first frame update
    void Start()
    {

    }


    public void Login(string name, string password, LoginCallbacks callback)
    {
        Framework.WebRequest.AddWebRequest(GameFramework.Utility.Text.Format("{0}/index/login?username=18910600417&password=123456", Framework.OssApiUrl), new ResponseCallbacks((data) =>
        {
            OssLoginResponse e2 = GameFramework.Utility.Json.ToObject<OssLoginResponse>(data);
            if (e2.code ==1)
            {
                token = e2.data.token;
                GetData(token, callback);
            }
            else
            {
                callback.LoginFailureCallback(e2.code, e2.msg);
            }
        }, 
        (int code, string errorMessage) =>
        {
            callback.LoginFailureCallback(code, errorMessage);
        }));
    }

    public void Logout()
    {
        string url = GameFramework.Utility.Text.Format("{0}/index/logout?token={1}", Framework.OssApiUrl, token);
        Framework.WebRequest.AddWebRequest(url, (object)"logout");
        token = "";
        PlayerData = null;
    }

    public void Pass(string id)
    {
        Framework.WebRequest.AddWebRequest(GameFramework.Utility.Text.Format("{0}/index/pass?token={1}&poke_id={2}", Framework.OssApiUrl, token, id), new ResponseCallbacks((data) =>
        {
            PokeResponse e2 = GameFramework.Utility.Json.ToObject<PokeResponse>(data);
            if (e2.code == 1)
            {
                PlayerData.last_poke = id;
                if (PlayerData.pokes == null) {
                    PlayerData.pokes = new List<string>();
                }
                PlayerData.pokes.Add(id);
            }
            else
            {
            }
        },
        (int code, string errorMessage) =>
        {
        }));
    }

    public bool IsLogin()
    {
        return token != "" && PlayerData != null;
    }

    public void SetData(object data)
    {
        StartCoroutine(post(data));
    }

    public void GetData(string token, LoginCallbacks callback)
    {
        string setReq = GameFramework.Utility.Text.Format("{0}/index/get?token={1}", Framework.OssApiUrl, token);
        Framework.WebRequest.AddWebRequest(setReq,new ResponseCallbacks((data) =>
        {
            OssGetResponse e2 = GameFramework.Utility.Json.ToObject<OssGetResponse>(data);
            if (e2.code == 1)
            {
                PlayerData = e2.data;
                callback.LoginSuccessCallback(e2.data);
            }
            else
            {
                callback.LoginFailureCallback(e2.code, e2.msg);
            }
        },
        (int code, string errorMessage) =>
        {
            callback.LoginFailureCallback(code, errorMessage);
        }));
    }

    IEnumerator post(object data)
    {
        string url = GameFramework.Utility.Text.Format("{0}/index/set?token={1}", Framework.OssApiUrl, token);
        string ggg = GameFramework.Utility.Json.ToJson(data);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(GameFramework.Utility.Converter.GetBytes(ggg));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }

    public void Save() {
        SetData(PlayerData);
    }
}
