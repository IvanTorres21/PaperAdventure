using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float force = 100f;
    private Animator animationController;
    private bool isCoiling = false;

    private void Start()
    {
        animationController = GetComponent<Animator>();
    }

    IEnumerator MakePlayerJump(GameObject player)
    {
        yield return new WaitForSeconds(0.8f);
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
        animationController.Play("Spring_Idle");
        yield return new WaitForSeconds(0.1f); // To make sure the player isn't on it anymore
        isCoiling = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isCoiling)
        {
            isCoiling = true;
            animationController.Play("Spring_Jump");
            StartCoroutine(MakePlayerJump(collision.gameObject));
        }
    }

}
