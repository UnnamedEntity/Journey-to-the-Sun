using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1ProjectileBehaviour : MonoBehaviour
{
    SpriteRenderer sprite;

    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    public GameObject Enemy;
    EnemyBehaviour EnemyBehaviour;

    public Vector2 playerPosition;
    public Vector2 enemyPosition;
    Vector2 direction;

    int speed = 7;

    void Start()
    {
        Player = GameObject.Find("Player");
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = 1;

        //transform.position = new Vector3(enemyPosition.x, enemyPosition.y, 0);
        playerPosition = new Vector2(Player.transform.position.x, Player.transform.position.y); 
        enemyPosition = new Vector2(transform.position.x, transform.position.y);
        direction = playerPosition - enemyPosition;
    }

    void FixedUpdate()
    {
        Debug.Log(direction.normalized);
        transform.position += new Vector3(direction.normalized.x, direction.normalized.y, 0) * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag != "Enemy" || collision.gameObject.tag != "Projectile")
        //{
        //    Destroy(gameObject);
        //}
    }
}
