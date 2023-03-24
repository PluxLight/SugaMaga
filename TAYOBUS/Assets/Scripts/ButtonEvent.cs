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

    [System.Serializable]
    public class Consum
    {
        public int consumableItemIdx;
        public string consumableName;
        public string consumableCategory;
        public int consumableValue;
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
        ApiManager.Instance.GET("user/custom", null, delegate (UnityWebRequest request)
        {
            var dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            Debug.Log(dict["uid"]);
        });
    }

    public void OnClickTest4()
    {

        var str = new Dictionary<string, object>();
        var res = new Dictionary<string, object>();
        str.Add("consumableItemIdx", 0);

        var data = Json.Serialize(str);

        ApiManager.Instance.GET("game/consume", data, delegate (UnityWebRequest request)
        {
            Debug.Log("res download text : " + request.downloadHandler.text);

            var TestTableData = JsonHelper.FromJson<Consum>(request.downloadHandler.text);
            Debug.Log($"animalData = ${TestTableData.Length}");

            for (int i = 0; i < TestTableData.Length; i++)
            {
                Debug.Log($"TestTableData[i].consumableItemIdx = {TestTableData[i].consumableItemIdx}, " +
                    $"TestTableData[i].consumableCategory = {TestTableData[i].consumableCategory}, " +
                    $"TestTableData[i].consumableValue = {TestTableData[i].consumableValue}, " +
                    $"TestTableData[i].consumableName = {TestTableData[i].consumableName}");
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
        str.Add("hair", 1);
        str.Add("cap", 1);

        var data = Json.Serialize(str);

        ApiManager.Instance.PUT("user/custom", data, delegate (UnityWebRequest request)
        {
            Debug.Log("res download text : " + request.downloadHandler.text);
        });
    }

}
