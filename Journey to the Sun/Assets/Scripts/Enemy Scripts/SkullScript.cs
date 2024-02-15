using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkullScript : MonoBehaviour
{

    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    public GameObject RoomControllerObject;
    RoomController RoomController;

    public GameObject SkullProjectiles;

    EnemyBehaviour EnemyBehav;
    public Light2D light2D;
    Color blue = new Color(0.12549019607f, 1, 0.96862745098f);

    int randomTime;
    float timeElapsed;


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
            Instantiate(SkullProjectiles, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            StartCoroutine(SkullFlashRed());
        }
    }

    IEnumerator SkullFlashRed()
    {
        light2D.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        light2D.color = blue;
    }
}
