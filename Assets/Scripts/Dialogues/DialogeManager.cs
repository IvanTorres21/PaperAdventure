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

    private void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting");

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

        panel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
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
            yield return null;
        }
    }

    private void EndDialogue()
    {
        panel.SetActive(false);
        Debug.Log("End");
    }
}
