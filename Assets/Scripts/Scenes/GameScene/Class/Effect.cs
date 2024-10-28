using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[System.Serializable]
public class Effect{
    public float reduceInterval = 0f; //0~1
    public float increaseAmount = 0; //0~n

    //special effects with dictionary
    public List<SpecialEffect> specialEffects = new List<SpecialEffect>(); //ex) {"slow", 0.5f}

    public Effect(float reduceInterval, float increaseAmount,List<SpecialEffect> specialEffects){
        this.reduceInterval = reduceInterval;
        this.increaseAmount = increaseAmount;
        this.specialEffects = specialEffects;
    }
}

[System.Serializable]
public class SpecialEffect{
    public string name;
    public float amount;

    public SpecialEffect(string name, float amount){
        this.name = name;
        this.amount = amount;
    }
}

[System.Serializable]
public class Status{
    public float amount;
    public float APS;
    public float range;
    public float bulletSpeed;
    public Effect givingEffect;

    public TargetType targetType;
    public BulletType bulletType;

    // public static Status Default => new Status {
    //     amount = 1f,
    //     APS = 1f,
    //     range = 1f
    // };
}

public class AttackerData: Status{}
