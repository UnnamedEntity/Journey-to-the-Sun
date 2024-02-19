using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1Script : MonoBehaviour
{
    public GameObject Player;
    PlayerBehaviour _PlayerBehaviour;

    public GameObject Skeleton1Projectile;

    EnemyBehaviour _EnemyBehav;

    int _randomTime;
    float _timeElapsed;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        _PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();

        _EnemyBehav = GetComponent<EnemyBehaviour>();

        _randomTime = Random.Range(2, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_timeElapsed < _randomTime)
        {
            _timeElapsed += Time.deltaTime;
        }
        else
        {
            CreateProjectile();
            _timeElapsed = 0;
            _randomTime = Random.Range(5, 8);
        }
        if (_EnemyBehav.health == 0)
        {
            Destroy(gameObject);
        }
    }

    void CreateProjectile()
    {
        if (RoomController.GetWorldCoord(_PlayerBehaviour.playerRoomCoord) == _EnemyBehav.enemyWorldCoord)
        {
            Instantiate(Skeleton1Projectile, transform.position, transform.rotation);
        }
    }
}
