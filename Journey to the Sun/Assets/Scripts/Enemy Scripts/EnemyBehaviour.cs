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

    public Vector3 enemyWorldCoord;
    public Vector3 targetCoord;

    public Rigidbody2D rb;

    public float moveSpeed = 5;

    private bool coroutineStarted = false;
    private bool trackPlayer = false;

    void Start()
    {
        enemyWorldCoord = transform.parent.position;
        RoomControllerObject = GameObject.Find("RoomController");
        RoomController = RoomControllerObject.GetComponent<RoomController>();

        EnemyHelperObject = GameObject.Find("EnemyHelper");
        EnemyHelper = EnemyHelperObject.GetComponent<EnemyHelper>();

        Player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        
        Debug.Log("Parent position of enemy is " + enemyWorldCoord);
        targetCoord = enemyWorldCoord + EnemyHelper.GetRandomVector();
    }

    private void FixedUpdate()
    {

        //transform.position = Vector3.MoveTowards(transform.position, targetCoord, moveSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + targetCoord.normalized * moveSpeed * Time.deltaTime);

        if (transform.position == targetCoord & !coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(Wait());
        }

        if (targetCoord.x > transform.position.x)
        {
            FlipSpriteLeft();
        }
        else if((targetCoord.x < transform.position.x))
        {
            FlipSpriteRight();
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
        if(collision.gameObject.tag == "Enemy")
        {
            collision.collider.enabled = false;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.collider.enabled = true;
        }
    }
}
