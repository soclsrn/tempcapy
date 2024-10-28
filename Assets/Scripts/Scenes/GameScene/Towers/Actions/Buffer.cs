using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectingLogic;

public class Buffer : ITowerAction
{
    //This script is for Buffer object that give effect to target tower.
    //This script is attached to Buffer object.
    //Func 1. give selected effect to target tower.
    //Func 2. set target tower by selected logic. Ex) y+1, y-1, x+1, x-1, etc.

    public List<GameObject> targetTowers;
    public List<GameObject> doneTowers;
    // public Effect effect = Effect.Default;
    // public SelectingLogic selectingLogic = SelectingLogic.Default;

    override public void loop() {
        Debug.Log("Buffer Loop!");
        //set target tower by selected logic
        SetTargetTower();
        //give selected effect to target tower
        GiveEffect();
    }


    public void SetTargetTower()
    {
        //set target tower by selected logic
        //set targetTower by selectingLogic.SelectedTowerByLogic
        //set targetTower to selectingLogic.SelectedTowerByLogic
        targetTowers = SelectingLogic.SelectedTowerByLogic(transform.position,
            new List<Vector2> {new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0)}
        );
    }

    public void GiveEffect()
    {
        //give selected effect to target tower
        //give effect to targetTowers
        //give effect to targetTowers by effect
        foreach(GameObject targetTower in targetTowers){
            if(doneTowers.Contains(targetTower)){
                continue;
            }
            Debug.Log("GiveEffect: " + tower.status.givingEffect.reduceInterval.ToString());
            targetTower.GetComponent<Tower>().AddEffect(tower.status.givingEffect);
            doneTowers.Add(targetTower);
        }
    }
    
}
