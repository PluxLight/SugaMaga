using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;
using Google.MiniJSON;

public class GameSceneManager : MonoBehaviourPun
{

    // Item Spawn 변수
    [SerializeField]
    private GameObject itemNodeGroup;
    [SerializeField]
    private GameObject OriginalWeapons;
    [SerializeField]
    private GameObject OriginalHeals;
    [SerializeField]
    private Transform itemGroup;


    // Player Spawn 변수
    [SerializeField]
    private Transform[] spawnPoints;

    // 권장 : <<여기>> 의 Find 보다 객체 직접 연결을 추천함
    [SerializeField]
    private GameObject spawnPointGroup;
    public bool isConnect;

    int bodyIndex;
    int ACIndex;
    int backIndex;
    int hatIndex;
    int headIndex;
    int eyeIndex;
    int hairIndex;
    int mouthIndex;
    int eyebrowIndex;

    public static Vector3 pos;
    public static Quaternion rot;   

    IEnumerator CreatePlayer()
    {
        yield return new WaitUntil(() => isConnect);
        spawnPoints = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
       pos = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].position;
        rot = spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount].rotation;

        GameObject playerTemp = PhotonNetwork.Instantiate("Canvas", pos, rot, 0);

        Debug.Log("포톤 닉 " + PhotonNetwork.NickName);
        Debug.Log("포톡 닉이다" + photonView.Owner.NickName);  //로비매니저? nickname 가져옴


        ApiManager.Instance.GET("user/custom", null, PhotonNetwork.NickName, delegate (UnityWebRequest request)
        {
            var dict = Json
            .Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
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


        });



        GameObject.Find("CharacterManager").GetComponent<StartCharacter>().SetDataProperty(bodyIndex,
           ACIndex,
           backIndex,
           hatIndex,
           headIndex,
           eyeIndex,
           hairIndex,
           mouthIndex,
           eyebrowIndex);

    }

    void ItemSpwaner()
    {
        // 아이템 노드의 좌표에 아이템 배치
        // 어떤 아이템 배치하느냐? -> OriginalItem에서 itemidx를 랜덤선택, 오브젝트 복사 후 리스트에 넣기
        // 아이템을 배치하지 않을 노드 수, 전체 아이템 노드 수

        Debug.Log("아이템스포너 작동");

        int nodeNum = itemNodeGroup.transform.childCount;

        int healItemNum = nodeNum / 2; // 전체 아이템노드의 50%
        int WeaponItemNum = (nodeNum / 10) * 4; // 전체 아이템노드의 40%
        int noItemNode = nodeNum - healItemNum - WeaponItemNum;

        for (int i = 0; i < nodeNum; i++)
        {
            //false일때 noItem
            //그러나 noItem인 노드가 전체 노드의 10% 이상이라면 continue
            if (!(UnityEngine.Random.value > 0.5f) && noItemNode > 0)
            {
                Debug.Log("noItem");
                noItemNode--;
                itemNodeGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                Vector3 itemNodePos = itemNodeGroup.transform.GetChild(i).position;
                // true 이면 회복템 false 이면 무기
                Debug.Log("YesItem");

                if ((UnityEngine.Random.value > 0.5f))
                {
                    if (healItemNum > 0)
                    {
                        Debug.Log("heal");
                        int num = UnityEngine.Random.Range(0, OriginalHeals.transform.childCount);
                        Debug.Log(num + " " + healItemNum);
                        GameObject healitem = OriginalHeals.transform.GetChild(num).gameObject;
                        Instantiate(healitem, itemNodePos, transform.rotation, itemGroup);
                        healItemNum--;
                    }
                    else
                    {
                        Debug.Log("wp");
                        int num = UnityEngine.Random.Range(0, OriginalWeapons.transform.childCount);
                        Debug.Log(num + " " + WeaponItemNum);
                        GameObject weaponitem = OriginalWeapons.transform.GetChild(num).gameObject;
                        Instantiate(weaponitem, itemNodePos, transform.rotation, itemGroup);
                        WeaponItemNum--;
                    }
                }
                else
                {
                    if (WeaponItemNum > 0)
                    {
                        Debug.Log("wp");
                        int num = UnityEngine.Random.Range(0, OriginalWeapons.transform.childCount);
                        Debug.Log(num + " " + WeaponItemNum);
                        GameObject weaponitem = OriginalWeapons.transform.GetChild(num).gameObject;
                        Instantiate(weaponitem, itemNodePos, transform.rotation, itemGroup);
                        WeaponItemNum--;
                    }
                    else
                    {
                        Debug.Log("heal");
                        int num = UnityEngine.Random.Range(0, OriginalHeals.transform.childCount);
                        Debug.Log(num + " " + healItemNum);
                        GameObject healitem = OriginalHeals.transform.GetChild(num).gameObject;
                        Instantiate(healitem, itemNodePos, transform.rotation, itemGroup);
                        healItemNum--;
                    }
                }
            }
        }
    }

    // 로비씬으로 이동
    public void ExitGame()
    {
        SceneManager.LoadScene("Lobby");
    }
    void Awake()
    {
        ItemSpwaner();
        isConnect = true;
        StartCoroutine(CreatePlayer());

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
