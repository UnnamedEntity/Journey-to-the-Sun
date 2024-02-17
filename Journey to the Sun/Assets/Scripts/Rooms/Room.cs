using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Room : MonoBehaviour
{

    int[] possibleNoOfRooms = { 2, 3, 4 };
    int[] probabilityDistribution = { 10, 4, 1 };

    

    public GameObject RoomControllerObject;
    RoomController RoomController;

    public GameObject EnemyPrefabManagerObject;
    EnemyPrefabManager EnemyPrefabManager;

    EnemyHelper EnemyHelper;

    GameObject randomEnemy;

    int maxNumOfEnemies;
    int currentNumOfEnemies;
    int totalEnemyCount;

    List<int> weightedPossibleNoOfRooms = new List<int>();

    public int childRooms;


    
    public void Awake()
    {
        WeightArray();
        childRooms = GetRandomIndex(weightedPossibleNoOfRooms);
    }
    private void Start()
    {
        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyPrefabManagerObject = GameObject.Find("EnemyPrefabManager");
        EnemyPrefabManager = EnemyPrefabManagerObject.GetComponent<EnemyPrefabManager>();

        EnemyHelper = FindAnyObjectByType<EnemyHelper>();

        maxNumOfEnemies = Random.Range(1, 6);
        if(transform.name != "room(0.00, 0.00, 0.00)")
        {
            for (int i = 0; i < maxNumOfEnemies; i++)
            {
                randomEnemy = EnemyPrefabManager.enemyPrefabList[EnemyHelper.GetRandomEnemy()];
                Instantiate(randomEnemy, transform.position + EnemyHelper.GetRandomVector(), RoomController.roomTransform.rotation, this.transform);
                RoomController.totalEnemyCount++;
            }
        }
        
        
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
    
}
