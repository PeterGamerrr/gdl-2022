using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 playerPosition;


    void Update()
    { 
        playerPosition.x = player.position.x;
        playerPosition.y = player.position.y;
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;   
    }
}
