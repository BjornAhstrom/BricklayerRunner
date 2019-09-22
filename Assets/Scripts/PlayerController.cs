using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HealthBarController healthBar;

    public Vector2 moveVelocity;
    public float maxSpeed = 10;
    public float jumpVelocity;
    public LayerMask groundMask;
    public LayerMask enemyMask;

    [Range(0, 10)] public float distanceToGround = 2f;

    private bool walk, walkLeft, walkRight, jump;
    private bool hitByEnemy = false;
    private float healthBarStatus = 1.01f;

    Rigidbody2D rb;
    Vector3 scale;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        UpdatePlayerPosition();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        Jump();
        //UpdatePlayerPosition();
        CheckIfPlayerCollideWithEnemy();
    }

    void Jump()
    {
        if (jump && CheckGround())
          rb.velocity = Vector2.up * jumpVelocity;
    }

    

    void UpdatePlayerPosition()
    {
        scale = transform.localScale;

        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(maxSpeed * move, rb.velocity.y);

            if (walkLeft)
            {
                //Debug.Log("Left");
            scale.x = 0.3f;
            }
            if (walkRight)
            {
                //Debug.Log("right");
                scale.x = -0.3f;
            }
        transform.localScale = scale;
    }

    void CheckPlayerInput()
    {
        bool inputLeft = Input.GetKey(KeyCode.LeftArrow);
        bool inputRight = Input.GetKey(KeyCode.RightArrow);
        bool inputJump = Input.GetKey(KeyCode.Space);

        walk = inputLeft || inputRight;

        walkLeft = inputLeft && !inputRight;
        walkRight = !inputLeft && inputRight;

        jump = inputJump;
    }

    bool CheckGround()
    {
        Vector2 middle = new Vector2(transform.position.x, transform.position.y );  //- (0.65f * 0.5f)
        RaycastHit2D groundMiddle = Physics2D.Raycast(middle, Vector2.down, distanceToGround, groundMask);

        if (groundMiddle.collider == null)
        {
            //Debug.Log("No ground");
            return false;
        }

        return true;
    }

    void CheckIfPlayerCollideWithEnemy()
    {
        Vector2 left = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSide = Physics2D.Raycast(left, Vector2.left, 1.2f, enemyMask);

        if (leftSide.collider != null)
        {
            Debug.Log("Hit with enemy");
            hitByEnemy = true;
            StartCoroutine(Test());
            
        }
        else if (healthBarStatus <= 0.01f)
        {
            StopAllCoroutines();
            hitByEnemy = false;
        }
    }

    IEnumerator Test()
    {
        if (hitByEnemy == true)
        {
            healthBarStatus -= 0.01f;
            healthBar.SetStatusOnHealthBar(healthBarStatus);
            hitByEnemy = false;
            yield return new WaitForSeconds(1);
        }
    }
}
