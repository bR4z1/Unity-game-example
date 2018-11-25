using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public delegate void HealthBarDelegate(int health);
    public static event HealthBarDelegate OnHealthChanged;

    // return lastcheckposition after Die()
    private GameMaster gm;
    public static bool playerDie;

    int health = 0;

    private void Start()
    {
        health = Rules.MAX_PLAYER_HEALTH;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointsPos;
    }

    private void Update()
    {
        if(health == 0)
        {
            Die();
        }
        if (playerDie)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                playerDie = false;
            }
        }
    }

    void Die()
    {
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        playerDie = true;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        ClampHealth();
    }

    public void Heal(int heal)
    {
        health += heal;
        ClampHealth();
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, Rules.MAX_PLAYER_HEALTH);
        OnHealthChanged(health);
    }

}
