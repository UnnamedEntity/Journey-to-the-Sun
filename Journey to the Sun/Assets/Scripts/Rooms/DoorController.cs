using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
                    _PlayerBehaviour.Health += 1;
                    Debug.Log($"HP: {_PlayerBehaviour.Health}");
                    StartCoroutine(HealthUp());
                    break;
                case 2:
                    _PlayerBehaviour.movementSpeed += 2;
                    StartCoroutine(SpeedUp());
                    break;
                case 3:
                    _PlayerBehaviour.attackDamage += 0.5f;
                    StartCoroutine(AttackUp());
                    break;
                case 4:
                    _PlayerBehaviour.shotRate -= 0.1f;
                    StartCoroutine(ShotRateUp());
                    break;
                case 5:
                    _ProjectileBehaviour.speed += 2;
                    StartCoroutine(SpeedUp());
                    break;
            }
        }
        if (_RoomController.clearedRooms.Contains(CurrentRoom))
        {
            DisableDoors();
        }
    }

    IEnumerator HealthUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HealthUpScene", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.75f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("HealthUpScene");
    }

    IEnumerator AttackUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("AttackUpScene", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.75f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("AttackUpScene" +
            "");
    }

    IEnumerator ShotRateUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ShotRateUpScene", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.75f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ShotRateUpScene");
    }

    IEnumerator SpeedUp()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SpeedUpScene", LoadSceneMode.Additive);
        yield return new WaitForSeconds(1.75f);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("SpeedUpScene");
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
