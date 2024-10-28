using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Tower : MonoBehaviour
{
    
    GameObject tile;
    public List<GameObject> actions;
    public List<GameObject> actionObjects;

    public List<Effect> effects = new List<Effect>();
    public Status status;

    public void SetTile(GameObject tile)
    {
        this.tile = tile;
    }



    // Start is called before the first frame update
    virtual public void init()
    {
        Debug.Log("Tower Init!");
        //delete all children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        actionObjects.Clear();
        foreach(GameObject actionPrefab in actions)
        {
            var actionObject = Instantiate(actionPrefab, transform);
            actionObjects.Add(actionObject);
            actionObject.GetComponent<ITowerAction>().SetTower(this);
        }
    }

    // void lateInit()
    // {
    //     AdjustStatus();
    // }

    void Start()
    {
        init();
        // lateInit();
    }

    public void AddEffect(Effect effect)
    {
        Debug.Log("AddEffect: " + effect.reduceInterval.ToString());
        effects.Add(effect);
        AdjustStatus();
    }

    public void RemoveEffect(Effect effect)
    {
        effects.Remove(effect);
        AdjustStatus();
    }

    public void AdjustStatus()
    {
        foreach(GameObject actionObject in actionObjects)
        {
            actionObject.GetComponent<ITowerAction>().change();
        }
        //amount = amount;
    }
}
