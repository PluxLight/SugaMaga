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

    // ���� �����ϴ� �Լ�
    public void CreateRoom()
    {
        // ������ ���� ������
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4; // �ִ� �÷��̾� ��
        roomOptions.IsVisible = true; // ���� ��Ͽ� ���̵��� ����
        roomOptions.IsOpen = true; // ���� ���� �����ϵ��� ����

        // ���� ������ ����Ǿ� ���� ���� ���� ����
        if (PhotonNetwork.IsConnected)
        {
            // ������ �� �̸� ����
            string roomName = "Room_" + Random.Range(0, 1000);

            // ������ ������ ����
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }

    }

    // �� ������ �������� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);
    }

    // �� ������ �������� �� ȣ��Ǵ� �ݹ� �Լ�
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