using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterCollider : MonoBehaviour
{
    public GameObject DoorController;
    DoorController doorControllerScript;

    public GameObject RoomControllerObject;
    RoomController RoomController;

    private void Start()
    {
        DoorController = GameObject.Find("DoorController");
        doorControllerScript = DoorController.GetComponent<DoorController>();

        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !RoomController.clearedRooms.Contains(doorControllerScript.currentRoom))
        {
            doorControllerScript.defeatedEnemies = false;
            doorControllerScript.EnableDoors();
        }
        
    }
}
