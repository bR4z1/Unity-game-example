using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivatePlatform : MonoBehaviour {

    Transform trp;
    public float DISTANCE = 1.7f;
    bool goUP = false;
    float y_start_pos;

    void Start()
    {
        trp = GetComponent<Transform>();
        y_start_pos = trp.position.y;
    }

    public void MoveUP(bool go)
    {
        goUP = go;
    }

    void FixedUpdate()
    {
        if (goUP)
        {
            if (Mathf.Abs(y_start_pos - trp.position.y) <= DISTANCE)
            {
                trp.Translate(Vector2.up * Time.smoothDeltaTime);
            }
        }
        else
        {
            if (trp.position.y >= y_start_pos)
            {
                trp.Translate(Vector2.down * Time.smoothDeltaTime);
            }
        }

    }
}
