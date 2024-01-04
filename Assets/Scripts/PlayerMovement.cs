using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //setting variables
    public float movementSpeed = 6f;

    Rigidbody2D body;
    SpriteRenderer rend;

    float horizontal;
    float vertical;

    bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        //Pulls components from Unity to allow them to be used in code
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Creates values of either 1 or -1 for horizontal and vertical inputs
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    //FixedUpdate is called once every fixed frame based on requested framerate (i.e 60fps, 120fps)
    private void FixedUpdate()
    {
        //horizontal * movementSpeed makes the movement speed either positive or negative by multiplying by the GetAxisRaw value
        body.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        if(horizontal > 0 && isFacingRight)
        {
            FlipPlayer();
        }
        if(horizontal < 0 && !isFacingRight)
        {
            FlipPlayer();
        }

        System.Console.WriteLine(body.velocity);
    }

    void FlipPlayer()
    {
        Vector3 currentScaleX = transform.localScale;
        currentScaleX.x *= -1;
        transform.localScale = currentScaleX;
        isFacingRight = !isFacingRight;
    }

}
