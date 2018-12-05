using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
    public Animator anim;

	public static float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    public GameObject player;

    //ladder
    private float inputVertical;
    Rigidbody2D rb;
    bool isclimb = false;
    private float speedLadder = 4f;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ladder")
        {
            isclimb = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Player have 2 collider, can active 2 times(give player - composite collider???)
        if (Rules.healthHeal.ContainsKey(col.gameObject.tag))
        {
            Destroy(col.gameObject);
            rb.GetComponent<HealthBarTester>().Heal(Rules.healthHeal[col.gameObject.tag]);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "ladder")
        {
            isclimb = false;
            rb.gravityScale = 3f;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (EnemyDict.enemy.ContainsKey(col.gameObject.tag))
        {
            anim.SetBool("isDamage", true);
            if (col.gameObject.transform.position.x >= rb.transform.position.x)
            {
                rb.AddForce(Vector2.left * 25f, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * 25f, ForceMode2D.Impulse);
            }
            if (col.gameObject.transform.position.y <= rb.transform.position.y)
            {
                rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            }
            rb.GetComponent<HealthBarTester>().Hurt(Rules.enemyDamage[col.gameObject.tag]);
            StartCoroutine(DelayDamageTimeAnimation());
        }
    }

    IEnumerator DelayDamageTimeAnimation()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("isDamage", false);
    }

    // Update is called once per frame
    void Update () {
        inputVertical = Input.GetAxisRaw("Vertical") * speedLadder;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        // pause
        if (!PauseMenu.GameIsPaused)
        {
            if (!isclimb)
            {
                anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

                if (Input.GetButtonDown("Jump"))
                {
                    jump = true;
                    anim.SetBool("isJumping", true);
                }

                if (Input.GetButtonDown("Crouch"))
                {
                    crouch = true;
                    player.GetComponent<CharacterController2D>().m_JumpForce = 300.0f;
                }
                else if (Input.GetButtonUp("Crouch"))
                {
                    crouch = false;
                    player.GetComponent<CharacterController2D>().m_JumpForce = 700.0f;
                }
            }
            else
            {
                if (Input.GetKey("w"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, inputVertical);
                }
                else if (Input.GetKey("s"))
                {
                    rb.velocity = new Vector2(rb.velocity.x * -1, inputVertical);
                }
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
	}

    public void OnLadderClimb(bool isclimb)
    {
        anim.SetBool("isClimb", isclimb);
    }

    public void OnLanding()
    {
        anim.SetBool("isJumping", false);
    }

    public void OnCrouching (bool isCrouching)
    {
        anim.SetBool("isCrouching", isCrouching);
    }

	void FixedUpdate ()
	{
        OnLadderClimb(isclimb);


        if (isclimb)
        {
            if (horizontalMove == 0f && inputVertical == 0f)
            {
                anim.enabled = false;
            }
            else
            {
                anim.enabled = true;
            }
        }
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
    }
}
