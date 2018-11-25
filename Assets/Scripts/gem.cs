using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gem : MonoBehaviour {

    public static int countGem = 0;
    public Text countText;

    private void Start()
    {
        countText.text = "x 0";
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (countGem <= 9 && collision.gameObject.tag == "Sunny")
        {
            Destroy(gameObject);
            countGem++;
        }
        
    }

    public void SetCountText()
    {
        if (countGem == 9)
        {
            countText.text = "You have max gems!!!";
            countGem = 9;
        }
        else
        {
            countText.text = "You have - " + countGem.ToString() + " gems";
        }
    }

    private void Update()
    {
        SetCountText();
    }
}
