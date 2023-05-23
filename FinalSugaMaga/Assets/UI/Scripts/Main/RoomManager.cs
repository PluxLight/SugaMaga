using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.Networking;


public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int maxPlayers = 40;

    public Text playerCount;
    public VivoxManager vivoxManager;
    public string nick;

    public void Start()
    {
        ApiManager.Instance.Init();
        ApiManager.Instance.GET("user", null, PhotonNetwork.NickName,delegate (UnityWebRequest request)
        {
            Debug.Log("�̰� �޾ƾ���" + request.downloadHandler.text);
            nick = request.downloadHandler.text;
            Debug.Log(nick + "�г��� �޾ƿ���");
        });
    }

    private void Update()
    {
        if (nick != null)
        {
            StartCoroutine(vivoxlogin());
            nick = null;
        }
    }
    private IEnumerator vivoxlogin()
    {
        Debug.Log(nick + "�г��� �޾ƿ���");
        vivoxManager.Login(nick);
        Debug.Log(nick + "�α���");
        yield break;
    }
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
        Debug.Log("������Ϸ�");
        Debug.Log("���� �濡 �ִ� �÷��̾� ��: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("�� ��ġ");
        vivoxManager.JoinChannel("room", VivoxUnity.ChannelType.NonPositional);
        Debug.Log("�񺹽� ä�� ����");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // joined a room successfully
        Debug.Log("�����οϷ�");
        Debug.Log("���� �濡 �ִ� �÷��̾� ��: " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Lobby Scene");
        }
    }

    public void JoinCostumeRoom()
    {
        PhotonNetwork.LoadLevel("Costume Scene");
    }

}
