using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MiniJSON;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField]
    public class PlayerInfo
    {
        public string nickname;
        public int level;

        public static PlayerInfo CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<PlayerInfo>(jsonString);
        }
    }

    public string testA = "before";

    private void Start()
    {
        testA = "before ... A";
    }

    public void OnClickTest1()
    {
        ApiManager.Instance.GET("test/1", null, delegate (UnityWebRequest request)
        {
            Debug.Log(request.downloadHandler.text);
        });
    }

    public void OnClickTest2()
    {

        ApiManager.Instance.GET("test/2", null, delegate (UnityWebRequest request)
        {
            Debug.Log(request.downloadHandler.text);
        });
    }

    public void OnClickTest3()
    {
        /*Debug.Log("Onclick event 3");

        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.nickname = "unity";
        playerInfo.level = 1;
        string json = JsonUtility.ToJson(playerInfo);*/

        /*ApiManager.instance.CallApi("test/3", "GET", json, (res) =>
        {
            Debug.Log(res);
            testA = res;
        });*/

        var str = new Dictionary<string, object>();
        var res = new Dictionary<string, object>();
        str.Add("nickname", "unity");
        str.Add("level", 1);

        var data = Json.Serialize(str);

        ApiManager.Instance.GET("test/3", data, delegate (UnityWebRequest request)
        {
            /*Debug.Log(request.downloadHandler.text);*/
            var dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            Debug.Log(dict["result"]);
            Debug.Log(dict["nickname"]);
            Debug.Log(dict["level"]);

        });

    }

}
