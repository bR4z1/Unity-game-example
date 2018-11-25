using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

    public float distance = 4f;

    Rigidbody2D rb2D;
    GameObject enemyObject;
    Animator anim;
    bool isDelete = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    /// 
    /// ANOTHER VARIANT trigger head-jump-destroy
    /// Check distance top col and bot player
    /// 
    ////////////////////////////////////////////////////////////////////////////////////////// 
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    var diff = Mathf.Abs(transform.position.y - coll.gameObject.transform.position.y);
    //    if (diff < 0.5f)
    //    {
    //
    //    }
    //}
    // another way animate death
    //
    // public GameObject deathEffect;
    // Instantiate(deathEffect, transform.position, Quaternion.identity);
    // Destroy(gameObject);
    //

    private void OnTriggerEnter2D(Collider2D col)
    {
        // DESTROY enemy from jump
        if (EnemyDict.enemy.ContainsKey(col.gameObject.tag))
        {
            enemyObject = col.gameObject;
            enemyObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            enemyObject.GetComponent<PolygonCollider2D>().enabled = false;
            anim = enemyObject.GetComponent<Animator>();
            StartCoroutine(timeForAnimDeath());
            anim.SetTrigger("trigger_death");
            var enemyVelocity = enemyObject.GetComponent<EnemyMoving>();
            if(enemyVelocity != null)
            {
                enemyVelocity.OnProjectileHit();
            }
        }
    }
    IEnumerator timeForAnimDeath()
    {
        yield return new WaitForSeconds(0.3f);
        isDelete = true;
    }

    void FixedUpdate()
    {
        int mask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down),1.1f, mask);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down), Color.green, 10f);
        // jump after destroy enemy
        if (hit.collider != null)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y + 3.0f);
            //rb2D.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        }
        if (isDelete)
        {
            if(enemyObject != null)
            {
                gameObject.GetComponent<Score>().AddScore(EnemyDict.Score[enemyObject.tag]);
            }
            Destroy(enemyObject);
            isDelete = false;

        }
    }
}
