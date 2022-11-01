using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 10f;
    private float jumpForce = 45f;
    public bool isGrounded = false;
    public bool doubleJump = false;
    public bool inLadder = false;

    [Header("Components")]
    public Rigidbody2D rb;
    private Animator animationController;
    public SpriteRenderer sprite;

    [Header("Player Stats")]
    public int hp = 100;
    public int score = 0;
    private bool gotHit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animationController = transform.Find("Sprite").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ControlAnimations();
    }

    private void Update()
    {
        JumpPlayer();
    }

    private void JumpPlayer()
    {
        //Manages the jump mechanic
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if ((!isGrounded && doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            doubleJump = false;
        }

        // Increase falling speed without having to worry about it changing the jump
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// Controls all the movement done by the player
    /// </summary>
    private void MovePlayer()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        if(inLadder)
        {
            float inputY = Input.GetAxis("Vertical");
            rb.velocity = (new Vector2(rb.velocity.x, inputY * speed));
        }

        //Flip player 
        if (inputX > 0)
            sprite.flipX = false;
        else if (inputX < 0)
            sprite.flipX = true;

       
    }

    /// <summary>
    /// Controls player animations
    /// </summary>
    private void ControlAnimations()
    {
        if(Input.GetAxis("Horizontal") != 0 && isGrounded)
        {
            animationController.Play("Player_Walk");
        } else
        {
            animationController.Play("Player_Idle");
        }
    }
    public void GetHit(int damage)
    {
        if(!gotHit)
        {
            hp -= damage;
            StartCoroutine(tookDamageRoutine());
            if (hp == 0)
            {
                //TODO: Lose condition;
            }
        }
    }

    IEnumerator tookDamageRoutine()
    {
        sprite.color = Color.red;
        gotHit = true;
        yield return new WaitForSeconds(0.8f);
        gotHit = false;
        sprite.color = Color.white;
    }

    // Trigger and Collision detection

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            GetHit(15);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.CompareTag("Ground") || collision.CompareTag("Platform") || collision.CompareTag("MovingPlatform") || collision.CompareTag("Spring"))
       {
            isGrounded = true;
            doubleJump = true;
       }

       if(collision.CompareTag("Ladder"))
        {
            inLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Platform") || collision.CompareTag("MovingPlatform") || collision.CompareTag("Spring"))
        {
            isGrounded = false;
        }

        if (collision.CompareTag("Ladder"))
        {
            inLadder = false;
        }
    }

}
