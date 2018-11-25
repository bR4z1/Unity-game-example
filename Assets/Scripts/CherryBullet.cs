using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBullet : MonoBehaviour {

    public float speed = 1f;
    public Rigidbody2D rb;
    public static int cherryDamage = 35;

    void Start () {
        rb.AddRelativeForce(transform.right * speed, ForceMode2D.Impulse);
        StartCoroutine(DeleteCherry(gameObject));
        PickUpCherry.value_shoot--;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Middle") {
            //rb.velocity = new Vector2(0,0);
            rb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // ?
        }
        if (EnemyDict.enemy.ContainsKey(col.gameObject.tag))
        {
            EnemyHasdamage enemy = col.gameObject.GetComponent<EnemyHasdamage>();
            enemy.name = col.gameObject.tag;
            if (enemy != null)
            {
                Destroy(gameObject);
                enemy.TakeDamage(cherryDamage);
            }
        }
    }

    IEnumerator DeleteCherry(GameObject g)
    {
        yield return new WaitForSeconds(3f);
        Destroy(g);
    }
}
