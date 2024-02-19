using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    public GameObject RoomControllerObject;
    RoomController _RoomController;

    public GameObject EnemyHelperObject;
    EnemyHelper _EnemyHelper;

    public GameObject Player;
    public SpriteRenderer Rend;

    public Vector3 enemyWorldCoord;
    public Vector3 targetCoord;

    public float moveSpeed = 5;
    public int health;

    Color _white = Color.white;
    Color _red = Color.red;

    bool _coroutineStarted = false;
    bool _trackPlayer = false;

    void Start()
    {
        RoomControllerObject = GameObject.Find("RoomController");
        _RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyHelperObject = GameObject.Find("EnemyHelper");
        _EnemyHelper = EnemyHelperObject.GetComponent<EnemyHelper>();

        Player = GameObject.Find("Player");

        enemyWorldCoord = transform.parent.transform.position;
        targetCoord = enemyWorldCoord + _EnemyHelper.GetRandomVector();
    }

    private void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetCoord, moveSpeed * Time.deltaTime);
        if (transform.position == targetCoord && !_coroutineStarted)
        {
            _coroutineStarted = true;
            StartCoroutine(WaitAndMove());
        }
        if (targetCoord.x > enemyWorldCoord.x + 8.5 || targetCoord.x < enemyWorldCoord.x - 8.5 || targetCoord.y > enemyWorldCoord.y + 5.5 || targetCoord.y < enemyWorldCoord.y - 5.5)
        {
            targetCoord = enemyWorldCoord + _EnemyHelper.GetRandomVector();
        }

        if (targetCoord.x > transform.position.x)
        {
            FlipSpriteLeft();
        }
        else if ((targetCoord.x < transform.position.x))
        {
            FlipSpriteRight();
        }
        if(health <= 0)
        {
            _RoomController.totalEnemyCount -= 1;
            Destroy(gameObject);
        }
    }

    IEnumerator WaitAndMove()
    {
        if (_trackPlayer)
        {
            targetCoord = Player.transform.position;
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(1, 4));
            targetCoord = enemyWorldCoord + _EnemyHelper.GetRandomVector();
        }
        _coroutineStarted = false;
    }

    void FlipSpriteLeft()
    {
        Vector3 currentScaleX = transform.localScale;
        if(currentScaleX.x > 0)
        {
            return;
        }
        else
        {
            currentScaleX.x *= -1;
            transform.localScale = currentScaleX;
        }
    }

    void FlipSpriteRight()
    {
        Vector3 currentScaleX = transform.localScale;
        if (currentScaleX.x < 0)
        {
            return;
        }
        else
        {
            currentScaleX.x *= -1;
            transform.localScale = currentScaleX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyTrackingTrigger")
        {
            _trackPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrackingTrigger")
        {
            _trackPlayer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            health -= 1;
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        Rend.color = _red;
        yield return new WaitForSeconds(0.1f);
        Rend.color = _white;
    }
}
