using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject RoomControllerObject;
    public GameObject Player;

    public RoomController RoomController; 
    public PlayerBehaviour PlayerBehaviour;

    public float moveSpeed = 35;

    // Update is called once per frame
    void FixedUpdate()
    {
        RoomControllerObject = GameObject.Find("RoomController");
        Player = GameObject.Find("Player");
        RoomController = RoomControllerObject.GetComponent<RoomController>();
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();

        Vector3 cameraWorldCoord = RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord);
        cameraWorldCoord += new Vector3(0, 0, -4);
        transform.position = Vector3.MoveTowards(transform.position, cameraWorldCoord, moveSpeed * Time.deltaTime);
    }
}
