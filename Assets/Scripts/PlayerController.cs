using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private HealthBarForPlayerController healthBarController;
    private GameManager gameManager;
    private PlayerThrowsBrickController playerThrowsBrickController;

    [SerializeField] LayerMask groundMask;
    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    [Range(0, 30)] public float playerJumpVelocity = 17f;

    [Range(100, 1000)] public float moveSpeed = 450f;
    [Range(1, 10)] public float jumpForce = 5f;

    public bool moveLeft;
    public bool dontMove;


    //private bool walk, walkLeft, walkRight, jump, throwBrick;
    private float healthBarStatus = 1.01f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Vector2 position;

    private void Start()
    {
        healthBarController = GameObject.FindGameObjectWithTag("HealtBarPlayer").GetComponent<HealthBarForPlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerThrowsBrickController = GetComponent<PlayerThrowsBrickController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dontMove = true;
    }

    void Update()
    {
        CheckIfPlayerCollideWithEnemy();
        HealtBarStatus();
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    void HandleMove()
    {
        if (dontMove)
        {
            StopMoving();
        } else
        {
            if (moveLeft)
            {
                MoveLeft();
            }
            else if (!moveLeft)
            {
                MoveRight();
            }
        }
    } 

    public void AllowMovment(bool movment)
    {
        dontMove = false;
        moveLeft = movment;

    }

    public void DontAllowMovment()
    {
        dontMove = true;
    }

    public void Jump()
    {
        Vector2 jump = Vector2.up;

        if (CheckGround())
        {
            rb.velocity = jump * playerJumpVelocity;
        }
    }

    void MoveLeft()
    {
        spriteRenderer.flipX = false;
        Vector2 move = Vector2.left;
        position = move; // Position the player in the direction it throws bricks
        rb.velocity = new Vector2(move.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        
    }

    void MoveRight()
    {
        spriteRenderer.flipX = true;
        Vector2 move = Vector2.right;
        position = move; // Position the player in the direction it throws bricks
        rb.velocity = new Vector2(move.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(Mathf.SmoothStep(0f, 1f, Time.fixedDeltaTime), rb.velocity.y);//0f, rb.velocity.y);
    }

    public void ThrowBricks()
    {
            StartCoroutine(playerThrowsBrickController.ThrowsBricks(position));
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        canJump = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        canJump = false;
    //    }
    //}

    //void UpdatePlayerPosition()
    //{
    //    float move = Input.GetAxis("Horizontal");

    //    rb.velocity = new Vector2(gameManager.playerMaxSpeed * move, rb.velocity.y);

    //        if (walkLeft)
    //        {
    //        spriteRenderer.flipX = false;
    //        position = Vector2.left;
    //        }
    //        if (walkRight)
    //        {
    //        spriteRenderer.flipX = true;
    //        position = Vector2.right;
    //        }
    //}

    //void CheckPlayerInput()
    //{
    //    bool inputLeft = Input.GetKey(KeyCode.LeftArrow);
    //    bool inputRight = Input.GetKey(KeyCode.RightArrow);
    //    bool inputJump = Input.GetKey(KeyCode.Space);
    //    bool inputThrow = Input.GetButtonDown("Fire1");

    //    walkLeft = inputLeft && !inputRight;
    //    walkRight = !inputLeft && inputRight;
        
    //    jump = inputJump;
    //    throwBrick = inputThrow;
    //}

    bool CheckGround()
    {
        Vector2 middle = new Vector2(transform.position.x, transform.position.y);  //- (0.65f * 0.5f)
                                                                                   // RaycastHit2D groundMiddle = Physics2D.Raycast(middle, Vector2.down, playerDistanceToGround, groundMask);

        RaycastHit2D groundMiddle = Physics2D.BoxCast(middle, new Vector2(0.8f, 0.8f), 0f, Vector2.down, playerDistanceToGround, groundMask);

        if (groundMiddle.collider == null)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * playerDistanceToGround, new Vector3(0.8f,0.8f, 1f));
    }

    void CheckIfPlayerCollideWithEnemy()
    {
        Vector2 originPlayer = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSide = Physics2D.Raycast(originPlayer, Vector2.left, gameManager.playerHitDistanceLeftAndRightSideOn, gameManager.enemyMask);
        RaycastHit2D rightSide = Physics2D.Raycast(originPlayer, Vector2.right, gameManager.playerHitDistanceLeftAndRightSideOn, gameManager.enemyMask);

        if (leftSide.collider != null || rightSide.collider != null)
        {
            //collideWithEnemy = true;
            healthBarStatus -= gameManager.playerHealthBarStatusSpeed;
            healthBarController.SetStatusOnHealthBar(healthBarStatus);

        }
    }

    void HealtBarStatus()
    {
        if (healthBarStatus < 0.001f)
        {
            Debug.Log("Player dead");
            //gameObject.SetActive(false);
        }
    }
}
