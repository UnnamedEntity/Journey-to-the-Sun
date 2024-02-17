using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectileBehaviour : MonoBehaviour
{
    public GameObject Player;
    public GameObject SkullScriptObject;

    
    SkullScript SkullScript;

    public Vector3 direction;

    GameObject UpProjectile;
    SpriteRenderer UpSprite;
    GameObject DownProjectile;
    SpriteRenderer DownSprite;
    GameObject LeftProjectile;
    SpriteRenderer LeftSprite;
    GameObject RightProjectile;
    SpriteRenderer RightSprite;

    public int speed = 7;

    void Start()
    {
        Player = GameObject.Find("Player");

        UpProjectile = transform.Find("UpProjectile").gameObject;
        DownProjectile = transform.Find("DownProjectile").gameObject;
        LeftProjectile = transform.Find("LeftProjectile").gameObject;
        RightProjectile = transform.Find("RightProjectile").gameObject;

        UpSprite = UpProjectile.GetComponentInChildren<SpriteRenderer>();
        DownSprite = DownProjectile.GetComponentInChildren<SpriteRenderer>();
        LeftSprite = LeftProjectile.GetComponentInChildren<SpriteRenderer>();
        RightSprite = RightProjectile.GetComponentInChildren<SpriteRenderer>();

        UpSprite.sortingOrder = 1;
        DownSprite.sortingOrder = 1;
        LeftSprite.sortingOrder = 1;
        RightSprite.sortingOrder = 1;
    }
}
