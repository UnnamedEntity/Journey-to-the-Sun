using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1ProjectileBehaviour : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag != "Enemy" || collision.gameObject.tag != "Projectile")
        //{
        //    Destroy(gameObject);
        //}
    }
}
