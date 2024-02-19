using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkullScript : MonoBehaviour
{

    public GameObject Player;
    PlayerBehaviour _PlayerBehaviour;

    public GameObject RoomControllerObject;
    RoomController _RoomController;

    public GameObject SkullProjectiles;

    EnemyBehaviour _EnemyBehav;
    public Light2D Light2D;
    Color _blue = new Color(0.12549019607f, 1, 0.96862745098f);

    int _randomTime;
    float _timeElapsed;


    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        _PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();

        RoomControllerObject = GameObject.Find("RoomController");
        _RoomController = RoomControllerObject.GetComponent<RoomController>();

        _EnemyBehav = GetComponent<EnemyBehaviour>();

        _randomTime = Random.Range(2, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timeElapsed < _randomTime)
        {
            _timeElapsed += Time.deltaTime;
        }
        else
        {
            CreateProjectiles();
            _timeElapsed = 0;
            _randomTime = Random.Range(5, 8);
        }

    }

    void CreateProjectiles()
    {
        if (RoomController.GetWorldCoord(_PlayerBehaviour.playerRoomCoord) == _EnemyBehav.enemyWorldCoord)
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
        Light2D.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        Light2D.color = _blue;
    }
}
