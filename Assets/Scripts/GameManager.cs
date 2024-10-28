using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Game;
    void Start()
    {
        Debug.Log("GameManager Start!");
        GameObject gameScene = GameObject.Find("@GameScene");
        if(gameScene == null){
            Debug.Log("GameScene not found");
        }
        else{
            Debug.Log("GameScene found");
        }
    }
    
}
