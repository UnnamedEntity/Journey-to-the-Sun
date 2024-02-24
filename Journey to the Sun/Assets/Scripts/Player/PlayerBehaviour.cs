using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    //Modifiable properties
    public float movementSpeed = 8f;
    private int _health = 6;
    public int Health
    {
        get { return _health; }
        set
        {
            if(_health < 0)
            {
                _health = 0;
            }
        }
    }

    public float attackDamage = 1f;
    public float shotRate = 0.75f;
    bool _invinsible = false;

    Rigidbody2D _Body;
    Animator _Anim;

    public ProjectileBehaviour ProjectileBehaviour;
    public GameObject PositionOffset;
    public SpriteRenderer Rend;
    public SceneManager SceneManager;

    float _horizontal;
    float _vertical;
    
    public float timeOffset;
    public Vector3 playerRoomCoord;

    public Vector3 leftOffset = new Vector3(-1f, 0, 0);
    public Vector3 rightOffset = new Vector3(1f, 0, 0);
    public Vector3 upOffset = new Vector3(0, 1f, 0);
    public Vector3 downOffset = new Vector3(0, -1f, 0);

    bool _isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        //Pulls components from Unity to allow them to be used in code
        _Body = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Creates values of either 1 or -1 for horizontal and vertical inputs
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    //FixedUpdate is called once every fixed frame based on requested framerate (i.e 60fps, 120fps)
    private void FixedUpdate()
    {
        playerRoomCoord = RoomController.GetRoomCoord(transform.position);
        //Increases the value for time since the last shot until it meets the set shootDelay
        if (timeOffset < shotRate)
        {
            timeOffset += Time.deltaTime;
        }

        //horizontal * movementSpeed makes the movement speed either positive or negative by multiplying by the GetAxisRaw value
        _Body.velocity = new Vector2(_horizontal * movementSpeed, _vertical * movementSpeed);
        _Body.velocity = Vector2.ClampMagnitude(_Body.velocity, movementSpeed);


        //Sets value of "Speed" in the animation controller to the speed of the player
        _Anim.SetFloat("Speed", Mathf.Abs(GetSpeed(_Body.velocity)));

        //Checks the direction the player is facing and changes the direction of the sprite according to the direction the player is moving
        CheckFlipDirection();

        //Checks for play input for attacking
        PlayerAttackInput(KeyCode.LeftArrow, "Left", leftOffset);
        PlayerAttackInput(KeyCode.RightArrow, "Right", rightOffset);
        PlayerAttackInput(KeyCode.UpArrow, "Up", upOffset);
        PlayerAttackInput(KeyCode.DownArrow, "Down", downOffset);

        if(_health == 0)
        {
            Destroy(gameObject);
            Debug.Log("YOU DIED");
        }
    }

    void FlipPlayer()
    {
        Vector3 currentScaleX = transform.localScale;
        currentScaleX.x *= -1;
        transform.localScale = currentScaleX;
        _isFacingRight = !_isFacingRight;
    }

    void CheckFlipDirection()
    {
        if (_horizontal > 0 && _isFacingRight)
        {
            FlipPlayer();
        }
        if (_horizontal < 0 && !_isFacingRight)
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
            PositionOffset.transform.position = transform.position + projectileOffset; //Sets position of the offset to the position of the player + the offset vector
            if (timeOffset >= shotRate) //Allows code to be executed if the time since the last shot has gone past the delay
            {
                ProjectileBehaviour.direction = direction;
                timeOffset = 0;
                Instantiate(ProjectileBehaviour, PositionOffset.transform.position, transform.rotation);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Enemy") && !_invinsible)
        {
            StartCoroutine(FlashRed());
            StartCoroutine(PlayerHurt());
        }
    }
    IEnumerator FlashRed()
    {
        Rend.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Rend.color = Color.white;
    }
    IEnumerator PlayerHurt()
    {
        _health -= 1;
        _invinsible = true;
        yield return new WaitForSeconds(1);
        _invinsible = false;
    }

}
