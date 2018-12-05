using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SpaceBrick : MonoBehaviour
{
    public float HIDE_TIME = 3.0f;
    public float RESPAWN_TIME = 3.0f;
    public float DELAY_TIME = 1.0f;
    List<SpriteRenderer> sprites;
    GameObject space_brick;
    float fadeStart = 0.0f;
    private bool isReady = false;
    private bool onTopStay = false;
    private bool isColliding = false;
    List<float> timing;

    public float secsToNext = 0.0f;
    
    private float PositionPlayer;
    private float PositionSpaceBrick;

    void Start()
    {
        sprites = new List<SpriteRenderer>();
        var sprite = gameObject.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprites.Add(sprite);
        }
        timing = new List<float>();
        timing.Add(HIDE_TIME);
        timing.Add(RESPAWN_TIME);
        timing.Add(DELAY_TIME);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        PositionPlayer = GameObject.FindGameObjectWithTag("Sunny").transform.position.y;
        PositionSpaceBrick = gameObject.transform.position.y;
        if (PositionPlayer >= PositionSpaceBrick && secsToNext <= timing.Sum() && secsToNext >= timing[0] + timing[2]) //&& isShow)
        {
            isColliding = true;
            onTopStay = true; // player staying on top side appear box
        }
        var normal = col.contacts[0].normal;
        if (normal.y <= 0 && col.gameObject.name == "Player") 
        {
            if (isColliding)
            {
                return;
            }
            else
            {
                isColliding = true;
                isReady = true;
                space_brick = gameObject;
                fadeStart = Time.time;
            }
        }
    }

    private void FixedUpdate() // check player after run function "isShow"
    {
        if (onTopStay && !isReady)
        {
            isReady = true;
            fadeStart = Time.time;
            onTopStay = false;
        }
    }

    private void Update()
    {
        if (isReady)
        {
            secsToNext += Time.deltaTime;
            if (secsToNext <= HIDE_TIME)
            {
                float alpha = 1.0f - (Time.time - fadeStart) / HIDE_TIME;
                if (alpha <= 0.05f)
                {
                    space_brick.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                    space_brick.GetComponent<Rigidbody2D>().simulated = false;
                }
                else
                {
                    foreach (var sprite in sprites)
                    {
                        sprite.color = new Color(
                            sprite.color.r,
                            sprite.color.g,
                            sprite.color.b,
                            alpha
                        );
                        alpha = 0.0f;
                    }
                }
            }
            else if (secsToNext > HIDE_TIME && secsToNext <= HIDE_TIME + DELAY_TIME)
            {
                fadeStart = Time.time;
            }
            else if (secsToNext <= timing.Sum() && secsToNext >= HIDE_TIME + DELAY_TIME)
            {
                float alpha = 0.0f + (Time.time - fadeStart) / RESPAWN_TIME;
                if (alpha < 1.0f)
                {
                    foreach (var sprite in sprites)
                    {
                        sprite.color = new Color(
                            sprite.color.r,
                            sprite.color.g,
                            sprite.color.b,
                            alpha
                        );
                        alpha = 1.0f;
                    }
                }
                space_brick.GetComponent<Rigidbody2D>().simulated = true;
            }
            else
            {
                space_brick.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1f);
                isColliding = false;
                fadeStart = 0.0f;
                secsToNext = 0.0f;
                isReady = false;
            }

        }

        if (fadeStart == 0.0f)
        {
            return;
            
        }
    }
}
