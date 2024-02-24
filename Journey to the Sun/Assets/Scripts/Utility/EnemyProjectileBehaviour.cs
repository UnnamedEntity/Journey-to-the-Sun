using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileBehaviour : MonoBehaviour
{
    public GameObject Player;
    public PlayerBehaviour PlayerBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerBehaviour = Player.GetComponent<PlayerBehaviour>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Door")
        {
            Destroy(gameObject);
        }
        
    }
}
