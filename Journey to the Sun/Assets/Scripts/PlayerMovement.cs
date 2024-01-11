using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //setting variables
    public float movementSpeed = 8f;

    Rigidbody2D body;
    Animator anim;

    float horizontal;
    float vertical;
    public float shootDelay = 0.75f;
    public float timeOffset;

    public ProjectileBehaviour ProjectilePrefab;
    public Transform PositionOffset;
    public OffsetBehaviour offsetBehaviour;
    
  
    bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        //Pulls components from Unity to allow them to be used in code
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Creates values of either 1 or -1 for horizontal and vertical inputs
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(timeOffset < shootDelay)
        {
            timeOffset += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            offsetBehaviour.transform.position = transform.position + new Vector3(-1f, 0, 0);
            if (timeOffset >= shootDelay)
            {
                ProjectilePrefab.direction = "Left";
                timeOffset = 0;
                Instantiate(ProjectilePrefab, PositionOffset.position, transform.rotation);
            } 
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            offsetBehaviour.transform.position = transform.position + new Vector3(1f, 0, 0);
            if (timeOffset >= shootDelay)
            {
                ProjectilePrefab.direction = "Right";
                timeOffset = 0;
                Instantiate(ProjectilePrefab, PositionOffset.position, transform.rotation);
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            offsetBehaviour.transform.position = transform.position + new Vector3(0, 1f, 0);
            if (timeOffset >= shootDelay)
            {
                ProjectilePrefab.direction = "Up";
                timeOffset = 0;
                Instantiate(ProjectilePrefab, PositionOffset.position, transform.rotation);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            offsetBehaviour.transform.position = transform.position + new Vector3(0, -1f, 0);
            if (timeOffset >= shootDelay)
            {
                ProjectilePrefab.direction = "Down";
                timeOffset = 0;
                Instantiate(ProjectilePrefab, PositionOffset.position, transform.rotation);
            }
        }
    }

    //FixedUpdate is called once every fixed frame based on requested framerate (i.e 60fps, 120fps)
    private void FixedUpdate()
    {
        //horizontal * movementSpeed makes the movement speed either positive or negative by multiplying by the GetAxisRaw value
        body.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        body.velocity = Vector2.ClampMagnitude(body.velocity, movementSpeed);

        //Sets value of "Speed" in the animation controller to the speed of the player
        anim.SetFloat("Speed", Mathf.Abs(GetSpeed(body.velocity)));

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

    static float GetSpeed(Vector2 v2)
    {
        v2.x = Mathf.Abs(v2.x);
        v2.y = Mathf.Abs(v2.y);
        return Mathf.Max(v2.x, v2.y);
    }

}
