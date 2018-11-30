using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour {

    Animator anim;
    private bool isActiveCrank;
    GameObject CrankPlatform;
    bool isActiveF = true; // player can't move and press F same time

    private void Start()
    {
        anim = GetComponent<Animator>();
        CrankPlatform = GameObject.FindGameObjectWithTag("CrankPlatform");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            isActiveCrank = true;
            isActiveF = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            isActiveF = false;
        }
    }

    void Update()
    {
        if (isActiveCrank)
        {
            if (Input.GetKey(KeyCode.F) && isActiveF)
            {
                anim.SetBool("CrankIsDOWN", true);
                CrankPlatform.GetComponent<CrankPlatform>().MoveDown(true);
            }
            else
            {
                anim.SetBool("CrankIsDOWN", false);
                CrankPlatform.GetComponent<CrankPlatform>().MoveDown(false);
            }
        }
    }
}
