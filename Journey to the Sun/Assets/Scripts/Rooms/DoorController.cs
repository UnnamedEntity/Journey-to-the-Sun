using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool defeatedEnemies = false;

    public GameObject currentRoom;
    public GameObject Player;
    public GameObject RoomControllerObject;
    RoomController RoomController;

    PlayerBehaviour PlayerBehaviour;
    ProjectileBehaviour ProjectileBehaviour;

    

    int randomPowerUp;

    private void Start()
    {
        Player = GameObject.Find("Player");

        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
        ProjectileBehaviour = Player.GetComponent<ProjectileBehaviour>();


        RoomController.clearedRooms.Add(GameObject.Find("room(0.00, 0.00, 0.00)"));
    }

    void Update()
    {
        currentRoom = GameObject.Find($"room{PlayerBehaviour.playerRoomCoord}");
        if (currentRoom.transform.childCount == 11 && !RoomController.clearedRooms.Contains(currentRoom) && transform.name == currentRoom.name)
        {
            RoomController.clearedRooms.Add(currentRoom);

            randomPowerUp = Random.Range(1, 5);
            switch (randomPowerUp)
            {
                case 1:
                    PlayerBehaviour.health += 1;
                    Debug.Log("Health Up");
                    Debug.Log($"HP: {PlayerBehaviour.health}");
                    break;
                case 2:
                    PlayerBehaviour.movementSpeed += 2;
                    Debug.Log("Speed Up");
                    break;
                case 3:
                    PlayerBehaviour.attackDamage += 0.5f;
                    Debug.Log("Attack Up");
                    break;
                case 4:
                    PlayerBehaviour.shootDelay -= 0.1f;
                    Debug.Log("Shot Rate Up");
                    break;
                case 5:
                    ProjectileBehaviour.speed += 2;
                    Debug.Log("Shot Speed Up");
                    break;
            }
        }
        if (RoomController.clearedRooms.Contains(currentRoom))
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
