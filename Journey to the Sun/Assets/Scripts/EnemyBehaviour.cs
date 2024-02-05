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

    public GameObject Player;


    public Vector3 enemyWorldCoord;
    public Vector3 targetCoord;

    public float moveSpeed = 5;

    private bool coroutineStarted = false;
    private bool trackPlayer = false;

    void Start()
    {
        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyHelperObject = GameObject.Find("EnemyHelper");
        EnemyHelper = EnemyHelperObject.GetComponent<EnemyHelper>();

        Player = GameObject.Find("Player");

        enemyWorldCoord = transform.parent.transform.position;
        targetCoord = enemyWorldCoord + EnemyHelper.GetRandomVector();
    }
    private void FixedUpdate()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetCoord, moveSpeed * Time.deltaTime);
        if(transform.position == targetCoord & !coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(Wait());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyTrackingTrigger")
        {
            trackPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrackingTrigger")
        {
            trackPlayer = false;
        }
    }

    IEnumerator Wait()
    {
        
        if (trackPlayer)
        {
            targetCoord = Player.transform.position;
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            targetCoord = enemyWorldCoord + EnemyHelper.GetRandomVector();
        }
        coroutineStarted = false;
    }

}
