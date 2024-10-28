using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Attacker: ITowerAction
{
    private float timer = 0f;
    public float finalInterval = 2f;
    public float finalAmount = 1f;
    public float finalBulletSpeed = 1f;
    private GameObject bulletPrefab;

    override public void change(){

        float _interval = 1/tower.status.APS;
        float _amount = tower.status.amount;
        foreach (Effect effect in tower.effects)
        {
            if(effect == null){
                Debug.Log("Effect is null");
                continue;
            }
            Debug.Log("Adjusttower.status: " + effect.reduceInterval.ToString());
            _interval *= (1-effect.reduceInterval);
            _amount += effect.increaseAmount;
            foreach (SpecialEffect specialEffect in effect.specialEffects)
            {
                //apply special effect
            }
        }
        Debug.Log("finalInterval val: " + _interval.ToString());
        finalInterval = _interval;
        finalAmount = _amount;
        finalBulletSpeed = tower.status.bulletSpeed;
        Debug.Log("finalInterval set: " + finalInterval.ToString());
    }

    override public void init()
    {
        Debug.Log("Tower Init!");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        change();
    }

    override public void loop(){
        Debug.Log("Attacker Loop!");
        timer += Time.deltaTime;

        if (timer >= finalInterval)
        {
            timer = 0f;
            var targetEnemy = TargetingLogic.TargetEnemy(tower.status.targetType, 0f, transform);
            if(targetEnemy == null){
                return;
            }
            var dir = targetEnemy.transform.position - transform.position;
            AssaultBullet(dir, targetEnemy);
        }
    }

    void AssaultBullet(Vector2 direction, GameObject target)
    {
        var bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetInfo(new BulletInfo(tower.status.bulletType, finalAmount, finalBulletSpeed), target, direction);
    }

}
