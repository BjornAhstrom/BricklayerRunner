using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowController : MonoBehaviour
{

    
    [SerializeField] LayerMask playerLayerMask;

    Transform playerTransform;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    GameObject bar;

    [Range(100, 1000)] public float speed = 400f;
    [Range(0, 0.1f)] public float enemyHealthBarStatusSpeed = 0.01f;
    public float greenStatusBarHeight = 1f;

    private float healthBarStatus = 1.01f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bar = GameObject.FindGameObjectWithTag("Bar");

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        WhenEnemyFollowThePlayerChangeScale();
        CheckIfCollideWithPlayer();
        HealtBarStatus();
    }


    private void FixedUpdate()
    {
        MoveEnemyInPlayersDirection();
    }

    void MoveEnemyInPlayersDirection()
    {
        Vector2 current = rb.transform.position;
        Vector2 player = playerTransform.position;

        Vector2 direction = new Vector2((player - current).normalized.x, 0);

        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        //rb.velocity = Vector2.ClampMagnitude(rb.velocity, 7);


        rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, rb.velocity.y);

        //rb.MovePosition(current + direction * speed * Time.fixedDeltaTime);

        //rb.MovePosition(Vector2.MoveTowards(current, player, speed * Time.fixedDeltaTime));

        //rb.transform.position = Vector2.MoveTowards(current, player, speed * Time.deltaTime);

        //rb.MovePosition((Vector2)transform.position + (direction * enemyMoveSpeed * Time.deltaTime));
    }

    void WhenEnemyFollowThePlayerChangeScale()
    { 

        if (playerTransform.position.x + 1 >= transform.position.x)
        {
            spriteRenderer.flipX = true;

        }
        else if (playerTransform.position.x <= transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    // If player jump on enemy, the enemy will disappear
    void CheckIfCollideWithPlayer()
    {
        Vector2 left = new Vector2(transform.position.x, transform.position.y);
        Vector2 middle = new Vector2(transform.position.x, transform.position.y);
        Vector2 right = new Vector2(transform.position.x + 0.8f, transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(left, Vector2.up, 1f, playerLayerMask);
        RaycastHit2D hitMiddle = Physics2D.Raycast(middle, Vector2.up, 1f, playerLayerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(right, Vector2.up, 1f, playerLayerMask);

        if (hitLeft.collider != null || hitMiddle.collider != null || hitRight.collider != null)
        {
            healthBarStatus -= enemyHealthBarStatusSpeed;
            bar.transform.localScale = new Vector2(healthBarStatus, greenStatusBarHeight);
        }
    }

    void HealtBarStatus()
    {
        if (healthBarStatus <= 0.01f)
        {
            Debug.Log("Enemy DEAD ");
            gameObject.SetActive(false);
        }
    }

}
