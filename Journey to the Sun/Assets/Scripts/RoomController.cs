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

    public Vector2 worldCoord;
    public Vector2 playerRoomCoord;

    Vector2 Up = new Vector2(0, 1);
    Vector2 Down = new Vector2(0, -1);
    Vector2 Left = new Vector2(-1, 0);
    Vector2 Right = new Vector2(1, 0);
    
    int maxRooms = 10;
    int createdRooms = 1;
    int collisions;
    bool startGen = false;
    int createdChildRooms;

    
    private Queue<GameObject> _toCreate = new Queue<GameObject>();
    List<Vector3> listOfCollisionCoords = new List<Vector3>();
    List<Vector3> directionList = new List<Vector3>();
    List<Vector3> createdRoomCoords = new List<Vector3>();


    public GameObject parentRoom;
    int childRooms;

    void StartGen()
    {
        Debug.Log("--------------NEW ITERATION--------------");
        parentRoom = _toCreate.Dequeue();
        childRooms = childRooms = parentRoom.GetComponent<Room>().childRooms;
        Debug.Log($"New parent room is {parentRoom}");
        Debug.Log($"Parent room has {childRooms} children");
        CreateDirectionList(childRooms);
    }
    

    void Start()
    {
        childRooms = parentRoom.GetComponent<Room>().childRooms;
        CreateDirectionList(childRooms);
    }

    private void Update()
    {
        if (createdRooms < maxRooms - 1 && Input.GetKeyDown(KeyCode.Space) && startGen == false)
        {
            StartGen();
            startGen = true;
        }

        if (createdRooms < maxRooms && startGen)
        {
            StartGen();
        }

        if (Input.GetKeyDown("d"))
        {
            foreach(Vector3 roomCoord in createdRoomCoords)
            {
                Destroy(GameObject.Find($"room{roomCoord}"));
                startGen = false;
                createdRooms = 0;
            }
        }
        
    }

    //Returns a list of random directions based on the target number of child rooms of the parent room
    void CreateDirectionList(int childRooms)
    {
        var directionListPreset = new List<Vector3>();
        Vector3[] directionArray = { Up, Down, Left, Right };

        directionList.Clear();
        Debug.Log("Called CreateDirectionList");

        directionListPreset.AddRange(directionArray);

        for (int i = 0; i < childRooms; i++)
        {
            int randomIndex = Random.Range(0, directionListPreset.Count);
            directionList.Add(directionListPreset[randomIndex]);
            directionListPreset.RemoveAt(randomIndex);
            Debug.Log("Added to direction list: " + directionList[i]);
        }

        CheckAndRemoveCollisions();
    }

    //Creates a room and names it according to its coordinates
    void CreateRoom(Vector3 newRoomCoord, Vector3 direction)
    {
        if(createdChildRooms < childRooms && directionList.Contains(direction))
        {
            var Room = Instantiate(room, GetWorldCoord(newRoomCoord), roomTransform.rotation);
            Room.name = $"room{newRoomCoord}";
            _toCreate.Enqueue(Room);
            Debug.Log($"Created room at {Room} and added to queue");
            createdRooms++;
            createdChildRooms++;
            createdRoomCoords.Add(newRoomCoord);
        }
    }
    
    void CheckAndRemoveCollisions()
    {
        collisions = 0;
        createdChildRooms = 0;
        listOfCollisionCoords.Clear();

        CheckCollision(Vector3.up);
        CheckCollision(Vector3.down);
        CheckCollision(Vector3.left);
        CheckCollision(Vector3.right);

        RemoveCollisions();
    }

    void CheckCollision(Vector3 direction)
    {
        if(GameObject.Find($"room{GetRoomCoord(parentRoom.transform.position) + direction}"))
        {
            Debug.Log($"Collision found, adding {GetRoomCoord(parentRoom.transform.position) + direction} to the list of collision coordinates");
            collisions++;
            listOfCollisionCoords.Add(direction);
        }
        else
        {
            Debug.Log("No collisions found");
            Debug.Log("Room will be created at " + (GetRoomCoord(parentRoom.transform.position) + direction));
            CreateRoom(GetRoomCoord(parentRoom.transform.position) + direction, direction);
        }
    }

    void RemoveCollisions()
    {
        Debug.Log("Removing all collisions");
        foreach (Vector3 coords in listOfCollisionCoords)
        {
            if (directionList.Contains(coords))
            {
                directionList.Remove(coords);
                
                Debug.Log($"New direction list is {directionList}");
            }
        }
        
    }

    
    //Converts room coordinate to world coordinate
    public static Vector3 GetWorldCoord(Vector3 roomCoord)
    {
        var worldCoord = new Vector3(roomCoord.x * 22, roomCoord.y * 16);
        return worldCoord;
    }

    //Converts world coordinate to room coordinate
    public static Vector3 GetRoomCoord(Vector3 worldCoord)
    { 
        var roomCoord = new Vector3(Mathf.FloorToInt((worldCoord.x + 11) / 22), Mathf.FloorToInt((worldCoord.y + 8) / 16));
        return roomCoord;
    }

    private void FixedUpdate()
    {
        playerRoomCoord = GetRoomCoord(playerTransform.position);
    }
}