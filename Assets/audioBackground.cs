using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioBackground : MonoBehaviour {

    private static audioBackground instatnce;

    private void Awake()
    {
        if (instatnce == null)
        {
            instatnce = this;
            DontDestroyOnLoad(instatnce);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
