using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool defeatedEnemies = false;
    bool disabledDoors = false;

    public GameObject currentRoom;
    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    public List<GameObject> clearedRooms = new List<GameObject>();

    private void Start()
    {
        Player = GameObject.Find("Player");
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
        clearedRooms.Add(GameObject.Find("room(0.00, 0.00, 0.00)"));
    }

    void Update()
    {
        currentRoom = GameObject.Find($"room{PlayerBehaviour.playerRoomCoord}");
        if (currentRoom.transform.childCount == 11 && !clearedRooms.Contains(currentRoom))
        {
            clearedRooms.Add(currentRoom);
        }
        if (clearedRooms.Contains(currentRoom))
        {
            DisableDoors();
        }
    }

    public void EnableDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door"); 

        foreach (GameObject door in doors) 
        {
            door.GetComponent<Renderer>().enabled = true;
            door.GetComponent<Collider2D>().enabled = true;
        }
    }
    public void DisableDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door"); 

        foreach (GameObject door in doors) 
        {
            door.GetComponent<Renderer>().enabled = false;
            door.GetComponent<Collider2D>().enabled = false;
        }
    }
}
