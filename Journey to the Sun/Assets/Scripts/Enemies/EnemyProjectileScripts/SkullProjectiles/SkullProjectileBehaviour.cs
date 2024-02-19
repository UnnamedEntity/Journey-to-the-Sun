using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectileBehaviour : MonoBehaviour
{
    public GameObject Player;
    public GameObject SkullScriptObject;

    public Vector3 direction;

    GameObject _UpProjectile;
    SpriteRenderer _UpSprite;
    GameObject _DownProjectile;
    SpriteRenderer _DownSprite;
    GameObject _LeftProjectile;
    SpriteRenderer _LeftSprite;
    GameObject _RightProjectile;
    SpriteRenderer _RightSprite;

    public int speed = 7;

    void Start()
    {
        Player = GameObject.Find("Player");

        _UpProjectile = transform.Find("UpProjectile").gameObject;
        _DownProjectile = transform.Find("DownProjectile").gameObject;
        _LeftProjectile = transform.Find("LeftProjectile").gameObject;
        _RightProjectile = transform.Find("RightProjectile").gameObject;

        _UpSprite = _UpProjectile.GetComponentInChildren<SpriteRenderer>();
        _DownSprite = _DownProjectile.GetComponentInChildren<SpriteRenderer>();
        _LeftSprite = _LeftProjectile.GetComponentInChildren<SpriteRenderer>();
        _RightSprite = _RightProjectile.GetComponentInChildren<SpriteRenderer>();

        _UpSprite.sortingOrder = 1;
        _DownSprite.sortingOrder = 1;
        _LeftSprite.sortingOrder = 1;
        _RightSprite.sortingOrder = 1;
    }
}
