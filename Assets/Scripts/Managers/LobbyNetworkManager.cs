using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버와 연결 성공");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 입장 성공");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성 성공");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 성공");
        SceneManager.LoadScene("GameScene");
    }
}
