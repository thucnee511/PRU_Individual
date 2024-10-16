using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float startX, endX;
    public float startY, endY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var playerX = player.transform.position.x;
        var playerY = player.transform.position.y;
        var cameraX = transform.position.x;
        var cameraY = transform.position.y;
        if (playerX > startX && playerX < endX)
        {
            cameraX = playerX;
        }
        else
        {
            if (playerX <= startX)
            {
                cameraX = startX;
            }
            if (playerX >= endX)
            {
                cameraX = endX;
            }
        }
        if (playerY > startY && playerY < endY)
        {
            cameraY = playerY;
        }
        else
        {
            if (playerY <= startY)
            {
                cameraY = startY;
            }
            if (playerY >= endY)
            {
                cameraY = endY;
            }
        }
        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
    }
}
