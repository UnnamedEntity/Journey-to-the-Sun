using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullProjectileBehaviour : MonoBehaviour
{
    public GameObject Player;
    public GameObject SkullScriptObject;

    SpriteRenderer sprite;
    SkullScript SkullScript;

    Vector3 direction;

    int speed = 7;
    private void Awake()
    {
        SkullScriptObject = GameObject.Find("SkullScriptObject");
        SkullScript = SkullScriptObject.GetComponent<SkullScript>();

        direction = SkullScript.direction;
    }

    void Start()
    {
        Player = GameObject.Find("Player");
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = 1;
        direction = (direction - sprite.transform.position).normalized;
    }

    void FixedUpdate()
    {
        Debug.Log(direction);
        transform.position += direction * speed * Time.deltaTime;
    }
}
