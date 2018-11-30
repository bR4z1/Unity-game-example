using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerControll : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
