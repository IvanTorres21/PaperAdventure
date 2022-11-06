using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneController : MonoBehaviour
{
    public GameObject map;
    public GameObject phone;
    public GameObject player;


    public GameObject scene5;
    public GameObject scene6A;
    public GameObject scene6B;

    public void WasSaved()
    {

        player.GetComponent<PlayerController>().rb.isKinematic = true;
        phone.SetActive(true);
        map.SetActive(false);
        scene6A.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void WasKilled()
    {
        StartCoroutine(BeingKilled());
        scene6B.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    IEnumerator BeingKilled()
    {
        while(player.GetComponent<PlayerController>().hp > 5)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            player.GetComponent<PlayerController>().DieWithfire(3);

        }
    }
}
