using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyRoomManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameText;
    Dictionary<string, GameObject> Room_Dictionary = new Dictionary<string, GameObject>();
    public GameObject roomPrefab;
    public Transform scrollContent;
    public override void OnRoomListUpdate(List<RoomInfo> roomlist)
    {
        Debug.Log("방 목록 갱신");
        GameObject tempRoom = null;
        foreach (var room in roomlist)
        {
            if (room.RemovedFromList == true)
            {
                Room_Dictionary.TryGetValue(room.Name,out tempRoom);
                Destroy(tempRoom);
                Room_Dictionary.Remove(room.Name);
                Debug.Log("방이 삭제됨");
            }
            else
            {
                if (Room_Dictionary.ContainsKey(room.Name))
                {
                    Room_Dictionary.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
                else
                {
                    GameObject manyroom = Instantiate(roomPrefab,scrollContent);
                    manyroom.GetComponent<RoomData>().RoomInfo = room;
                    Room_Dictionary.Add(room.Name, manyroom);
                }
             
            }
        }
    }

    public void MakeRoom()
    {
        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 2;
        if (string.IsNullOrEmpty(roomNameText.text))
        {
            roomNameText.text = $"Room_{Random.Range(1, 100):000}";
        }
        PhotonNetwork.CreateRoom(roomNameText.text, option);
    }
}
