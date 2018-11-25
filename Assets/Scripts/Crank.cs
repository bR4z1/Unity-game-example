using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour {

    Animator anim;
    private bool isActiveCrank;
    GameObject CrankPlatform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        CrankPlatform = GameObject.FindGameObjectWithTag("CrankPlatform");
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            isActiveCrank = true;
        }
    }

    void Update()
    {
        if (isActiveCrank)
        {
            if (Input.GetKey(KeyCode.F))
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
        isActiveCrank = false;
    }
}
