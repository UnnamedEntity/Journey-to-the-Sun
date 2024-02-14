using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullScript : MonoBehaviour
{
    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    public GameObject RoomControllerObject;
    RoomController RoomController;

    public GameObject SkullProjectile;

    EnemyBehaviour EnemyBehav;

    int randomTime;
    float timeElapsed;

    Vector3 targetCoord;
    int projectileSpeed = 5;

    public Vector3 direction;

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
        if (timeElapsed < randomTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            CreateProjectiles();
            timeElapsed = 0;
            randomTime = Random.Range(5, 8);
        }

    }

    void CreateProjectiles()
    {
        if (RoomController.GetWorldCoord(PlayerBehaviour.playerRoomCoord) == EnemyBehav.enemyWorldCoord)
        {
            direction = Vector3.up;
            Instantiate(SkullProjectile, transform.position, transform.rotation);
            direction = Vector3.down;
            Instantiate(SkullProjectile, transform.position, transform.rotation);
            direction = Vector3.left;
            Instantiate(SkullProjectile, transform.position, transform.rotation);
            direction = Vector3.right;
            Instantiate(SkullProjectile, transform.position, transform.rotation);
        }
    }
}
