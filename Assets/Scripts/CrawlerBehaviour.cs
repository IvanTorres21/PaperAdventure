using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CrawlerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 4f;
    private Rigidbody2D rb;
    public int direction = 1; // 1 -> towards right, -1 -> towards left

    [Header("Attacks")]
    private float jumpForce = 15f;
    public float attackDistance = 10f;
    bool attacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!attacking)
            WanderAround();

    }

    private void Update()
    {
        if(!attacking)
            CheckAttack();
    }

    private void CheckAttack()
    {
        RaycastHit2D hitR = Physics2D.Raycast(transform.position, Vector2.right, Mathf.Infinity, 1 << 6);
        RaycastHit2D hitL = Physics2D.Raycast(transform.position, Vector2.left, Mathf.Infinity, 1 << 6);

        if(hitR.collider != null)
        {
            float distance = Mathf.Abs(hitR.point.x - transform.position.x);
            if (distance < attackDistance)
            {
                Debug.Log("Hit player");
                attacking = true;
                StartCoroutine(AttackPlayer(1));
            }
            
            
        }
        
        if (hitL.collider != null)
        {
            float distance = Mathf.Abs(hitL.point.x - transform.position.x);
            if (distance < attackDistance)
            {
                Debug.Log("Hit player");
                attacking = true;
                StartCoroutine(AttackPlayer(-1));

            }
        }
    }

    private void WanderAround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, Mathf.Infinity, 1 << 0);
        Debug.DrawRay(transform.position, Vector2.right * direction);
        
        if (hit.collider != null)
        {
            //if(hit.collider.CompareTag("Obstacle"))
            //{
                float distance = Mathf.Abs(hit.point.x - transform.position.x);
                Debug.Log(distance);
                if (distance <= 5f)
                {
                    Debug.Log("Hit wall");
                    direction = direction * -1;
                }
            //}        
        }

        rb.velocity = new Vector2(direction * 5f, rb.velocity.y);
    }

    IEnumerator AttackPlayer(int attackDirection)
    {
        yield return new WaitForSeconds(0.6f);
        rb.AddForce(new Vector2(attackDirection * jumpForce, 0.6f * jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}
