using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool verticalMovement;
    private bool forward = true;

    private void Start()
    {
        startPos = transform.position;
        if(verticalMovement)
        {
            endPos.x = transform.position.x;
            if (endPos.y < startPos.y) forward = false; 
        } else
        {
            endPos.y = transform.position.y;
            if (endPos.x < startPos.x) forward = false;
        }

       
    }

    private void Update()
    {
        Vector3 currentTarget = forward ? endPos : startPos;
        this.transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        if(transform.position == currentTarget)
        {
            forward = !forward;
        }
    }
}
