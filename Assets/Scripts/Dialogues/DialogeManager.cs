using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogeManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<string> names;

    public TextMeshProUGUI TMP_name;
    public TextMeshProUGUI TMP_sentence;
    public GameObject panel;
    public GameObject options;
    public GameObject FireController;

    private Dialogue dialogueX;
    
    private void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(dialogueX != null && dialogueX.hasOptions)
        {
            options.SetActive(false);
        }
        if (dialogue.hasOptions)
            FireController.SetActive(false);
        Debug.Log("Starting");
        Time.timeScale = 0;
        sentences.Clear();
        names.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialogue.speaker)
        {
            names.Enqueue(name);
        }
        dialogueX = dialogue;
        panel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if(sentences.Count == 0)
        {
            EndDialogue(dialogueX);
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        TMP_name.text = name;
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(name + ": " + sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        TMP_sentence.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            TMP_sentence.text += letter;
            yield return new WaitForSecondsRealtime(0.005f);
        }
    }

    private void EndDialogue(Dialogue dialogue)
    {
        panel.SetActive(false);
        if (!dialogue.hasOptions)
            Time.timeScale = 1;
        else
            options.SetActive(true);

        if(dialogue.endsScene)
        {
            FindObjectOfType<GameManager>().LoadScene(dialogue.nextScene);
        }
        Debug.Log("End");
    }
}
