using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxEffect;
    public float parallaxEffectY = 1;

    private Transform cameraPos;
    private Vector3 cameraLastPosition;

    private void Start()
    {
        cameraPos = Camera.main.transform;
        cameraLastPosition = cameraPos.position;
    }

    private void LateUpdate()
    {
        Vector3 backgroundMove = cameraPos.position - cameraLastPosition;
        transform.position += new Vector3(backgroundMove.x * parallaxEffect, backgroundMove.y * parallaxEffectY, 0);
        cameraLastPosition = cameraPos.position;
    }
}
