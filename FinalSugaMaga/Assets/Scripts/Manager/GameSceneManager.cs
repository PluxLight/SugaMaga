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

    // Item Spawn ����
    [SerializeField]
    private GameObject itemNodeGroup;
    [SerializeField]
    private GameObject OriginalWeapons;
    [SerializeField]
    private GameObject OriginalHeals;
    [SerializeField]
    private Transform itemGroup;


    // Player Spawn ����
    [SerializeField]
    private Transform[] spawnPoints;

    // ���� : <<����>> �� Find ���� ��ü ���� ������ ��õ��
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

        Debug.Log("���� �� " + PhotonNetwork.NickName);
        Debug.Log("���� ���̴�" + photonView.Owner.NickName);  //�κ�Ŵ���? nickname ������


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
        // ������ ����� ��ǥ�� ������ ��ġ
        // � ������ ��ġ�ϴ���? -> OriginalItem���� itemidx�� ��������, ������Ʈ ���� �� ����Ʈ�� �ֱ�
        // �������� ��ġ���� ���� ��� ��, ��ü ������ ��� ��

        Debug.Log("�����۽����� �۵�");

        int nodeNum = itemNodeGroup.transform.childCount;

        int healItemNum = nodeNum / 2; // ��ü �����۳���� 50%
        int WeaponItemNum = (nodeNum / 10) * 4; // ��ü �����۳���� 40%
        int noItemNode = nodeNum - healItemNum - WeaponItemNum;

        for (int i = 0; i < nodeNum; i++)
        {
            //false�϶� noItem
            //�׷��� noItem�� ��尡 ��ü ����� 10% �̻��̶�� continue
            if (!(UnityEngine.Random.value > 0.5f) && noItemNode > 0)
            {
                Debug.Log("noItem");
                noItemNode--;
                itemNodeGroup.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                Vector3 itemNodePos = itemNodeGroup.transform.GetChild(i).position;
                // true �̸� ȸ���� false �̸� ����
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

    // �κ������ �̵�
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
