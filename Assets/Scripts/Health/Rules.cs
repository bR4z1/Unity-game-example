using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour {

    static int maxPlayerHealth;

    public static Dictionary<string, int> enemyDamage = new Dictionary<string, int>
    {
        {"opossum", 4 },
        {"frog", 2 },
        {"eagle", 1 },
        {"Spike", 3 },
        {"FallBox", 4 },
    };

    public static Dictionary<string, int> healthHeal = new Dictionary<string, int>
    {
        {"healthSmall", 1 },
        {"healthMedium", 2 },
        {"healthBig", 4 }
    };

    public static int MAX_PLAYER_HEALTH
    {
        get { return maxPlayerHealth; }
        set
        {
            value = Mathf.Clamp(value, 0, 100);
            maxPlayerHealth = value;
        }
    }
}
