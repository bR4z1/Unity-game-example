using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMovingInWall : MonoBehaviour
{

    Transform rb;
    public float DISTANCE = 2f;
    private bool isReady = false;
    private bool isActivate;
    private bool isBack;
    public float VectorMovingDirectionX = -1;
    float x_start_pos;

    void Start()
    {
        rb = GetComponent<Transform>();
        x_start_pos = rb.position.x;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Sunny");
        if (col.gameObject.name == "Player" 
            && !isReady // isActive and isBack overlap. (else) Platform will stack in one position.
            && player.transform.position.y >= gameObject.transform.position.y) // active top side
        {
            isActivate = true;
            isReady = true;
        }
    }

    void FixedUpdate()
    {
        if (isReady)
        {
            if (isActivate)
            {
                if(Mathf.Abs(rb.position.x - x_start_pos) <= DISTANCE)
                {
                    rb.Translate(Vector2.right * VectorMovingDirectionX * Time.smoothDeltaTime);
                }
                else
                {
                    BackPlatform();
                    isActivate = false;
                }
            }
            if (isBack)
            {
                if (Mathf.Abs(rb.position.x - x_start_pos) >= 0.01f)
                {
                    rb.Translate(Vector2.left * VectorMovingDirectionX * Time.smoothDeltaTime);
                }
                else
                {
                    isBack = false;
                    isReady = false;
                }
            }
        }
    }

    void BackPlatform()
    {
        StartCoroutine(waitSec());
    }

    IEnumerator waitSec()
    {
        yield return new WaitForSeconds(1f);
        isBack = true;
    }
}

