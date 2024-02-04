using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    public GameObject RoomControllerObject;
    RoomController RoomController;

    public GameObject EnemyHelperObject;
    EnemyHelper EnemyHelper;


    public Vector3 enemyWorldCoord;
    public Vector3 targetCoord;

    public float moveSpeed = 5;

    void Start()
    {
        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyHelperObject = GameObject.Find("EnemyHelper");
        EnemyHelper = EnemyHelperObject.GetComponent<EnemyHelper>();

        enemyWorldCoord = this.transform.parent.transform.position;
        targetCoord = enemyWorldCoord + EnemyHelper.GetRandomVector();
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetCoord, moveSpeed * Time.deltaTime);
        if(transform.position == targetCoord)
        {
            targetCoord = enemyWorldCoord + EnemyHelper.GetRandomVector();
        }
    }
}
