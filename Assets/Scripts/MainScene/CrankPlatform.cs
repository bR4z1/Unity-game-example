using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankPlatform : MonoBehaviour {

    Transform trp;
    public float DISTANCE;
    bool goDOWN = false;
    float y_start_pos;

	void Start () {
        trp = GetComponent<Transform>();
        y_start_pos = trp.position.y;
	}

    public void MoveDown(bool go)
    {
        goDOWN = go;
    }
	
	void FixedUpdate () {
		if (goDOWN)
        {
            if(y_start_pos-trp.position.y <= DISTANCE)
            {
                trp.Translate(Vector2.down * Time.smoothDeltaTime);
            }
        }
        else
        {
            if(trp.position.y <= y_start_pos)
            {
                trp.Translate(Vector2.up * Time.smoothDeltaTime);
            }
        }
	}
}
