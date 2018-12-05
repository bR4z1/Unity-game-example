using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    Rigidbody2D rb;
    public float force = 0f; // 1 or 0

    Vector2 direction;
    bool isPUFF = false;
    GameObject Pos1;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Cannon")
        {
            Pos1 = col.gameObject;
            direction = transform.position - Pos1.transform.position;
            ForceCorrectValue();
            isPUFF = true;
            StartCoroutine(WaitTime());
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.3f);//wait impulse move ball
        isPUFF = false;
    }

    // max value charge 5
    void ForceCorrectValue()
    {
        if (force >= 1)
        {
            force = 5;
        }
        else
        {
            force = 5 * force;
        }
    }


    private void FixedUpdate()
    {
        if (isPUFF)
        {
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
        //Ray ray = new Ray(transform.position, pos1.transform.position - pos2.transform.position);

        //Debug.DrawLine(pos1.transform.position, pos2.transform.position, Color.red, 10f);
    }

}
