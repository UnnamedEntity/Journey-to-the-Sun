using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool defeatedEnemies = false;

    public GameObject CurrentRoom;
    public GameObject Player;
    public GameObject RoomControllerObject;
    RoomController _RoomController;

    PlayerBehaviour _PlayerBehaviour;
    ProjectileBehaviour _ProjectileBehaviour;

    
    int _randomPowerUp;

    private void Start()
    {
        Player = GameObject.Find("Player");

        RoomControllerObject = GameObject.Find("RoomController");
        _RoomController = RoomControllerObject.GetComponent<RoomController>();
        _PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
        _ProjectileBehaviour = Player.GetComponent<ProjectileBehaviour>();


        _RoomController.clearedRooms.Add(GameObject.Find("room(0.00, 0.00, 0.00)"));
    }

    void Update()
    {
        CurrentRoom = GameObject.Find($"room{_PlayerBehaviour.playerRoomCoord}");
        if (CurrentRoom.transform.childCount == 11 && !_RoomController.clearedRooms.Contains(CurrentRoom) && transform.name == CurrentRoom.name)
        {
            _RoomController.clearedRooms.Add(CurrentRoom);

            _randomPowerUp = Random.Range(1, 5);
            switch (_randomPowerUp)
            {
                case 1:
                    _PlayerBehaviour.health += 1;
                    Debug.Log("Health Up");
                    Debug.Log($"HP: {_PlayerBehaviour.health}");
                    break;
                case 2:
                    _PlayerBehaviour.movementSpeed += 2;
                    Debug.Log("Speed Up");
                    break;
                case 3:
                    _PlayerBehaviour.attackDamage += 0.5f;
                    Debug.Log("Attack Up");
                    break;
                case 4:
                    _PlayerBehaviour.shootDelay -= 0.1f;
                    Debug.Log("Shot Rate Up");
                    break;
                case 5:
                    _ProjectileBehaviour.speed += 2;
                    Debug.Log("Shot Speed Up");
                    break;
            }
        }
        if (_RoomController.clearedRooms.Contains(CurrentRoom))
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
