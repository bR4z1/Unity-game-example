using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Score : MonoBehaviour {

    public Text scoreText;
    public Text finalScoreText;
    float time;
    int score;
    private bool timeIsEnd;

    private void Start()
    {
        score = 0;
        UpdateScore();
        time = 0;
        finalScoreText.text = "";
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score " + score;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FinalScore")
        {
            int total_score = (gem.countGem * 1000) + (PickUpCherry.value_shoot * 300) + score;

            timeIsEnd = true;

            string SumGemAndCherry = " gems x 1000 = " + (gem.countGem * 1000).ToString() 
                + Environment.NewLine +
                 " cherry x 300 = " + (PickUpCherry.value_shoot * 300).ToString();

            finalScoreText.text = "Score: " + score + Environment.NewLine
                + SumGemAndCherry
                + Environment.NewLine +
                "Total score: " + total_score
                + Environment.NewLine +
                "Total time on level: "
                + string.Format("{0:00} min {1:00} sec", (time / 60) % 60, time % 60);

            Time.timeScale = 0f; // freez game
        }
    }

    private void Update()
    {
        if (!timeIsEnd)
        {
            time += Time.deltaTime;
        }
    }
}
