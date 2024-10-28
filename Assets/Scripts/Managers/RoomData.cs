using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomData : MonoBehaviourPunCallbacks
{
    TMP_Text RoomInfoText;
    RoomInfo _roomInfo;

    public RoomInfo RoomInfo
    {
        get { return _roomInfo; }
        set
        {
            _roomInfo = value;
            RoomInfoText.text = $"{_roomInfo.Name}({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})";
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name) );
        }
    }
    void Awake()
    {
        RoomInfoText = GetComponentInChildren<TMP_Text>();
    }
    void OnEnterRoom(string roomName)
    {
        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName, option,TypedLobby.Default);
    }
}
