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

    // �뿡�� ������ �Լ�
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // �뿡�� ������ �Լ� ȣ��
    public void OnClickLeaveRoom()
    {
        LeaveRoom();
    }

    // �뿡�� ���� �� ����Ǵ� �Լ�
    public override void OnLeftRoom()
    {
        vivoxManager.vivox.ChannelSession.Disconnect();
        vivoxManager.vivox.LoginSession.DeleteChannelSession(new VivoxUnity.ChannelId(vivoxManager.vivox.issuer, "room", vivoxManager.vivox.domain, VivoxUnity.ChannelType.Positional));
       // �뿡�� ������ �� ����� �ڵ�
        PhotonNetwork.LoadLevel("MainMenuScene");
    }
    public void GameStart()
    {
        // ���ӽ���
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        /* UnityEngine.UI.Text countDownText = GameObject.Find("StartCountDown").GetComponent<UnityEngine.UI.Text>();

         for (int i = 0; i < waitTime; i++)
         {
             countDownText.text = "���� ���۱��� : " + (waitTime - i).ToString() + "��";

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
