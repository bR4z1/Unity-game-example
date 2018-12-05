using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBoxChangelayerAndTag : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            gameObject.layer = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            gameObject.layer = 14;
        }
    }
}
