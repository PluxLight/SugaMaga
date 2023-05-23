using MiniJSON;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class CostumeUI : MonoBehaviour
{
    public Image[] bodyImages;
    public GameObject[] bodyObjects;

    public Image[] ACImages;
    public GameObject[] ACObjects;
    public Image NoAC;

    public Image[] backImages;
    public GameObject[] backObjects;
    public Image NoBack;

    public Image[] eyeImages;
    public GameObject[] eyeObjects;

    public Image[] hairImages;
    public GameObject[] hairObjects;
    public Image NoHair;

    public Image[] hatImages;
    public GameObject[] hatObjects;
    public Image NoHat;

    public Image[] headImages;
    public GameObject[] headObjects;

    public Image[] mouthImages;
    public GameObject[] mouthObjects;

    public Image[] eyebrowImages;
    public GameObject[] eyebrowObjects;
    public Image NoEyebrow;

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
        GetCostume();

    }

    public void GetCostume()
    {
        ApiManager.Instance.GET("user/custom", null,delegate (UnityWebRequest request)
        {

            var dict = Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;

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

    private void CostumeSetting(GameObject[] ObjectLst,int Index, int num)
    {
        if (Index - num >= 0)
        {
        ObjectLst[Index-num].SetActive(true);
        }
    }



    public void OnClickBodyImage(Image clickedBodyImage)
    {
        for (int i = 0; i < bodyImages.Length; i++)
        {
            if (clickedBodyImage == bodyImages[i])
            {
                bodyObjects[i].SetActive(true);
                bodyIndex = i;
            }
            else
            {
                bodyObjects[i].SetActive(false);
            }
        }
    }

    public void OnClickACImage(Image clickedACImage)
    {
        if (clickedACImage == NoAC)
        {
            // 0번 보내기
            foreach(GameObject item in ACObjects)
            {
                item.SetActive(false);
                ACIndex = 0;
            }
        }
        else
        {
            for (int i = 0; i < ACImages.Length; i++)
            {
                if (clickedACImage == ACImages[i])
                {
                    ACObjects[i].SetActive(true);
                    ACIndex = i + 1;
                }
                else
                {
                    ACObjects[i].SetActive(false);
                }
            }
        }
    }

    public void OnClickBackImage(Image clickedBackImage)
    {
        if(clickedBackImage == NoBack)
        {
            foreach (GameObject item in backObjects)
            {
                item.SetActive(false);
                backIndex = 0;
            }
        }
        else
        {
            for (int i = 0; i < backImages.Length; i++)
            {
                if (clickedBackImage == backImages[i])
                {
                    backObjects[i].SetActive(true);
                    backIndex = i + 1;
                }
                else
                {
                    backObjects[i].SetActive(false);
                }
            }
        }
    }

    public void OnClickHairImage(Image clickedHairImage)
    {
        if(headIndex < 15)
        {
            if(clickedHairImage== NoHair)
            {
                foreach (GameObject item in hairObjects)
                {
                    item.SetActive(false);
                    hairIndex = 0;
                }
            }
            else
            {
                for (int i = 0; i < hairImages.Length; i++)
                {
                    if (clickedHairImage == hairImages[i])
                    {
                        hairObjects[i].SetActive(true);
                        hairIndex = i + 1;
                    }
                    else
                    {
                        hairObjects[i].SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.Log("헤드아머일 경우 불가능");
        }
    }

    public void OnClickHatImage(Image clickedHatImage)
    {
        if(clickedHatImage== NoHat)
        {
            foreach (GameObject item in hatObjects)
            {
                item.SetActive(false);
                hatIndex = 0;
            }
        }
        else
        {
            for (int i = 0; i < hatImages.Length; i++)
            {
                if (clickedHatImage == hatImages[i])
                {
                    hatObjects[i].SetActive(true);
                    hatIndex = i + 1;
                }
                else
                {
                    hatObjects[i].SetActive(false);
                }
            }
        }
    }

    public void OnClickHeadImage(Image clickedHeadImage)
    {
        for (int i = 0; i < headImages.Length; i++)
        {
            if (clickedHeadImage == headImages[i])
            {
                headObjects[i].SetActive(true);
                if (i >= 15)
                {
                    if (hairIndex != 0)
                    {
                        hairObjects[hairIndex - 1].SetActive(false);
                    }
                    if (eyebrowIndex != 0)
                    {
                        eyebrowObjects[eyebrowIndex - 1].SetActive(false);
                    }
                    mouthObjects[mouthIndex].SetActive(false);
                    eyeObjects[eyeIndex].SetActive(false);
                }
                if (headIndex >= 15)
                {
                    if (hairIndex != 0)
                    {
                        hairObjects[hairIndex-1].SetActive(true);
                    }
                    if (eyebrowIndex != 0)
                    {
                        eyebrowObjects[eyebrowIndex - 1].SetActive(true);
                    }
                    eyeObjects[eyeIndex].SetActive(true);
                    mouthObjects[mouthIndex].SetActive(true);
                }
                headIndex = i;
            }
            else
            {
                headObjects[i].SetActive(false);
            }
        }
    }

    public void OnClickMouthImage(Image clickedMouthImage)
    {
        if(headIndex < 15)
        {
            for (int i = 0; i < mouthImages.Length; i++)
            {
                if (clickedMouthImage == mouthImages[i])
                {
                    mouthObjects[i].SetActive(true);
                    mouthIndex = i;
                }
                else
                {
                    mouthObjects[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("헤드 아머");
        }
    }

    public void OnClickEyeImage(Image clickedEyeImage)
    {
        if (headIndex < 15)
        {
            for (int i = 0; i < eyeImages.Length; i++)
            {
                if (clickedEyeImage == eyeImages[i])
                {
                    eyeObjects[i].SetActive(true);
                    eyeIndex = i;
                }
                else
                {
                    eyeObjects[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("헤드 아머");
        }
    }

    public void OnClickEyebrowImage(Image clickedEyebrowImage)
    {
        if (headIndex < 15)
        {
            if(clickedEyebrowImage== NoEyebrow)
            {
                foreach (GameObject item in eyebrowObjects)
                {
                    item.SetActive(false);
                }
                eyebrowIndex = 0;
            }
            else
            {
                for (int i = 0; i < eyebrowImages.Length; i++)
                {
                    if (clickedEyebrowImage == eyebrowImages[i])
                    {
                        eyebrowObjects[i].SetActive(true);
                        eyebrowIndex = i+1;
                    }
                    else
                    {
                        eyebrowObjects[i].SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.Log("헤드 아머");
        }
    }

    public void JoinMain()
    {

        Dictionary<string, int> res = new Dictionary<string, int>() 
        { 
            {"body", bodyIndex }, 
            {"ac", ACIndex }, 
            {"back", backIndex }, 
            {"hat", hatIndex }, 
            {"head", headIndex }, 
            {"eye", eyeIndex }, 
            {"hair", hairIndex }, 
            {"mouth", mouthIndex }, 
            {"eyebrow", eyebrowIndex }, 
        };
        var data = Json.Serialize(res);
        ApiManager.Instance.PUT("user/custom", data, delegate (UnityWebRequest request)
        {
            Debug.Log(request);
        });
        PhotonNetwork.LoadLevel("MainMenuScene");
        DontDestroyOnLoad(GameObject.Find("BackgroundMusic"));
    }
}
