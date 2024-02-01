using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform PlayerPosition;
    NavMeshAgent agent;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        PlayerPosition = player.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        agent.SetDestination(PlayerPosition.position + new Vector3(1,0));
        if (Input.GetKeyDown("e"))
        {
            agent.speed = 0;
        }
        if (Input.GetKeyDown("r"))
        {
            agent.speed = 5;
        }
    }
}
