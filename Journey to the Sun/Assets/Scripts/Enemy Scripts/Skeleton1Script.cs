using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1Script : MonoBehaviour
{
    public GameObject Player;

    public GameObject Skeleton1Projectile;
    int randomTime;
    float timeElapsed;

    Vector3 targetCoord;
    int projectileSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        randomTime = Random.Range(2, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timeElapsed < randomTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            Instantiate(Skeleton1Projectile, transform.position, transform.rotation);
            timeElapsed = 0;
            randomTime = Random.Range(5, 8);
        }
        
    }

}
