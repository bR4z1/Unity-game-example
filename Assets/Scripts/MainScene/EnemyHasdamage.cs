using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHasdamage : MonoBehaviour {

    Rigidbody2D rb;
    string nameEnemy;
    int health;
    // try another way animation die
    public GameObject impactEffect;
    GameObject player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Sunny");
        nameEnemy = rb.tag;
        health = EnemyDict.enemy[nameEnemy];
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "pickBoxThrow")
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        //print(health);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        player.GetComponent<Score>().AddScore(EnemyDict.Score[nameEnemy]);
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
