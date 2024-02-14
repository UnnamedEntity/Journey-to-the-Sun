using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1Script : MonoBehaviour
{
    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    public GameObject RoomControllerObject;
    RoomController RoomController;

    public GameObject Skeleton1Projectile;

    EnemyBehaviour EnemyBehav;

    int randomTime;
    float timeElapsed;

    Vector3 targetCoord;
    int projectileSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();

        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyBehav = GetComponent<EnemyBehaviour>();

        randomTime = Random.Range(2, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeElapsed < randomTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            CreateProjectile();
            timeElapsed = 0;
            randomTime = Random.Range(5, 8);
        }
        
    }

    void CreateProjectile()
    {
        if (RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord) == EnemyBehav.enemyWorldCoord)
        {
            Instantiate(Skeleton1Projectile, transform.position, transform.rotation);
        }
    }

}
