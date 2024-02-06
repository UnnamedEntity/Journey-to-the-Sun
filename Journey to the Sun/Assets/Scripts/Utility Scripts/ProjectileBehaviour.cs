using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public string direction;

    // Update is called once per frame
    void Update()
    {
        if(direction == "Left")
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (direction == "Right")
        {
            transform.position += transform.right * speed * Time.deltaTime;
            ///transform.position += (transform.right + transform.up) * speed * Time.deltaTime * 0.75f;
        }
        if (direction == "Up")
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        if (direction == "Down")
        {
            transform.position -= transform.up * speed * Time.deltaTime;
        }

    }
    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
