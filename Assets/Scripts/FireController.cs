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
        while(throwFire)
        {
            yield return new WaitForSeconds(0.8f);
            int index = (int) Random.Range(0, 3);
            zones[index].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            zones[index].SetActive(false);
            fires[index].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            fires[index].SetActive(false);
        }
    }
}
