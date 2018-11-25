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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!pickBoxThrow)
            {
                Physics2D.queriesStartInColliders = false;
                int mask = LayerMask.GetMask("pickBox");
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, DISTANCE, mask);
                if (hit.collider != null && hit.collider.tag == "pickBox")
                {
                    pickBoxThrow = true;
                    hit.rigidbody.mass = 0.7f;
                }
            }
            else if (!Physics2D.OverlapPoint(BoxPoint.position, notThrow))
            {
                pickBoxThrow = false;
                Invoke("ChangeMass", 2f);
                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(transform.right * throwforce * gameObject.transform.localScale.x, ForceMode2D.Impulse);
                    hit.collider.tag = "pickBoxThrow";
                }
            }

    
        }

        if (pickBoxThrow)
        {
            hit.collider.gameObject.transform.position = BoxPoint.position;
            
        }
    }

    void ChangeMass()
    {
        hit.rigidbody.mass = 1f;
        hit.collider.tag = "pickBox";
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;

    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * DISTANCE);
    //}
}
