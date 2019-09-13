using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveVelocity;
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool walk, walkLeft, walkRight, jump;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        CheckPlayerInput();
        UpdatePlayerPosition();
    }

    void Jump()
    {
        if (jump)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !jump)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void UpdatePlayerPosition()
    {
        //Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        if (walk)
        {
            if (walkLeft)
            {
                rb.velocity = Vector2.left * moveVelocity;
                //pos.x -= moveVelocity.x * Time.deltaTime;

                //scale.x = -1;
            }

            else if (walkRight)
            {
                rb.velocity = Vector2.right * moveVelocity;
                //pos.x += moveVelocity.x * Time.deltaTime;

                //scale.x = 1;
            }

            //transform.position = pos;
        }
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
}
