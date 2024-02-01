using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Skeleton1;
    public GameObject Skeleton2;
    public List<GameObject> enemyPrefabList = new List<GameObject>();
    void Start()
    {
        enemyPrefabList.Add(Skeleton1);
        enemyPrefabList.Add(Skeleton2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
