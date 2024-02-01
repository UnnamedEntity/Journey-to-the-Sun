using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //setting variables
    public float movementSpeed = 8f;

    Rigidbody2D body;
    Animator anim;

    public ProjectileBehaviour ProjectilePrefab;
    public Transform PositionOffset;
    public OffsetBehaviour offsetBehaviour;

    float horizontal;
    float vertical;
    public float shootDelay = 0.75f;
    public float timeOffset;
    public Vector3 playerRoomCoord;

    public Vector3 leftOffset = new Vector3(-1f, 0, 0);
    public Vector3 rightOffset = new Vector3(1f, 0, 0);
    public Vector3 upOffset = new Vector3(0, 1f, 0);
    public Vector3 downOffset = new Vector3(0, -1f, 0);

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
    }

    //FixedUpdate is called once every fixed frame based on requested framerate (i.e 60fps, 120fps)
    private void FixedUpdate()
    {
        playerRoomCoord = RoomController.GetRoomCoord(transform.position);
        //Increases the value for time since the last shot until it meets the set shootDelay
        if (timeOffset < shootDelay)
        {
            timeOffset += Time.deltaTime;
        }

        //horizontal * movementSpeed makes the movement speed either positive or negative by multiplying by the GetAxisRaw value
        body.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        body.velocity = Vector2.ClampMagnitude(body.velocity, movementSpeed);


        //Sets value of "Speed" in the animation controller to the speed of the player
        anim.SetFloat("Speed", Mathf.Abs(GetSpeed(body.velocity)));

        //Checks the direction the player is facing and changes the direction of the sprite according to the direction the player is moving
        CheckFlipDirection();

        //Checks for play input for attacking
        PlayerAttackInput(KeyCode.LeftArrow, "Left", leftOffset);
        PlayerAttackInput(KeyCode.RightArrow, "Right", rightOffset);
        PlayerAttackInput(KeyCode.UpArrow, "Up", upOffset);
        PlayerAttackInput(KeyCode.DownArrow, "Down", downOffset);
    }

    void FlipPlayer()
    {
        Vector3 currentScaleX = transform.localScale;
        currentScaleX.x *= -1;
        transform.localScale = currentScaleX;
        isFacingRight = !isFacingRight;
    }

    void CheckFlipDirection()
    {
        if (horizontal > 0 && isFacingRight)
        {
            FlipPlayer();
        }
        if (horizontal < 0 && !isFacingRight)
        {
            FlipPlayer();
        }
    }

    static float GetSpeed(Vector2 v2)
    {
        v2.x = Mathf.Abs(v2.x);
        v2.y = Mathf.Abs(v2.y);
        return Mathf.Max(v2.x, v2.y);
    }

    void PlayerAttackInput(KeyCode keyCode, string direction, Vector3 projectileOffset)
    {
        if (Input.GetKey(keyCode))
        {
            offsetBehaviour.transform.position = transform.position + projectileOffset; //Sets position of the offset to the position of the player + the offset vector
            if (timeOffset >= shootDelay) //Allows code to be executed if the time since the last shot has gone past the delay
            {
                ProjectilePrefab.direction = direction;
                timeOffset = 0;
                Instantiate(ProjectilePrefab, PositionOffset.position, transform.rotation);
            }
        }
    }
}
