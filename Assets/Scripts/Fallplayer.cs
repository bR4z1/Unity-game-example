using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallplayer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            PlayerHealth.playerDie = true;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

}
