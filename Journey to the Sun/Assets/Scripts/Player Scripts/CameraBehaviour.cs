using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    
    public RoomController RoomController; //Allows us to use methods from RoomController script
    public PlayerBehaviour PlayerBehaviour;
    public float moveSpeed = 35;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 cameraWorldCoord = RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord);
        cameraWorldCoord += new Vector3(0, 0, -4);
        transform.position = Vector3.MoveTowards(transform.position, cameraWorldCoord, moveSpeed * Time.deltaTime);
    }
}
