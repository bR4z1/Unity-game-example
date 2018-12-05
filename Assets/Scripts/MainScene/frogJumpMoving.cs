using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogJumpMoving : MonoBehaviour {

    public float moveSpeed;

    private bool canMove;

    public Rigidbody2D frogBody;

    public Animator anim;

    private bool isIdle = true;

    private bool m_FacingLeft = false;

    float speed = 3f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        frogBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle)
        {
            anim.SetBool("isJump", false);
        }
        else
        {
            anim.SetBool("isJump", true);
            
        }
    }
    public void Move()
    {
        frogBody.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
        if (m_FacingLeft)
        {
            frogBody.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
        }
        else
        {
            frogBody.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isIdle = true;
        if (col.gameObject.tag == "Ground")
        {
            StartCoroutine(waitIdle());
        }
    }

    IEnumerator waitIdle()
    {
        yield return new WaitForSeconds(1f);
        isIdle = false;
        Move();
        Flip();
    }

    private void Flip()
    {
        m_FacingLeft = !m_FacingLeft;
        frogBody.transform.Rotate(0, 180f, 0);
    }

}
