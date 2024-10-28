using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    GameObject _tileObject;

    public GameObject GetTowerObject(){
        return _tileObject;
    }
    public void setTileObject(GameObject obj){
        _tileObject = obj;
    }
    public bool isNull(){
        return _tileObject == null;
    }
}
