using System.Collections;
using System.Collections.Generic;
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
    
    int maxRooms = 11;
    int createdRooms = 1;

    
    private Queue<GameObject> _toCreate = new Queue<GameObject>();


    public GameObject parentRoom;

    void Start()
    {
        Debug.Log("Started generation procedure");
        Debug.Log("--------------------------STARTING FIRST ITERATION--------------------------");
        Debug.Log($"Parent room is {parentRoom}");
        toCreate_Children(parentRoom);
        while (createdRooms < maxRooms)
        {
            Debug.Log("Created rooms is less than max rooms");
            if (Input.GetKeyDown("c"))
            {
                Debug.Log("--------------------------REPEATING PROCEDURE--------------------------");
                Debug.Log($"New parent room is {parentRoom}");
                toCreate_Children(parentRoom);
            }
        }
   }

    //Called first on Start(), gathers the coordinates of the rooms to create next to the parent room
    void toCreate_Children(GameObject parentRoom)
    {
        Debug.Log("Called toCreate_Children");
        var directionList = CreateDirectionList(parentRoom.GetComponent<Room>().childRooms);
        Debug.Log("Beginning checking of existing rooms");
        for(int i = 0; i < directionList.Count; i++)
        {
            Debug.Log("Checking " + directionList[i]);
            Debug.Log($"Looking for: room{directionList[i]}");
            if (GameObject.Find($"room{directionList[i]}"))
            {
                Debug.Log("Conflict found, deleting from direction list");
                directionList.RemoveAt(i);
            }
            else
            {
                Debug.Log("No conflicts found");
            }
        }
        CreateChildren(parentRoom, directionList);
    }

    //Returns a list of random directions based on the target number of child rooms of the parent room
    List<Vector3> CreateDirectionList(int childRooms)
    {
        Debug.Log("Called CreateDirectionList");
        Vector3[] directionArray = { Up, Down, Left, Right };
        var directionListPreset = new List<Vector3>();
        directionListPreset.AddRange(directionArray);
        var directionList = new List<Vector3>();

        for (int i = 0; i < childRooms; i++)
        {
            int randomIndex = Random.Range(0, directionListPreset.Count);
            directionList.Add(directionListPreset[randomIndex]);
            directionListPreset.RemoveAt(randomIndex);
            Debug.Log("Added to direction list: " + directionList[i]);
        }
        return directionList;
    }

    //Called after toCreate_Children has completed, after directionList has been acquired for target coordinates. Creates the children and adds them to a queue to be set as the next parent room
    void CreateChildren(GameObject parentRoom, List<Vector3> directionList)
    {
        Debug.Log("Called CreateChildren");
        for(int i = 0; i < directionList.Count; i++)
        {
            Debug.Log("Room will be created at " + (parentRoom.transform.position + directionList[i]));
            CreateRoom(parentRoom.transform.position + directionList[i]);
            directionList.RemoveAt(i);
        }
    }

    //Creates a room and names it according to its coordinates
    void CreateRoom(Vector3 newRoomCoord)
    {
        Debug.Log($"Creating room with room coordinate: {newRoomCoord}");
        var Room = Instantiate(room, GetWorldCoord(newRoomCoord), roomTransform.rotation);
        Room.name = $"room{newRoomCoord}";
        _toCreate.Enqueue(Room);
        Debug.Log($"Created room at {Room} and added to queue");
        Debug.Log($"Next parent room is {_toCreate.Peek()}");
        parentRoom = _toCreate.Dequeue();
        createdRooms++;
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