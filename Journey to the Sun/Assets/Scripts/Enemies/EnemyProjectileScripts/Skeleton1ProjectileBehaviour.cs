using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1ProjectileBehaviour : MonoBehaviour
{
    SpriteRenderer _Sprite;

    public GameObject Player;
    PlayerBehaviour _PlayerBehaviour;

    Vector3 _direction;

    int _speed = 7;

    void Start()
    {
        Player = GameObject.Find("Player");
        _Sprite = GetComponentInChildren<SpriteRenderer>();
        _Sprite.sortingOrder = 1;
        _direction = (Player.transform.position - _Sprite.transform.position).normalized; 
    }

    void FixedUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
