using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabManager : MonoBehaviour
{
    public GameObject Skeleton1;
    public GameObject Skeleton2;
    public GameObject Skull;
    public GameObject Vampire;

    public List<GameObject> enemyPrefabList = new List<GameObject>();
    void Start()
    {
        enemyPrefabList.Add(Skeleton1);
        enemyPrefabList.Add(Skeleton2);
        enemyPrefabList.Add(Skull);
        enemyPrefabList.Add(Vampire);
    }
}
