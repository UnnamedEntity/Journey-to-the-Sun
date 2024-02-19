using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterCollider : MonoBehaviour
{
    public GameObject DoorController;
    DoorController _DoorControllerScript;

    public GameObject RoomControllerObject;
    RoomController _RoomController;

    private void Start()
    {
        DoorController = GameObject.Find("DoorController");
        _DoorControllerScript = DoorController.GetComponent<DoorController>();

        RoomControllerObject = GameObject.Find("RoomController");
        _RoomController = RoomControllerObject.GetComponent<RoomController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !_RoomController.clearedRooms.Contains(_DoorControllerScript.CurrentRoom))
        {
            _DoorControllerScript.defeatedEnemies = false;
            _DoorControllerScript.EnableDoors();
        }
        
    }
}
