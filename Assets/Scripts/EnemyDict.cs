using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDict : MonoBehaviour {

    // Need to tag name in Unity has been equaled dictonary key
    public static Dictionary<string, int> enemy = new Dictionary<string, int>
    {
        {"opossum",100 },
        {"frog",70 },
        {"eagle",35 },
        {"Spike", 0 },
        {"FallBox",0 }
    };

    public static Dictionary<string, int> Score = new Dictionary<string, int>
    {
        {"opossum",200 },
        {"frog",300 },
        {"eagle",100 },
    };
}
