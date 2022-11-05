using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwordController : MonoBehaviour
{
    public GameObject sword;
    private int hits = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hits--;
        if(hits == 0)
            FinishLevel();
        else
            sword.transform.position = new Vector3(sword.transform.position.x - 0.2f, sword.transform.position.y - 0.2f, sword.transform.position.z);
    }

    private void FinishLevel()
    {
        Debug.Log("Finished");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.y > 49.8f)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            Camera.main.gameObject.GetComponent<VerticalScrollCamera>().isScrolling = false;
        }
    }
}
