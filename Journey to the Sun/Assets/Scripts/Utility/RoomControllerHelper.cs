using UnityEngine;

public static class RoomControllerHelper
{
    //Converts world coordinate to room coordinate
    public static Vector2 GetRoomCoord(Vector2 worldCoord)
    {
        var roomCoord = new Vector2(Mathf.FloorToInt((worldCoord.x + 11) / 22), Mathf.FloorToInt((worldCoord.y + 8) / 16));
        return roomCoord;
    }

    //Converts room coordinate to world coordinate
    public static Vector2 GetWorldCoord(Vector2 roomCoord)
    {
        var worldCoord = new Vector2(roomCoord.x * 22, roomCoord.y * 16);
        return worldCoord;
    }
}