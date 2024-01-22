using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject room;
    public Transform roomTransform;
    public Vector2 worldCoord;
    
    public Vector2 currentRoomCoord = new Vector2(0, 0);
    public Vector2 playerRoomCoord;
    public Vector2 newRoomCoord = new Vector2(0, 0);
    public Vector2 Up = new Vector2(0, 1);
    public Vector2 Down = new Vector2(0, -1);
    public Vector2 Left = new Vector2(-1, 0);
    public Vector2 Right = new Vector2(1, 0);
    int maxRooms;
    int createdRooms;

    public Transform playerTransform;
    
    

    void Start()
    {
        CreateRoom(new Vector2(0, 0));
        CreateRoom(currentRoomCoord + Right);
        CreateRoom(currentRoomCoord + Up);
        CreateRoom(currentRoomCoord + Right);
        CreateRoom(currentRoomCoord + Down);
        CreateRoom(currentRoomCoord + Down);
        CreateRoom(currentRoomCoord + Left);
        while (createdRooms < maxRooms)
        {
        }
   }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerRoomCoord = GetRoomCoord(playerTransform.position);
    }
    void CreateRoom(Vector2 newRoomCoord)
    {
        var worldCoord = GetWorldCoord(newRoomCoord);
        var Room = Instantiate(room, worldCoord, roomTransform.rotation);
        Room.name = $"room{newRoomCoord}";
        currentRoomCoord = newRoomCoord;
        createdRooms++;
    }

    //Converts room coordinate to world coordinate
    public static Vector2 GetWorldCoord(Vector2 roomCoord)
    {
        var worldCoord = new Vector2(roomCoord.x * 22, roomCoord.y * 16);
        return worldCoord;
    }

    //Converts world coordinate to room coordinate
    public static Vector2 GetRoomCoord(Vector2 worldCoord)
    { 
        var roomCoord = new Vector2(Mathf.FloorToInt((worldCoord.x + 11) / 22), Mathf.FloorToInt((worldCoord.y + 8) / 16));
        return roomCoord;
    }
}





//roomCoord = new Vector3(0, 0, 0);
//worldCoord = GetWorldCoord(roomCoord);
////Creates a new room using the room prefab, the world coordinates set through the method, and the rotation value of the room
//Instantiate(room, worldCoord, roomPosition.rotation);

//roomCoord = new Vector3(1, 0, 0);
//worldCoord = GetWorldCoord(roomCoord);
//Instantiate(room, worldCoord, roomPosition.rotation);

//roomCoord = new Vector3(0, 1, 0);
//worldCoord = GetWorldCoord(roomCoord);
//Instantiate(room, worldCoord, roomPosition.rotation);
