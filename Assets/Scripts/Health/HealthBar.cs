using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image[] hearts;
    public int healthPerHeart = 4;
    public Text restartText;

    private void Start()
    {
        restartText.text = "";
    }

    void OnEnable()
    {
        Rules.MAX_PLAYER_HEALTH = hearts.Length * healthPerHeart;
        PlayerHealth.OnHealthChanged += OnHealthChanged;
    }

    void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int health)
    {
        int heart = health / healthPerHeart; //default to lower bound
        int heartFill = health % healthPerHeart; // return the remainder of the divison

        if(health % healthPerHeart == 0)
        {
            // count start with 0. It's not fill Heart full if player healed. Need to check
            if (heart == hearts.Length)
            {
                hearts[heart - 1].fillAmount = 1;
                return;
            }
            if (heart > 0)
            {
                hearts[heart].fillAmount = 0;
                hearts[heart - 1].fillAmount = 1;
            }
            else
            {
                hearts[heart].fillAmount = 0;
            }
            return;
        }

        hearts[heart].fillAmount = heartFill / (float)healthPerHeart;
    }

    public void Restart()
    {
        restartText.text = "Plese press R to restart!";
    }

    private void Update()
    {
        if (PlayerHealth.playerDie == true)
        {
            Restart();
        }
    }
}
