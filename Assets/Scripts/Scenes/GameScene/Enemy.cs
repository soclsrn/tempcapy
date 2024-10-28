using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float speed = 1f;
    public void DealtDamage(float damage)
    {
        health -= damage;
        Debug.Log("Dealt Damage: " + damage);
        Debug.Log("Current Health: " + health);
        if(health <= 0)
            Die();
    }

    public void Die()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log("Enemy Die!");
        Destroy(this.gameObject);
    }

    void Update()
    {
        //constantly move to (0, 0)
        Vector2 direction = new Vector2(0, 0) - new Vector2(transform.position.x, transform.position.y);
        Vector2 movement = direction.normalized * speed;
        transform.Translate(movement * Time.deltaTime);
    }
}
