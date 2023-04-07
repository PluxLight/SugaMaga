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
            Debug.Log("이거 받아야해" + request.downloadHandler.text);
            nick = request.downloadHandler.text;
            Debug.Log(nick + "닉네임 받아오기");
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
        Debug.Log(nick + "닉네임 받아오기");
        vivoxManager.Login(nick);
        Debug.Log(nick + "로그인");
        yield break;
    }
    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
        Debug.Log("방생성완료");
        Debug.Log("현재 방에 있는 플레이어 수: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("퀵 매치");
        vivoxManager.JoinChannel("room", VivoxUnity.ChannelType.NonPositional);
        Debug.Log("비복스 채널 조인");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 조인 실패");
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        // joined a room successfully
        Debug.Log("방조인완료");
        Debug.Log("현재 방에 있는 플레이어 수: " + PhotonNetwork.CurrentRoom.PlayerCount);
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
