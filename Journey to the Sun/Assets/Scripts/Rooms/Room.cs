using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Room : MonoBehaviour
{

    int[] _possibleNoOfRooms = { 2, 3, 4 };
    int[] probabilityDistribution = { 10, 4, 1 };

    

    public GameObject RoomControllerObject;
    RoomController _RoomController;

    public GameObject EnemyPrefabManagerObject;
    EnemyPrefabManager _EnemyPrefabManager;

    EnemyHelper _EnemyHelper;

    GameObject _randomEnemy;

    int _maxNumOfEnemies;

    List<int> _weightedPossibleNoOfRooms = new List<int>();

    public int childRooms;


    
    public void Awake()
    {
        WeightArray();
        childRooms = GetRandomIndex(_weightedPossibleNoOfRooms);
    }
    private void Start()
    {
        RoomControllerObject = GameObject.Find("RoomController");
        _RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyPrefabManagerObject = GameObject.Find("EnemyPrefabManager");
        _EnemyPrefabManager = EnemyPrefabManagerObject.GetComponent<EnemyPrefabManager>();

        _EnemyHelper = FindAnyObjectByType<EnemyHelper>();

        _maxNumOfEnemies = Random.Range(1, 6);
        if(transform.name != "room(0.00, 0.00, 0.00)")
        {
            for (int i = 0; i < _maxNumOfEnemies; i++)
            {
                _randomEnemy = _EnemyPrefabManager.enemyPrefabList[_EnemyHelper.GetRandomEnemy()];
                Instantiate(_randomEnemy, transform.position + _EnemyHelper.GetRandomVector(), _RoomController.roomTransform.rotation, this.transform);
                _RoomController.totalEnemyCount++;
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
        for (int i = 0; i < _possibleNoOfRooms.Length; i++)
        {
            switch (i)
            {
                //Depending on the index value, adds the index of the possible number of rooms to the list the corresponding to amount of times as stated by the probability distribution
                case 0:
                    for (int x = 0; x < probabilityDistribution[0]; x++)
                    {
                        _weightedPossibleNoOfRooms.Add(_possibleNoOfRooms[i]);
                    }
                    break;
                case 1:
                    for (int x = 0; x < probabilityDistribution[1]; x++)
                    {
                        _weightedPossibleNoOfRooms.Add(_possibleNoOfRooms[i]);
                    }
                    break;
                case 2:
                    for (int x = 0; x < probabilityDistribution[2]; x++)
                    {
                        _weightedPossibleNoOfRooms.Add(_possibleNoOfRooms[i]);
                    }
                    break;
                case 3:
                    for (int x = 0; x < probabilityDistribution[3]; x++)
                    {
                        _weightedPossibleNoOfRooms.Add(_possibleNoOfRooms[i]);
                    }
                    break;
            }
        }
    }
    
}
