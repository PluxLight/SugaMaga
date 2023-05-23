using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameExit : MonoBehaviour
{
    public int curPlayer = PhotonNetwork.PlayerList.Length;
    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
        curPlayer -= 1;
        SceneManager.LoadScene("MainMenuScene"); // 로비씬으로 이동
        Debug.Log("포톤 네트워크 방 떠남");
    }
}
