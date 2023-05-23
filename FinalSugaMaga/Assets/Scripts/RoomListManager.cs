using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
public class RoomListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomListContent; // �� ����� ��� Content ��ü
    [SerializeField] private GameObject roomListItemPrefab; // �� ��Ͽ� ����� ������

    private List<RoomInfo> roomList; // ���� �� ��� ������ ����Ʈ

    // �� ��� ������Ʈ
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        this.roomList = roomList;

        // �� ��� ���ΰ�ħ
        RefreshRoomList();
    }

    // �� ��� ���ΰ�ħ
    public void RefreshRoomList()
    {
        // ������ �� ��� ����
        foreach (Transform child in roomListContent.transform)
        {
            Destroy(child.gameObject);
        }

        // �� ��� ���� ����
        foreach (RoomInfo roomInfo in roomList)
        {
            // �������� �̿��� �� ��� ������ ����
            GameObject roomListItem = Instantiate(roomListItemPrefab, roomListContent.transform);

            // �� �̸��� �÷��̾� �� ǥ��
            Text[] texts = roomListItem.GetComponentsInChildren<Text>();
            texts[0].text = roomInfo.Name;
            texts[1].text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;

            // ��ư �̺�Ʈ �߰�
            Button button = roomListItem.GetComponent<Button>();
            button.onClick.AddListener(() => OnRoomListItemClicked(roomInfo.Name));
        }
    }

    // �� ��� ������ Ŭ�� �̺�Ʈ
    public void OnRoomListItemClicked(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
