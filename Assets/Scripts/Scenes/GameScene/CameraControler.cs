using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraControler : MonoBehaviour
{
    GameObject playerPrefab;
    bool SetCam = false;
    public Transform player;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;
    private void FixedUpdate()
    {
        if (SetCam){
            Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }

    public void setPlayer()
    {
        GameObject Player = PhotonNetwork.Instantiate("Prefabs/Player", new Vector3(0, 0, 0), Quaternion.identity);
        player =  Player.transform;
        SetCam = true;
    }
}
