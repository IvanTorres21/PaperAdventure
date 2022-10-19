using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunnyController : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 4f;
    private float jumpForce = 12f;
    private bool movingForward = true;
    private int direction = 1;
    private bool isGrounded = false;
    private bool facingRight = true;
    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 targetPos;

    [Header("Components")]
    private Rigidbody2D rb;
    private GameObject playerChecker;

    [Header("Damage")]
    public int damage = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        startPos = transform.position;
        if(startPos.x < endPos.x)
        {
            facingRight = false;
            transform.localScale = new Vector3( -1, 1, 1);
        }
    }

    private void Update()
    {
        MoveBunny();
    }

    private void MoveBunny()
    {
        targetPos = movingForward ? endPos : startPos;
        

        if(targetPos.x > transform.position.x)
        {
            direction = 1;
        } else if(targetPos.x < transform.position.x)
        {
            direction = -1;
        }

        // We are using this instead of MoveTowards because this way we can have any inclination and it won't be a problem
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        if ((int) transform.position.x == (int) targetPos.x)
        {
            facingRight = !facingRight;
            movingForward = !movingForward;
            transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
        }
       
    }

    public void Attack()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetHit(damage);
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
