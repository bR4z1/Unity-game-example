using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBigCrate : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "CannonBall")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
