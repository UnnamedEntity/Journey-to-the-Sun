using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraWorldCoord = RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord);
        cameraWorldCoord += new Vector3(0, 0, -4);
        transform.position = cameraWorldCoord;
    }
}
