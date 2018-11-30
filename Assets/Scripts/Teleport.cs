using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public GameObject tl;
    private bool isActive;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Sunny");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            isActive = false;
        }
    }

    public void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.transform.position = tl.transform.position;
                isActive = false;
            }
        }
    }

}
