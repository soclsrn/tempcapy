using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectingLogic
    {
        //list of selected pos
        // public List<Vector2> relSelectedPos = new List<Vector2>();

        static GameObject GetTowerByPos(Vector2 pos){
            //get tower by pos
            //return tower object
            //check with raycast and tag "Tower"
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null){
                if(hit.collider.tag == "Grid"){
                    return hit.collider.GetComponent<Grid>().GetTowerObject();
                }
            }
            return null;
        }
        
        //func of select tower
        static public List<GameObject> SelectedTowerByLogic(Vector2 anchorPos, List<Vector2> relSelectedPos){
            //from anchorPos, select tower by logic
            //select targetTowers relatively by relSelectedPos 
            List<GameObject> targetTowers = new List<GameObject>();
            foreach(Vector2 relPos in relSelectedPos){
                Vector2 targetPos = anchorPos + relPos;
                GameObject targetTower = GetTowerByPos(targetPos);
                if(targetTower != null){
                    targetTowers.Add(targetTower);
                }
            }
            return targetTowers;
        }

        // public static SelectingLogic Default => new SelectingLogic {
        //     relSelectedPos = new List<Vector2> {new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0)}
        // };
    }