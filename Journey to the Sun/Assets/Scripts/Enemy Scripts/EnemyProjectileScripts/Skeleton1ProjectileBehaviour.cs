using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1ProjectileBehaviour : MonoBehaviour
{
    SpriteRenderer sprite;

    public GameObject Player;
    PlayerBehaviour PlayerBehaviour;

    Vector3 direction;

    int speed = 7;

    void Start()
    {
        Player = GameObject.Find("Player");
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = 1;
        direction = (Player.transform.position - sprite.transform.position).normalized; 
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
