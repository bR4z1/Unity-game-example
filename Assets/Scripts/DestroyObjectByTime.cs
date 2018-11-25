using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectByTime : MonoBehaviour {

	void Start () {
        Destroy(gameObject, 1f); // 1 second wait
	}
}
