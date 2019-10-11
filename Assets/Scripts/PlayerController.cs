using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //private PlayerThrowsBrickController playerThrowsBrickController;
    [SerializeField] HealthBarForPlayerController healthBarController;
    [SerializeField] EnemyController enemyController;
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask enemyMask;
    
    
    //public GameObject bar;

    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    //[Range(0, 30)] public float playerJumpVelocity = 17f;
    [Range(0, 0.01f)] public float playerHealthBarStatusSpeed = 0.005f;
    [Range(0, 5)] public float playerHitDistanceLeftAndRightSideOn = 1.2f;
    //[Range(100, 1000)] public float moveSpeed = 450f;


    public Vector2 scoreTextOffset;
    public float jumpForce = 5f;
    public float greenStatusBarHeight = 1f;
    [HideInInspector] public int playerScore = 0;
    //public bool moveLeft;
    //public bool dontMove;

    public float healthBarStatus = 1.01f;

    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Player")).GetComponent<PlayerController>();
            }

            return _instance;
        }
    }

    //private bool walk, walkLeft, walkRight, jump, throwBrick;
    
    
    //SpriteRenderer spriteRenderer;
    //Rigidbody2D rb;
    //Vector2 position;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //playerThrowsBrickController = GetComponent<PlayerThrowsBrickController>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        //dontMove = true;
        playerScore = 0;
    }

    void Update()
    {
        CheckIfPlayerCollideWithEnemy();
        HealtBarStatus();
        //UpdateScoreText(playerScore);
    }

    //private void FixedUpdate()
    //{
    //    HandleMove();
    //}

    //void HandleMove()
    //{
    //    if (dontMove)
    //    {
    //        StopMoving();
    //    } else
    //    {
    //        if (moveLeft)
    //        {
    //            MoveLeft();
    //        }
    //        else if (!moveLeft)
    //        {
    //            MoveRight();
    //        }
    //    }
    //} 

    //public void AllowMovment(bool movment)
    //{
    //    dontMove = false;
    //    moveLeft = movment;
    //}

    //public void DontAllowMovment()
    //{
    //    dontMove = true;
    //}

    //public void Jump()
    //{
    //    Vector2 jump = Vector2.up;

    //    if (CheckGround())
    //    {
    //        rb.velocity = jump * playerJumpVelocity;
    //    }
    //}

    //void MoveLeft()
    //{
    //    spriteRenderer.flipX = false;
    //    Vector2 move = Vector2.left;
    //    position = move; // Position the player in the direction it throws bricks
    //    rb.velocity = new Vector2(move.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    //}

    //void MoveRight()
    //{
    //    spriteRenderer.flipX = true;
    //    Vector2 move = Vector2.right;
    //    position = move; // Position the player in the direction it throws bricks
    //    rb.velocity = new Vector2(move.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
    //}

    //void StopMoving()
    //{
    //    rb.velocity = new Vector2(Mathf.SmoothStep(0f, 1f, Time.fixedDeltaTime), rb.velocity.y);//0f, rb.velocity.y);
    //}

    //public void ThrowBricks()
    //{
    //        StartCoroutine(playerThrowsBrickController.ThrowsBricks(position));
    //}

    public bool CheckGround()
    {
        Vector2 middle = new Vector2(transform.position.x, transform.position.y);  //- (0.65f * 0.5f)
                                                                                   // RaycastHit2D groundMiddle = Physics2D.Raycast(middle, Vector2.down, playerDistanceToGround, groundMask);

        RaycastHit2D groundMiddle = Physics2D.BoxCast(middle, new Vector2(0.3f, 0.3f), 0, Vector2.down, playerDistanceToGround, groundMask);

        if (groundMiddle.collider == null)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * playerDistanceToGround, new Vector3(0.3f,0.3f, 0));
    }

    void CheckIfPlayerCollideWithEnemy()
    {
        Vector2 originPlayer = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSide = Physics2D.Raycast(originPlayer, Vector2.left, playerHitDistanceLeftAndRightSideOn, enemyMask);
        RaycastHit2D rightSide = Physics2D.Raycast(originPlayer, Vector2.right, playerHitDistanceLeftAndRightSideOn, enemyMask);

        if (leftSide.collider != null || rightSide.collider != null)
        {
            healthBarStatus -= playerHealthBarStatusSpeed;
            //UpdateHealtBarStatus();

        }
    }

    public void HealtBarStatus()
    {
        if (healthBarStatus < 0.001f)
        {
           // Debug.Log("Player dead");
            //gameObject.SetActive(false);
        }
    }

    //void UpdateHealtBarStatus()
    //{
    //    bar.transform.localScale = new Vector2(healthBarStatus, greenStatusBarHeight);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit the " + collision.name);

        if (collision.CompareTag("Water"))
        {
            //Debug.Log("Hit the " + collision.name);
            //moveSpeed = 150f;
            jumpForce = 2.5f;
        }
        else if (collision.CompareTag("Grass"))
        {
            //moveSpeed = 450f;
            jumpForce = 5f;
        }
        else if (collision.CompareTag("OverWater"))
        {
            jumpForce = 1f;
        }
    }

}
