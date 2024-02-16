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
    int maxRooms = 12;
    int createdRooms = 1;
    public int childRooms;
    int presentChildRooms;
    int iterationAttempts = 0;
    bool removeDoorsCalled = false;
    public bool roomGenComplete = false;
    public int totalEnemyCount;
    bool displayedWinMessage = false;
    
    private Queue<GameObject> _toCreate = new Queue<GameObject>();

    //lists
    List<Vector3> listOfCollisionCoords = new List<Vector3>();
    List<Vector3> directionList = new List<Vector3>();
    public List<Vector3> listOfCreatedRooms = new List<Vector3>();
    List<Vector3> listOfNullAndReservedRooms = new List<Vector3>();
    public List<GameObject> clearedRooms = new List<GameObject>();

    void StartIteration()
    {
        iterationAttempts++;
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
        if (iterationAttempts > 1)
        {
            iterationAttempts = 0;
            SceneManager.RefreshGen();
        }

        if (createdRooms < maxRooms && roomGenComplete == false)
        {
            StartIteration();
        }
        else if(removeDoorsCalled == false)
        {
            RemoveDoors();
            removeDoorsCalled = true;
        }
        else
        {
            roomGenComplete = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.RefreshGen();
        }
        if(totalEnemyCount == 0 && !displayedWinMessage)
        {
            Debug.Log("YOU WIN!");
            displayedWinMessage = true;
        }
    }

    void CreateDirectionList(int childRooms)
    {
        iterationAttempts = 0;
        var directionListPreset = new List<Vector3>();
        Vector3[] directionArray = { Vector3.up, Vector3.down, Vector3.left, Vector3.right };

        directionList.Clear();

        directionListPreset.AddRange(directionArray);
        for (int i = 0; i < childRooms; i++)
        {
            int randomIndex = Random.Range(0, directionListPreset.Count);
            directionList.Add(directionListPreset[randomIndex]);
            directionListPreset.RemoveAt(randomIndex);
        }
        CheckAndRemoveCollisions();
    }

    void CreateRoom(Vector3 newRoomCoord, Vector3 direction)
    {
        var room = Instantiate(this.room, GetWorldCoord(newRoomCoord), roomTransform.rotation);
        room.name = $"room{newRoomCoord}";
        _toCreate.Enqueue(room);
        createdRooms++;
        presentChildRooms++;
        listOfCreatedRooms.Add(newRoomCoord);
    }
    
    void CheckAndRemoveCollisions()
    {
        presentChildRooms = 0;
        listOfCollisionCoords.Clear();

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
            presentChildRooms++;
            listOfCollisionCoords.Add(direction);
            return;
        }

        if (!directionList.Contains(direction))
        {
            listOfNullAndReservedRooms.Add(GetRoomCoord(parentRoom.transform.position) + direction);
        }

        if (listOfNullAndReservedRooms.Contains(GetRoomCoord(parentRoom.transform.position) + direction))
        {
            return;
        }

        if (presentChildRooms < childRooms)
        { 
            CreateRoom(GetRoomCoord(parentRoom.transform.position) + direction, direction);
        }
    }

    void RemoveCollisions()
    {
        foreach (Vector3 coords in listOfCollisionCoords)
        {
            if (directionList.Contains(coords))
            {
                directionList.Remove(coords);
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