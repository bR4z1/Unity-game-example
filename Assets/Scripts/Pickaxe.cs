using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour {

    public Animator anim;
    public GameObject pickaxe;
    bool isAnimPickaxe = false;
    GameObject tile;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "TileForDIG" && isAnimPickaxe)
        {
            tile = col.gameObject;
            Invoke("WaitSec", 1f);
        }
    }

    void WaitSec()
    {
        Destroy(tile);
        isAnimPickaxe = false;
        return;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickaxe.GetComponent<Collider2D>().enabled = true;
            anim.SetBool("isDig", true);
            pickaxe.SetActive(true);
            isAnimPickaxe = true;
            Invoke("SetActivePickAxe", 1f);
        }
    }

    void SetActivePickAxe()
    {
        anim.SetBool("isDig", false);
        pickaxe.GetComponent<Collider2D>().enabled = false;
        pickaxe.SetActive(false);
    }
}
