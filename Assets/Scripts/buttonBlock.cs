using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBlock : MonoBehaviour {

    Transform trb;
    float DISTANCE = 0.2f;
    private bool isActive;
    float y_start_pos;
    private bool isTopStay;
    GameObject DoorPlatform;

    void Start () {
        trb = GetComponent<Transform>();
        y_start_pos = trb.position.y;
        DoorPlatform = GameObject.FindGameObjectWithTag("DoorPlatform");
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if((col.gameObject.name == "Player" || col.gameObject.name == "crate") 
            && col.gameObject.transform.position.y >= gameObject.transform.position.y)
        {
            isActive = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Player" || col.gameObject.name == "crate")
        {
            isTopStay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTopStay = false;
    }

    void Update () {
        if (isActive)
        {
            if(Mathf.Abs(trb.position.y-y_start_pos) <= DISTANCE)
            {
                trb.Translate(Vector2.down * Time.smoothDeltaTime);
            }
            else
            {
                if (isTopStay)
                {
                    trb.Translate(Vector2.zero);
                    DoorPlatform.GetComponent<DoorActivatePlatform>().MoveUP(isTopStay);
                }
                else
                {
                    isActive = false;
                }
            }
        }
        else 
        {
            DoorPlatform.GetComponent<DoorActivatePlatform>().MoveUP(isTopStay);
            if (trb.position.y <= y_start_pos)
            {
                trb.Translate(Vector2.up * Time.smoothDeltaTime);
            }
        }
    }
}
