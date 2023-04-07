using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    [SerializeField]
    private InputField roomTitleInputField;

    [SerializeField]
    private GameObject createRoomUI;

    public void OnClickCreateRoomButton()
    {
        if(roomTitleInputField.text != "")
        {
            roomData.roomTitle = roomTitleInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
            CreateRoom();
        }
        else
        {
            roomTitleInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }


    private CreateGameRoomData roomData;
    // Start is called before the first frame update
    void Start()
    {
        roomData = new CreateGameRoomData() { maxPlayerCount = 6, roomTitle = "" };
    }

    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            int minusNum = 6+2*i;
            if (i == count - minusNum)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    // 방을 생성하는 함수
    public void CreateRoom()
    {
        // 생성할 방의 설정값
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4; // 최대 플레이어 수
        roomOptions.IsVisible = true; // 방이 목록에 보이도록 설정
        roomOptions.IsOpen = true; // 방이 입장 가능하도록 설정

        // 포톤 서버에 연결되어 있을 때만 방을 생성
        if (PhotonNetwork.IsConnected)
        {
            // 랜덤한 방 이름 생성
            string roomName = "Room_" + Random.Range(0, 1000);

            // 생성한 방으로 입장
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

    }

    // 방 생성에 실패했을 때 호출되는 콜백 함수
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);
    }

    // 방 생성에 성공했을 때 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully.");
    }
}

public class CreateGameRoomData
{
    public int maxPlayerCount;
    public string roomTitle;
}