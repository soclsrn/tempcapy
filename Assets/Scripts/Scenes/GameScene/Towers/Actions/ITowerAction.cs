using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ITowerAction : MonoBehaviour
{
    //fordable inspector method

    // public bool isAttacker = false;


    public Tower tower;

    public void SetTower(Tower tower){
        this.tower = tower;
    }
    
    
    void Start()
    {
        init();
    }

    virtual public void init(){

    }


    
    void Update()
    {
        loop();
    }

    virtual public void loop(){
        
    }

    virtual public void change(){

    }

    
}
