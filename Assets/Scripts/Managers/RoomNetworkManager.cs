using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomNetworkManager : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    private UI_GamePopup gamePopup;
    public void setplayer()
    {
        Debug.Log("플레이어 생성");
        gamePopup = FindObjectOfType<UI_GamePopup>();
        GameObject cam = GameObject.Find("Main Camera");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount == 1)
        {
            gamePopup.wait();
        }
        else
        {
            gamePopup.ready();
        }
        cam.GetComponent<CameraControler>().setPlayer();
    }
    public void press_start_button()
    {
        PV.RPC("start_game", RpcTarget.All);
    }

    [PunRPC]
    void start_game()
    {
        Managers.UI.ShowPopupUI<UI_CardGettingPopup>();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        gamePopup = FindObjectOfType<UI_GamePopup>();
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount == 2)
        {
            gamePopup.ready();
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        gamePopup = FindObjectOfType<UI_GamePopup>();
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        gamePopup.wait();
    }
    //public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
   
    //}
}
