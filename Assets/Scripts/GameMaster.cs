using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    private static GameMaster instatnce;
    public Vector2 lastCheckPointsPos;

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
