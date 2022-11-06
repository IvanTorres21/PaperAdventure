using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SwordController : MonoBehaviour
{
    public GameObject sword;
    public GameObject endingDialogueTrigger;
    public GameObject enviromentalFire;

    public BoxCollider2D ground;

    private int hits = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hits--;
        if(hits == 0)
            FinishLevel();
        else
            sword.transform.position = new Vector3(sword.transform.position.x - 0.2f, sword.transform.position.y - 0.2f, sword.transform.position.z);
    }

    private void Update()
    {
        if(hits <= 0)
        {
            sword.transform.position = new Vector3(sword.transform.position.x, sword.transform.position.y - 4f * Time.deltaTime, sword.transform.position.z);
            sword.transform.Rotate(new Vector3(0, 0, -60 * Time.deltaTime));
        }
    }

    private void FinishLevel()
    {
        Debug.Log("Finished");
        FindObjectOfType<FireController>().setThrowingFire(false);
        StartCoroutine(StartEndSequence());
    }

    IEnumerator StartEndSequence()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<PlayerController>().canMove = false;
        FindObjectOfType<VerticalScrollCamera>().StopMusic();
        enviromentalFire.SetActive(true);
        endingDialogueTrigger.GetComponent<DialogueTrigger>().TriggerDialogue();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ground.enabled = true;
        }
    }
}
