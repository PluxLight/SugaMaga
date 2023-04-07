using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    public VivoxManager vivoxManager;
    public Text playerCount;
    // Start is called before the first frame update
    private int waitTime = 10;

    void Update()
    {
        CheckPlayerCount();    
    }

    public void CheckPlayerCount()
    {
        int currPlayer = PhotonNetwork.PlayerList.Length;
        int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCount.text = string.Format("[{0}/{1}]", currPlayer, maxPlayer);
    }

    // 룸에서 나가는 함수
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // 룸에서 나가는 함수 호출
    public void OnClickLeaveRoom()
    {
        LeaveRoom();
    }

    // 룸에서 나갈 때 실행되는 함수
    public override void OnLeftRoom()
    {
        vivoxManager.vivox.ChannelSession.Disconnect();
        vivoxManager.vivox.LoginSession.DeleteChannelSession(new VivoxUnity.ChannelId(vivoxManager.vivox.issuer, "room", vivoxManager.vivox.domain, VivoxUnity.ChannelType.Positional));
       // 룸에서 나갔을 때 실행될 코드
        PhotonNetwork.LoadLevel("MainMenuScene");
    }
    public void GameStart()
    {
        // 게임시작
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        /* UnityEngine.UI.Text countDownText = GameObject.Find("StartCountDown").GetComponent<UnityEngine.UI.Text>();

         for (int i = 0; i < waitTime; i++)
         {
             countDownText.text = "게임 시작까지 : " + (waitTime - i).ToString() + "초";

             yield return new WaitForSeconds(1f);
         }*/

        LoadGameRoom();

        yield break;
    }
    private void LoadGameRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }

        Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.CurrentRoom.PlayerCount);
        //PhotonNetwork.LoadLevel("GameWorld");
        PhotonNetwork.LoadLevel("GameScene");
    }
}
