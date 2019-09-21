using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveVelocity;
    public float maxSpeed = 10;
    public float jumpVelocity;
    //public float fallMultiplier = 2.5f;
    //public float lowJumpMultiplier = 2f;
    public LayerMask groundMask;
    [Range(0, 10)]
    public float distanceToGround = 2f;

    private bool walk, walkLeft, walkRight, jump;

    Rigidbody2D rb;
    Vector3 scale;

    private void Start()
    {
        
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
    }

    void Jump()
    {
        if (jump && CheckGround())
          rb.velocity = Vector2.up * jumpVelocity;
    }

    private void FixedUpdate()
    {
        UpdatePlayerPosition();
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








    //void UpdatePlayerPosition()
    //{
    //    //Vector3 pos = transform.localPosition;
    //    Vector3 scale = transform.localScale;

    //    if (walk)
    //    {
    //        if (walkLeft)
    //        {
    //            rb.velocity = Vector2.left * moveVelocity;

    //            //Debug.Log("Pressed left");
    //            scale.x = -1;
    //        }

    //        else if (walkRight)
    //        {
    //            rb.velocity = Vector2.right * moveVelocity;

    //            //Debug.Log("Pressed right");
    //            scale.x = 1;
    //        }
    //    }
    //}

}
