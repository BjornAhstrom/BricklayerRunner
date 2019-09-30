using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HealthBarForPlayerController healthBarController;
    [SerializeField] public PlayerSpawnerController playerSpawnerController;
    [SerializeField] private GameManager gameManager;
    private PlayerThrowsBrickController playerThrowsBrickController;

    private bool walk, walkLeft, walkRight, jump, throwBrick;
    private float healthBarStatus = 1.01f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Vector2 position;
    

    private void Start()
    {
        playerThrowsBrickController = GetComponent<PlayerThrowsBrickController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        UpdatePlayerPosition();
        Jump();
        ThrowBricks();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        CheckIfPlayerCollideWithEnemy();
        HealtBarStatus();
    }

    void Jump()
    {
        if (jump && CheckGround())
          rb.velocity = Vector2.up * gameManager.playerJumpVelocity;
    }

    void UpdatePlayerPosition()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(gameManager.playerMaxSpeed * move, rb.velocity.y);

            if (walkLeft)
            {
            spriteRenderer.flipX = false;
            position = Vector2.left;
            }
            if (walkRight)
            {
            spriteRenderer.flipX = true;
            position = Vector2.right;
            }
    }

    void ThrowBricks()
    {
        if (throwBrick != false)
        {
            StartCoroutine(playerThrowsBrickController.ThrowsBricks(position));
            
        }
        throwBrick = false;
    }

    void CheckPlayerInput()
    {
        bool inputLeft = Input.GetKey(KeyCode.LeftArrow);
        bool inputRight = Input.GetKey(KeyCode.RightArrow);
        bool inputJump = Input.GetKey(KeyCode.Space);
        bool inputThrow = Input.GetButtonDown("Fire1");

        //walk = inputLeft || inputRight;

        walkLeft = inputLeft && !inputRight;
        walkRight = !inputLeft && inputRight;
        
        jump = inputJump;
        throwBrick = inputThrow;
    }

    bool CheckGround()
    {
        Vector2 middle = new Vector2(transform.position.x, transform.position.y );  //- (0.65f * 0.5f)
        RaycastHit2D groundMiddle = Physics2D.Raycast(middle, Vector2.down, gameManager.playerDistanceToGround, gameManager.groundMask);

        if (groundMiddle.collider == null)
        {
            return false;
        }

        return true;
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
