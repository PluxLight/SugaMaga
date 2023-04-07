using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using MiniJSON;
using UnityEngine.SceneManagement;
using System;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class StartCharacter : MonoBehaviour
{
    private Hashtable cp;
    public GameObject[] bodyObjects;

    public GameObject[] ACObjects;

    public GameObject[] backObjects;

    public GameObject[] eyeObjects;

    public GameObject[] hairObjects;

    public GameObject[] hatObjects;

    public GameObject[] headObjects;

    public GameObject[] mouthObjects;

    public GameObject[] eyebrowObjects;

    int bodyIndex;
    int ACIndex;
    int backIndex;
    int hatIndex;
    int headIndex;
    int eyeIndex;
    int hairIndex;
    int mouthIndex;
    int eyebrowIndex;

    public void Start()
    {

        ApiManager.Instance.Init();
        Scene sc = SceneManager.GetActiveScene();
        if (sc.name == "MainMenuScene")
        {
            Debug.Log("½ÇÇàÁß>?>");
            lobbyCostume();
        }

    }
    private void lobbyCostume()
    {
        ApiManager.Instance.GET("user/custom", null, delegate (UnityWebRequest request)
        {
            var dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            Debug.Log("-----------------");
            Debug.Log(dict);
            Debug.Log(Convert.ToInt32(dict["body"]));
            bodyIndex = Convert.ToInt32(dict["body"]);
            ACIndex = Convert.ToInt32(dict["ac"]);
            backIndex = Convert.ToInt32(dict["back"]);
            hatIndex = Convert.ToInt32(dict["hat"]);
            headIndex = Convert.ToInt32(dict["head"]);
            eyeIndex = Convert.ToInt32(dict["eye"]);
            hairIndex = Convert.ToInt32(dict["hair"]);
            mouthIndex = Convert.ToInt32(dict["mouth"]);
            eyebrowIndex = Convert.ToInt32(dict["eyebrow"]);


            CostumeSetting(bodyObjects, bodyIndex, 0);
            CostumeSetting(headObjects, headIndex, 0);
            CostumeSetting(ACObjects, ACIndex, 1);
            CostumeSetting(backObjects, backIndex, 1);
            CostumeSetting(hatObjects, hatIndex, 1);
            if (headIndex < 15)
            {
                CostumeSetting(eyeObjects, eyeIndex, 0);
                CostumeSetting(hairObjects, hairIndex, 1);
                CostumeSetting(mouthObjects, mouthIndex, 0);
                CostumeSetting(eyebrowObjects, eyebrowIndex, 1);
            }

        });
    }

    public void GetCostume(string uid)
    {
        ApiManager.Instance.GET("user/custom", null,uid ,delegate (UnityWebRequest request)
        {
            var dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            Debug.Log("-----------------");
            Debug.Log(dict);
            Debug.Log(Convert.ToInt32(dict["body"]));
            bodyIndex = Convert.ToInt32(dict["body"]);
            ACIndex = Convert.ToInt32(dict["ac"]);
            backIndex = Convert.ToInt32(dict["back"]);
            hatIndex = Convert.ToInt32(dict["hat"]);
            headIndex = Convert.ToInt32(dict["head"]);
            eyeIndex = Convert.ToInt32(dict["eye"]);
            hairIndex = Convert.ToInt32(dict["hair"]);
            mouthIndex = Convert.ToInt32(dict["mouth"]);
            eyebrowIndex = Convert.ToInt32(dict["eyebrow"]);

            CostumeSetting(bodyObjects, bodyIndex, 0);
            CostumeSetting(headObjects, headIndex, 0);
            CostumeSetting(ACObjects, ACIndex, 1);
            CostumeSetting(backObjects, backIndex, 1);
            CostumeSetting(hatObjects, hatIndex, 1);
            if (headIndex < 15)
            {
                CostumeSetting(eyeObjects, eyeIndex, 0);
                CostumeSetting(hairObjects, hairIndex, 1);
                CostumeSetting(mouthObjects, mouthIndex, 0);
                CostumeSetting(eyebrowObjects, eyebrowIndex, 1);
            }
        });
    }

    private void CostumeSetting(GameObject[] ObjectLst, int Index, int num)
    {
        if (Index - num >= 0)
        {
            ObjectLst[Index - num].SetActive(true);
        }
    }

    public void SetDataProperty(int bodyIndex,
    int ACIndex,
    int backIndex,
    int hatIndex,
    int headIndex,
    int eyeIndex,
    int hairIndex,
    int mouthIndex,
    int eyebrowIndex)
    {
        int[] custom = { bodyIndex, ACIndex, backIndex, hatIndex, headIndex, eyeIndex, hairIndex, mouthIndex, eyebrowIndex };
        cp["data"] = custom;
    }
}
