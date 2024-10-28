using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public static class TargetingLogic
{
    static public GameObject TargetEnemy(TargetType type, float minDist, Transform transform)
    {
        GameObject target = null;
        switch (type)
        {
            case TargetType.Nearest:
                target = FindNearestEnemy(minDist, transform);
                break;
            case TargetType.Random:
                target = FindRandomEnemy(minDist, transform);
                break;
        }
        if (target != null)
        {
            return target;
        }
        return null;
    }

    static GameObject FindNearestEnemy(float minDist, Transform transform)
    {
        GameObject closest = null;
        minDist = minDist!=0 ? minDist : Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float dist = Vector3.Distance(obj.transform.position, currentPos);
            if (dist <= minDist)
            {
                closest = obj;
                minDist = dist;
            }
        }
        return closest;
    }

    //find random enemy considering min distance
    static GameObject FindRandomEnemy(float minDist, Transform transform)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        minDist = minDist!=0 ? minDist : Mathf.Infinity;
        List<GameObject> candidates = new List<GameObject>();
        Vector3 currentPos = transform.position;
        foreach (GameObject obj in enemies)
        {
            float dist = Vector3.Distance(obj.transform.position, currentPos);
            if (dist <= minDist)
            {
                candidates.Add(obj);
            }
        }
        if (candidates.Count == 0)
            return null;
        int rand = Random.Range(0, candidates.Count);
        return candidates[rand];
    }
}
