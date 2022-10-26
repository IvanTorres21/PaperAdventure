using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3f;
    public Vector3 startPos;
    public Vector3 endPos;
    public bool movingForward = true;
    public bool isVertical = false;

    private void Start()
    {
        startPos = transform.position;

        if (!isVertical) endPos = new Vector3(endPos.x, startPos.y, startPos.z);
        else endPos = new Vector3(startPos.x, endPos.y, startPos.z);
    }

    private void Update()
    {
        movePlatform();
    }

    private void movePlatform()
    {
        Vector3 pos = (movingForward) ? endPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        if (transform.position == pos) movingForward = !movingForward;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(this.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
