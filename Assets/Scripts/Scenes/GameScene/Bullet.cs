using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[System.Serializable]
public class BulletInfo{
    public BulletType bulletType;
    public float damage;
    public float speed;

    public BulletInfo(BulletType bulletType, float damage, float speed){
        this.bulletType = bulletType;
        this.damage = damage;
        this.speed = speed;
    }
}

public class Bullet : MonoBehaviour
{
    public Vector2 selectedDirection;
    public BulletInfo bulletInfo;
    public GameObject targetEnemy;

    public float damage;
    // Start is called before the first frame update
    public void SetInfo(BulletInfo bulletInfo, GameObject target, Vector2 direction)
    {
        this.bulletInfo = bulletInfo;
        targetEnemy = target;
        if(bulletInfo.bulletType==BulletType.NonTargeting){
            selectedDirection = direction;
        }
    }

    void Start()
    {
        Destroy(gameObject, 10f/bulletInfo.speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletInfo.bulletType==BulletType.Targeting){
            Debug.Log("Targeting: " + targetEnemy);
            if(targetEnemy==null){
                Destroy(gameObject);
                return;
            }
            selectedDirection = targetEnemy.transform.position - transform.position;
        }
        Vector2 movement = selectedDirection.normalized * bulletInfo.speed;
        transform.Translate(movement * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if(bulletInfo.bulletType==BulletType.Targeting && other.gameObject != targetEnemy){
                return;
            }
            other.GetComponent<Enemy>().DealtDamage(damage);
            Destroy(gameObject);
        }
    }



}
