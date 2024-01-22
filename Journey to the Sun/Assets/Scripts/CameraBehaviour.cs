using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    
    public RoomController RoomController; //Allows us to use methods from RoomController script
    public float moveSpeed = 35;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 cameraWorldCoord = RoomController.GetWorldCoord(RoomController.playerRoomCoord);
        cameraWorldCoord += new Vector3(0, 0, -4);
        Debug.Log(cameraWorldCoord);
        transform.position = Vector3.MoveTowards(transform.position, cameraWorldCoord, moveSpeed * Time.deltaTime);
    }
}
