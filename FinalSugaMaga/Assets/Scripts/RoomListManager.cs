using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
public class RoomListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomListContent; // 방 목록을 띄울 Content 객체
    [SerializeField] private GameObject roomListItemPrefab; // 방 목록에 사용할 프리팹

    private List<RoomInfo> roomList; // 포톤 방 목록 저장할 리스트

    // 방 목록 업데이트
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        this.roomList = roomList;

        // 방 목록 새로고침
        RefreshRoomList();
    }

    // 방 목록 새로고침
    public void RefreshRoomList()
    {
        // 기존의 방 목록 삭제
        foreach (Transform child in roomListContent.transform)
        {
            Destroy(child.gameObject);
        }

        // 방 목록 새로 생성
        foreach (RoomInfo roomInfo in roomList)
        {
            // 프리팹을 이용해 방 목록 아이템 생성
            GameObject roomListItem = Instantiate(roomListItemPrefab, roomListContent.transform);

            // 방 이름과 플레이어 수 표시
            Text[] texts = roomListItem.GetComponentsInChildren<Text>();
            texts[0].text = roomInfo.Name;
            texts[1].text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;

            // 버튼 이벤트 추가
            Button button = roomListItem.GetComponent<Button>();
            button.onClick.AddListener(() => OnRoomListItemClicked(roomInfo.Name));
        }
    }

    // 방 목록 아이템 클릭 이벤트
    public void OnRoomListItemClicked(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
