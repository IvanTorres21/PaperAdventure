using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool hasBeenTriggered = false;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogeManager>().StartDialogue(dialogue);
        hasBeenTriggered = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !hasBeenTriggered)
        {
            TriggerDialogue();
        }
    }
}
