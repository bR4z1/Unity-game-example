using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTester : MonoBehaviour {

    public PlayerHealth player;
    public void Heal(int health)
    {
        while (health > 0)
        {
            player.Heal(1);
            health--;
        }
    }
    public void Hurt(int dmg)
    {
        // ???? Need divide dmg to 1 because heart did't fill if dmg>1 or odd
        while (dmg > 0)
        {
            player.TakeDamage(1);
            dmg--;
        }
    }
}
