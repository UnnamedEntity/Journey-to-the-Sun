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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            PlayerBehaviour.health -= 1;
            Debug.Log(PlayerBehaviour.health);
        }
    }
}
