using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpCherry : MonoBehaviour {

    public static int value_shoot = 0;
    public Text countText;
    

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.name == "Player")
        {
            Destroy(gameObject);
            value_shoot += 1;
        }
    }

    public void SetCountText()
    {
        countText.text = " x " + value_shoot.ToString();
        countText.enabled = true;

    }

    private void Update()
    {
        SetCountText();
    }
}
