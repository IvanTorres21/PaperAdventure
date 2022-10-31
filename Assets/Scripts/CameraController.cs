using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    void FixedUpdate()
    {
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        float posY;
        if(player.GetComponent<PlayerController>().isGrounded)
        {
            playerPosition = new Vector3(playerPosition.x + offset, player.transform.position.y + 4, playerPosition.z);
        } else
        {
            playerPosition = new Vector3(playerPosition.x + offset, player.transform.position.y, playerPosition.z);
        }

        if (!player.GetComponent<PlayerController>().sprite.flipX)
        {
            playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
