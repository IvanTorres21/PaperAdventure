using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachedLevelEnd : MonoBehaviour
{
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ey");
        if (collision.CompareTag("Player"))
            FindObjectOfType<GameManager>().LoadScene(nextScene);
    }
}
