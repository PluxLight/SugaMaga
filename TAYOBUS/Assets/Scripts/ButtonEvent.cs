using System;
using System.Linq;
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

    [System.Serializable]
    public class TestTable
    {
        public int pk;
        public int intCol;
        public string strCol;
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
        var str = new Dictionary<string, object>();
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

    public void OnClickTest4()
    {

        var str = new Dictionary<string, object>();
        var res = new Dictionary<string, object>();
        str.Add("strCol", "unity");
        str.Add("intCol", 1);

        var data = Json.Serialize(str);

        ApiManager.Instance.GET("test/4", data, delegate (UnityWebRequest request)
        {
            Debug.Log("res download text : " + request.downloadHandler.text);

            var TestTableData = JsonHelper.FromJson<TestTable>(request.downloadHandler.text);
            Debug.Log($"animalData = ${TestTableData.Length}");

            for (int i = 0; i < TestTableData.Length; i++)
            {
                Debug.Log($"TestTableData[i].pk = {TestTableData[i].pk}, " +
                    $"TestTableData[i].intCol = {TestTableData[i].intCol}, " +
                    $"TestTableData[i].strCol = {TestTableData[i].strCol}");
            }
        });
    }

    public void OnClickTest5()
    {

        var str = new Dictionary<string, object>();
        var res = new Dictionary<string, object>();
        str.Add("strCol", "unity");
        str.Add("intCol", 1);

        var data = Json.Serialize(str);

        ApiManager.Instance.POST("test/5", data, delegate (UnityWebRequest request)
        {
            Debug.Log("res download text : " + request.downloadHandler.text);
        });
    }

    public void OnClickTest6()
    {

        var str = new Dictionary<string, object>();
        var res = new Dictionary<string, object>();
        str.Add("hair", 4);
        str.Add("cap", 4);

        var data = Json.Serialize(str);

        ApiManager.Instance.PUT("test/6", data, delegate (UnityWebRequest request)
        {
            Debug.Log("res download text : " + request.downloadHandler.text);
        });
    }

}
