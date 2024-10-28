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
        Debug.Log("������ ���� ����");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� ����");
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("�� ���� ����");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����");
        SceneManager.LoadScene("GameScene");
    }
}
