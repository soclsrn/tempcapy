using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Join : MonoBehaviour
{
    public void nextScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
