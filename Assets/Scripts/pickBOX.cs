using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickBOX : MonoBehaviour {

    bool pickBoxThrow;
    RaycastHit2D hit;
    public float DISTANCE = 2f;
    public Transform BoxPoint;
    float throwforce = 14f;
    public LayerMask notThrow;
    string hitColname;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!pickBoxThrow)
            {
                Physics2D.queriesStartInColliders = false;
                int mask = LayerMask.GetMask("pickBox");
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, DISTANCE, mask);
                if (hit.collider != null && (hit.collider.tag == "pickBox" || hit.collider.tag == "CannonBall"))
                {
                    hitColname = hit.collider.tag;
                    pickBoxThrow = true;
                    if (hitColname == "CannonBall")
                    {
                        PlayerMovement.runSpeed = 18f; // speed with cannon ball. SLOW
                    }
                }
            }
            else if (!Physics2D.OverlapPoint(BoxPoint.position, notThrow))
            {
                pickBoxThrow = false;
                if (hit.collider != null)
                {
                    if (hitColname == "CannonBall")
                    {
                        PlayerMovement.runSpeed = 40f; // speed with cannon ball
                    }
                    Invoke("ChangeMass", 1f);
                    if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.right * throwforce * gameObject.transform.localScale.x, ForceMode2D.Impulse);
                        hit.collider.tag = "pickBoxThrow";
                    }
                }
            }
        }

        if (pickBoxThrow)
        {
            if(hit.collider != null)
            {
                hit.collider.gameObject.transform.position = BoxPoint.position;
            }
        }
    }
    void ChangeMass() // I WANT STARTCOROUNTINE! or not?
    {
        if (hitColname != null)
        {
            ChangeTag(hitColname);
        }
    }

    void ChangeTag(string nameTag)
    {
         hit.collider.tag = nameTag;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;

    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * DISTANCE);
    //}
}
