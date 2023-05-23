using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using MiniJSON;
using System.Collections.Generic;
using UnityEditor;
using Photon.Pun;

public class ApiManager : MonoBehaviour
{
    // If Game Start or Login, Token Update
    // example is expired token
    private static string UID = "";
    private string SERVER_URL = "https://aeoragy.com/api/";
    /*private string SERVER_URL = "http://localhost:18081/api/";*/

    static ApiManager instance;
    static GameObject container;
    static GameObject Container
    {
        get { return container; }
    }

    public void Start()
    {
        UID = GameManager.instance.getUID();

    }

    public string getUid()
    {
        return UID;
    }


    public static ApiManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "ApiManager";
                instance = container.AddComponent(typeof(ApiManager)) as ApiManager;
            }

            return instance;
        }
    }

    public void Init()
    {
        UID = GameManager.instance.getUID();
        PhotonNetwork.NickName= UID;
        Debug.Log("ÆÛÅæ ³×Æ®¿öÅ© ´Ð³ß¹Ì " + PhotonNetwork.NickName);
        Debug.Log("API Manager Get UID : " + UID);
    }

    public void InputUID(string uid)
    {
        UID = uid;
    }
    public void GET(string url, string json, Action<UnityWebRequest> callback)
    {
        if (json != null)
        {
            var dict = Json.Deserialize(json) as Dictionary<string, object>;
            List<string> keys = new List<string>(dict.Keys);

            string temp = "?";

            foreach (string key in keys)
            {
                temp += key + "=" + dict[key] + "&";
            }
            temp = temp.Substring(0, temp.Length - 1);

            url += temp;
        }

        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + url);
        request.SetRequestHeader("uid", UID);

        StartCoroutine(WaitRequest(request, callback));
    }
    public void GET(string url, string json, string nickName, Action<UnityWebRequest> callback)
    {
        if (json != null)
        {
            var dict = Json.Deserialize(json) as Dictionary<string, object>;
            List<string> keys = new List<string>(dict.Keys);

            string temp = "?";

            foreach (string key in keys)
            {
                temp += key + "=" + dict[key] + "&";
            }
            temp = temp.Substring(0, temp.Length - 1);

            url += temp;
        }

        UnityWebRequest request = UnityWebRequest.Get(SERVER_URL + url);
        request.SetRequestHeader("uid", nickName);

        StartCoroutine(WaitRequest(request, callback));
    }

    public void POST(string url, string json, Action<UnityWebRequest> callback)
    {
        UnityWebRequest request = UnityWebRequest.Post(SERVER_URL + url, json);
        request = SettingRequest(request, json);

        StartCoroutine(WaitRequest(request, callback));
    }

    public void PUT(string url, string json, Action<UnityWebRequest> callback)
    {
        UnityWebRequest request = UnityWebRequest.Put(SERVER_URL + url, json);
        request = SettingRequest(request, json);

        StartCoroutine(WaitRequest(request, callback));
    }


    public IEnumerator WaitRequest(UnityWebRequest request, Action<UnityWebRequest> callback)
    {
        yield return request.SendWebRequest();
        callback(request);
    }

    public UnityWebRequest SettingRequest(UnityWebRequest request, string json)
    {
        request.SetRequestHeader("uid", UID);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

}