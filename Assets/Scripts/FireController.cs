using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireController : MonoBehaviour
{
    public GameObject[] zones;
    public GameObject[] fires;
    private bool throwFire = true;
    public AudioSource fireAudio;


    private void Start()
    {
        setThrowingFire(true);
    }

    public void setThrowingFire(bool state)
    {
        if(state)
        {
            StartCoroutine(ThrowFire());
        } else
        {
            throwFire = false;
        }
    }

    IEnumerator ThrowFire()
    {
        yield return new WaitForSeconds(0.2f);
        while (throwFire)
        {
            fireAudio.Play();
            yield return new WaitForSeconds(1f);
            int index = (int) Random.Range(0, 3);
            zones[index].SetActive(true);
            yield return new WaitForSeconds(1.6f);
            zones[index].SetActive(false);
            fires[index].SetActive(true);
            yield return new WaitForSeconds(2.5f);
            fires[index].SetActive(false);
        }
    }
}
