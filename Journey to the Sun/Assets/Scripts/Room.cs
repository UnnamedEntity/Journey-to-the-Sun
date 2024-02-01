using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    int[] possibleNoOfRooms = { 2, 3, 4 };
    int[] probabilityDistribution = { 10, 4, 1 };

    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    public GameObject RoomCont;
    RoomController RoomController;

    GameObject randomEnemy;

    int numOfEnemies;

    public GameObject[] enemyPrefabs;
    List<int> weightedPossibleNoOfRooms = new List<int>();

    public int childRooms;
    
    public void Awake()
    {
        
        WeightArray();
        childRooms = GetRandomIndex(weightedPossibleNoOfRooms);
    }
    private void Start()
    {
        enemyPrefabs = new GameObject[5];
        enemyPrefabs = Resources.LoadAll("Assets/Prefabs/") as GameObject[];
        Player = GameObject.Find("Player");
        RoomCont = GameObject.Find("RoomController");
        RoomController = RoomCont.GetComponent<RoomController>();
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
        numOfEnemies = Random.Range(1, 6);
        for (int i = 0; i < numOfEnemies; i++)
        {
            randomEnemy = enemyPrefabs[GetRandomEnemy()];
            Instantiate(randomEnemy, GetRandomSpawnVector(), RoomController.roomTransform.rotation);
        }
    }
    private void Update()
    {
        //    foreach (GameObject enemy in prefabList)
        //    {
        //        Debug.Log("Enemy " + enemy);
        //    }
        //
    }

    int GetRandomIndex(List<int> weightedPossibleNoOfRooms)
    {
        
        int childRooms;
        int randomIndex = Random.Range(0, weightedPossibleNoOfRooms.Count);
        childRooms = weightedPossibleNoOfRooms[randomIndex];
        return childRooms;
    }
    void WeightArray()
    {
        for (int i = 0; i < possibleNoOfRooms.Length; i++)
        {
            switch (i)
            {
                //Depending on the index value, adds the index of the possible number of rooms to the list the corresponding to amount of times as stated by the probability distribution
                case 0:
                    for (int x = 0; x < probabilityDistribution[0]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
                case 1:
                    for (int x = 0; x < probabilityDistribution[1]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
                case 2:
                    for (int x = 0; x < probabilityDistribution[2]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
                case 3:
                    for (int x = 0; x < probabilityDistribution[3]; x++)
                    {
                        weightedPossibleNoOfRooms.Add(possibleNoOfRooms[i]);
                    }
                    break;
            }
        }
    }
    Vector3 GetRandomSpawnVector()
    {
        var spawnVector = new Vector3(0, 0, 0);
        var minx = (RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord).x - 4) - ((RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord).x - 4) / 2);
        Debug.Log(minx);
        return spawnVector;
    }
    
    int GetRandomEnemy()
    {
        var randomIndex = Random.Range(0, enemyPrefabs.Length);
        return randomIndex;
    }
}
