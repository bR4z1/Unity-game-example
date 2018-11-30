using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    public bool ChargeCannon = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CannonBall" && ChargeCannon == false)
        {
            Destroy(col.gameObject);
            PlayerMovement.runSpeed = 40f; // return speed player
            ChargeCannon = true;
        }
    }


}


