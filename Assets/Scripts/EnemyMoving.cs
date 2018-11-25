using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour {

    float BOT_SPEED = 1.2f;
    float SENSOR_OFFSET = 0.6f;
    float currVelocity;
    public float FIXED_DISTANCE = 0.0f; // for eagle
    float rightPosition;
    float leftPosition;
    Rigidbody2D rigidBody;
    private bool m_FacingLeft = true;
    private bool isDead;
    int isRigth = 1;

    private void Start()
    {
        currVelocity = -BOT_SPEED;
        rigidBody = GetComponent<Rigidbody2D>();
        if (FIXED_DISTANCE > 0)
        {
            rightPosition = transform.position.x;
            leftPosition = rightPosition - FIXED_DISTANCE;
        }
        if (transform.rotation.y > 0)
        {
            isRigth = -1;
            m_FacingLeft = false;
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        var shift = SENSOR_OFFSET * Mathf.Sign(currVelocity);
        if (FIXED_DISTANCE > 0)
        {
            if (transform.position.x < leftPosition || transform.position.x > rightPosition)
            {
                currVelocity *= -1.0f;
                Flip();
            }
        }
        else
        {
            if ((!IsOnGround(shift) && !IsOnGround(shift * 1.2f)) || HasObsticle(shift) || HasEnemyFriend(shift))
            {
                currVelocity *= -1.0f;
                Flip();
            }
        }


        rigidBody.velocity = new Vector2(currVelocity * isRigth, rigidBody.velocity.y);
    }

    public void OnProjectileHit()
    {
        isDead = true;
    }

    public bool IsOnGround(float shift = 0.0f)
    {
        int mask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(shift, 0, 0), Vector2.down, 2f, mask);
        //Debug.DrawRay(transform.position, Vector2.down * 2f, Color.green, mask);
        return hit.collider != null;
    }

    // hit wall or object
    public bool HasObsticle(float shift)
    {
        int mask = LayerMask.GetMask("Default");
        RaycastHit2D hit = Physics2D.Raycast(transform.position , new Vector2(shift, 0).normalized * isRigth, 0.9f, mask);
        //Debug.DrawRay(transform.position, new Vector2(shift, 0).normalized * 0.9f * isRigth, Color.green, mask);
        return hit.collider != null;
    }

    // enemy hit enemy
    public bool HasEnemyFriend(float shift) 
    {
        int mask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(shift*1.4f, 0, 0) * isRigth, new Vector2(shift, 0).normalized * isRigth, 0.4f, mask);
        //Debug.DrawRay(transform.position + new Vector3(shift * 1.4f, 0, 0) * isRigth, new Vector2(shift, 0).normalized * 0.4f * isRigth, Color.green, mask);
        return hit.collider != null;
    }

    private void Flip()
    {
        m_FacingLeft = !m_FacingLeft;
        transform.Rotate(0, 180f, 0);
    }
}
