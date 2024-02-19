using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject room;
    public Transform roomTransform;
    public Transform playerTransform;
    public SceneManager SceneManager;
    public GameObject parentRoom;
    

    public Vector3 worldCoord;
    //private variables
    int _maxRooms = 60;
    int _createdRooms = 1;
    public int childRooms;
    int _presentChildRooms;
    int _iterationAttempts = 0;
    bool _removeDoorsCalled = false;
    public bool roomGenComplete = false;
    public int totalEnemyCount;
    bool _displayedWinMessage = false;
    public float refreshTimer;
    
    private Queue<GameObject> _toCreate = new Queue<GameObject>();

    //lists
    List<Vector3> _listOfCollisionCoords = new List<Vector3>();
    List<Vector3> _directionList = new List<Vector3>();
    public List<Vector3> listOfCreatedRooms = new List<Vector3>();
    List<Vector3> _listOfNullAndReservedRooms = new List<Vector3>();
    public List<GameObject> clearedRooms = new List<GameObject>();

    void StartIteration()
    {
        _iterationAttempts++;
        parentRoom = _toCreate.Dequeue();
        childRooms = parentRoom.GetComponent<Room>().childRooms;
        CreateDirectionList(childRooms);
    }

    void Start()
    {
        parentRoom = GameObject.Find("room(0.00, 0.00, 0.00)");
        listOfCreatedRooms.Add(GetRoomCoord(parentRoom.transform.position));
        childRooms = parentRoom.GetComponent<Room>().childRooms;
        CreateDirectionList(childRooms);
    }

    private void Update()
    {
        if (_iterationAttempts > 1)
        {
            _iterationAttempts = 0;
            SceneManager.RefreshGen();
        }

        if (_createdRooms < _maxRooms && roomGenComplete == false)
        {
            StartIteration();
        }
        else if(_removeDoorsCalled == false)
        {
            RemoveDoors();
            _removeDoorsCalled = true;
        }
        else
        {
            roomGenComplete = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            refreshTimer += Time.deltaTime;
            if(refreshTimer > 1.2)
            {
                SceneManager.RefreshGen();
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            refreshTimer = 0;
        }
        if(totalEnemyCount == 0 && !_displayedWinMessage && roomGenComplete)
        { 
            Debug.Log("YOU WIN!");
            _displayedWinMessage = true;
        }
    }

    void CreateDirectionList(int childRooms)
    {
        _iterationAttempts = 0;
        var directionListPreset = new List<Vector3>();
        Vector3[] directionArray = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        _directionList.Clear();

        directionListPreset.AddRange(directionArray);
        for (int i = 0; i < childRooms; i++)
        {
            int randomIndex = Random.Range(0, directionListPreset.Count);
            _directionList.Add(directionListPreset[randomIndex]);
            directionListPreset.RemoveAt(randomIndex);
        }
        CheckAndRemoveCollisions();
    }

    void CreateRoom(Vector3 newRoomCoord, Vector3 direction)
    {
        var room = Instantiate(this.room, GetWorldCoord(newRoomCoord), roomTransform.rotation);
        room.name = $"room{newRoomCoord}";
        _toCreate.Enqueue(room);
        _createdRooms++;
        _presentChildRooms++;
        listOfCreatedRooms.Add(newRoomCoord);
    }
    
    void CheckAndRemoveCollisions()
    {
        _presentChildRooms = 0;
        _listOfCollisionCoords.Clear();

        CheckCollision(Vector3.up);
        CheckCollision(Vector3.down);
        CheckCollision(Vector3.left);
        CheckCollision(Vector3.right);

        RemoveCollisions();
    }

    void CheckCollision(Vector3 direction)
    {
        if (GameObject.Find($"room{GetRoomCoord(parentRoom.transform.position) + direction}"))
        {
            _presentChildRooms++;
            _listOfCollisionCoords.Add(direction);
            return;
        }

        if (!_directionList.Contains(direction))
        {
            _listOfNullAndReservedRooms.Add(GetRoomCoord(parentRoom.transform.position) + direction);
        }

        if (_listOfNullAndReservedRooms.Contains(GetRoomCoord(parentRoom.transform.position) + direction))
        {
            return;
        }

        if (_presentChildRooms < childRooms)
        { 
            CreateRoom(GetRoomCoord(parentRoom.transform.position) + direction, direction);
        }
    }

    void RemoveCollisions()
    {
        foreach (Vector3 coords in _listOfCollisionCoords)
        {
            if (_directionList.Contains(coords))
            {
                _directionList.Remove(coords);
            }
        } 
    }

    
    public static Vector3 GetWorldCoord(Vector3 roomCoord)
    {
        var worldCoord = new Vector3(roomCoord.x * 22, roomCoord.y * 16);
        return worldCoord;
    }

    public static Vector3 GetRoomCoord(Vector3 worldCoord)
    { 
        var roomCoord = new Vector3(Mathf.FloorToInt((worldCoord.x + 11) / 22), Mathf.FloorToInt((worldCoord.y + 8) / 16));
        return roomCoord;
    }

    void RemoveDoors()
    {
        for(int i = 0; i < listOfCreatedRooms.Count; i++)
        {
            GameObject room = GameObject.Find($"room{listOfCreatedRooms[i]}");
            if (!listOfCreatedRooms.Contains(listOfCreatedRooms[i] + Vector3.up))
            {
                GameObject topWall = room.transform.GetChild(6).gameObject;
                topWall.GetComponent<Renderer>().enabled = true;
                topWall.GetComponent<Collider2D>().enabled = true;
            }
            if (!listOfCreatedRooms.Contains(listOfCreatedRooms[i] + Vector3.down))
            {
                GameObject bottomWall = room.transform.GetChild(7).gameObject;
                bottomWall.GetComponent<Renderer>().enabled = true;
                bottomWall.GetComponent<Collider2D>().enabled = true;
            }
            if (!listOfCreatedRooms.Contains(listOfCreatedRooms[i] + Vector3.left))
            {
                GameObject leftWall = room.transform.GetChild(4).gameObject;
                leftWall.GetComponent<Renderer>().enabled = true;
                leftWall.GetComponent<Collider2D>().enabled = true;
            }
            if (!listOfCreatedRooms.Contains(listOfCreatedRooms[i] + Vector3.right))
            {
                GameObject rightWall = room.transform.GetChild(5).gameObject;
                rightWall.GetComponent<Renderer>().enabled = true;
                rightWall.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}