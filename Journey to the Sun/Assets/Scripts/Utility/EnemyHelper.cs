using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelper : MonoBehaviour
{

    public EnemyPrefabManager EnemyPrefabManager;
    public RoomController RoomController;
    public Vector3 GetRandomVector()
    {
        var minX = -8;
        var maxX = 8;
        var minY = -5;
        var maxY = 5;
        var xCoord = Random.Range(minX, maxX);
        var yCoord = Random.Range(minY, maxY);
        var spawnVector = new Vector3(xCoord, yCoord, 0);
        return spawnVector;
    }


    public int GetRandomEnemy()
    {
        var randomIndex = Random.Range(0, EnemyPrefabManager.enemyPrefabList.Count);
        return randomIndex;
    }

    
}
