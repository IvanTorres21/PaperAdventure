using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrollCamera : MonoBehaviour
{
    private float speed = 1.1f;

    public bool isScrolling = true;
    private bool isPlayingMusic = true;
    public AudioSource music;

    private void Start()
    {
        StartCoroutine(PlayingMusic());
    }

    IEnumerator PlayingMusic()
    {
        yield return new WaitForSeconds(0.5f);
        isPlayingMusic = false;
    }

    private void FixedUpdate()
    {
        if(!isPlayingMusic)
        {
            music.Play();
            isPlayingMusic = true;
        }
        if(isScrolling)
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
        if (transform.position.y >= 51.47f)
            isScrolling = false;
    }

    public void StopMusic()
    {
        music.Stop();
    }
}

