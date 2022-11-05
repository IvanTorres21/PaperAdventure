using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrollCamera : MonoBehaviour
{
    private float speed = 1.3f;

    public bool isScrolling = false;

    private void FixedUpdate()
    {
        if(isScrolling)
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
    }
}

