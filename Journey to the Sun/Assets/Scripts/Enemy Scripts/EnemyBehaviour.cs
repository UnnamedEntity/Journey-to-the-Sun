using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    public GameObject RoomControllerObject;
    RoomController RoomController;

    public GameObject EnemyHelperObject;
    EnemyHelper EnemyHelper;

    public GameObject Player;
    public SpriteRenderer rend;

    public Vector3 enemyWorldCoord;
    public Vector3 targetCoord;

    public float moveSpeed = 5;

    public int health;

    Color white = Color.white;
    Color red = Color.red;

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
        if (transform.position == targetCoord & !coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(Wait());
        }

        if (targetCoord.x > transform.position.x)
        {
            FlipSpriteLeft();
        }
        else if ((targetCoord.x < transform.position.x))
        {
            FlipSpriteRight();
        }
        if(health == 0)
        {
            Destroy(gameObject);
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
        rend.color = red;
        yield return new WaitForSeconds(0.1f);
        rend.color = white;
    }
}
