using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 10f;
    private float jumpForce = 30f;
    public bool isGrounded = false;
    public bool doubleJump = false;
    public bool inLadder = false;

    [Header("Components")]
    public Rigidbody2D rb;
    private Animator animationController;
    private SpriteRenderer sprite;

    [Header("Player Stats")]
    private int hp = 100;
    private int score = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animationController = transform.Find("Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
        ControlAnimations();
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
        
    }

    // Trigger and Collision detection

    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.CompareTag("Ground") || collision.CompareTag("Platform") || collision.CompareTag("MovingPlatform"))
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
        if (collision.CompareTag("Ground") || collision.CompareTag("Platform") || collision.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
        }

        if (collision.CompareTag("Ladder"))
        {
            inLadder = false;
        }
    }

}
