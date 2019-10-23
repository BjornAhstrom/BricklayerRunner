using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Range(0, 30)] public float playerJumpVelocity = 17f;
    [Range(100, 1000)] public float moveSpeed = 450f;

    public bool moveLeft;
    public bool dontMove;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Vector2 position;

    private void Start()
    {
        rb = PlayerController.Instance.GetComponent<Rigidbody2D>();
        spriteRenderer = PlayerController.Instance.GetComponent<SpriteRenderer>();

        dontMove = true;
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
        }
        else
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

        if (PlayerController.Instance.CheckGround())
        {
            SoundManager.Instance.JumpSound();
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
        rb.velocity = new Vector2(0f, rb.velocity.y); //new Vector2(Mathf.SmoothStep(0f, 1f, Time.fixedDeltaTime), rb.velocity.y);
    }

    public void ThrowBricks()
    {
        StartCoroutine(GameManager.Instance.GetComponent<PlayerThrowsBrickController>().ThrowsBricks(position));
    }









































    //    public Vector3 offset;
    //    Vector3 mainCameraPosition;

    //    public delegate void ButtonPressed();
    //    public static event ButtonPressed AButton;
    //    public static event ButtonPressed BButton;
    //    public static event ButtonPressed UpArrowButton;
    //    public static event ButtonPressed DownArrowButton;
    //    public static event ButtonPressed LeftArrowButton;
    //    public static event ButtonPressed RightArrowButton;

    //    private void LateUpdate()
    //    {
    //        mainCameraPosition = Camera.main.transform.position;
    //        transform.position = new Vector3(mainCameraPosition.x + offset.x, mainCameraPosition.y + offset.y);
    //    }

    //#if (UNITY_IOS || UNITY_ANDROID)
    //    private void Update()
    //    {
    //        foreach(Touch touch in Input.touches)
    //        {
    //            if (touch.phase == TouchPhase.Began)
    //            {
    //                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

    //                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

    //                if (LeftArrowButton != null && hit.collider != null && hit.collider.tag == "LeftArrowButton")
    //                {
    //                    //Debug.Log("Left press");
    //                    LeftArrowButton();
    //                }
    //                else if (RightArrowButton != null && hit.collider != null && hit.collider.tag == "RightArrowButton")
    //                {
    //                    //Debug.Log("Right press");
    //                    RightArrowButton();
    //                }
    //            }
    //        }
    //    }
    //#elif (UNITY_EDITOR)

    //    private void Update()
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

    //            if (LeftArrowButton != null && hit.collider != null && hit.collider.tag == "LeftArrowButton")
    //            {
    //                //Debug.Log("Left press");
    //                LeftArrowButton();
    //            }
    //            else if (RightArrowButton != null && hit.collider != null && hit.collider.tag == "RightArrowButton")
    //            {
    //                //Debug.Log("Right press");
    //                RightArrowButton();
    //            }
    //        }
    //    }
    //#endif
}
