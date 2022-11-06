using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string nextLevelVideo;
    public float videoLength;
    public bool isPlayingVideo;

    private void Start()
    {
        if(isPlayingVideo)
        {
            StartCoroutine(DelayedLoadScene());
        }
    }

    IEnumerator DelayedLoadScene()
    {
        yield return new WaitForSeconds(videoLength);
        LoadScene(nextLevelVideo);
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
