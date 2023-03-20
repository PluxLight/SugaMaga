using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Onclick event 1");
        Debug.Log("before testA : " + testA);

        ApiManager.instance.CallApi("test/1", "GET", null, (res) =>
        {
            testA = res;
        });

        Debug.Log("after testA : " + testA);
    }

    public void OnClickTest2()
    {
        Debug.Log("Onclick event 2");
        /*ApiManager.instance.CallApi("test/2", "GET");*/
    }

    public void OnClickTest3()
    {
        Debug.Log("Onclick event 3");

        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.nickname = "unity";
        playerInfo.level = 1;
        string json = JsonUtility.ToJson(playerInfo);

        ApiManager.instance.CallApi("test/3", "GET", json, (res) =>
        {
            Debug.Log(res);
            testA = res;
        });

    }

}
