using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowController : MonoBehaviour
{

    
    [SerializeField] LayerMask layerMask;
    //[SerializeField] LayerMask brickLayerMask;
    [SerializeField] GameObject brickPrefab;

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
        CheckIfCollideWithPlayerOrBricks();
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
    void CheckIfCollideWithPlayerOrBricks()
    {
        Vector2 topLeft = new Vector2(transform.position.x - 0.8f, transform.position.y);
        Vector2 topMiddle = new Vector2(transform.position.x, transform.position.y);
        Vector2 topRight = new Vector2(transform.position.x + 0.8f, transform.position.y);

        RaycastHit2D hitTopLeft = Physics2D.Raycast(topLeft, Vector2.up, 1f, layerMask);
        RaycastHit2D hitTopMiddle = Physics2D.Raycast(topMiddle, Vector2.up, 1f, layerMask);
        RaycastHit2D hitTopRight = Physics2D.Raycast(topRight, Vector2.up, 1f, layerMask);

        if (hitTopLeft.collider != null || hitTopMiddle.collider != null || hitTopRight.collider != null)
        {
            healthBarStatus -= enemyHealthBarStatusSpeed;
            UpdateHealthBarStatus();
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

    void UpdateHealthBarStatus()
    {
        bar.transform.localScale = new Vector2(healthBarStatus, greenStatusBarHeight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))
        {
            Debug.Log("Hit enemy with " + collision.transform.name);
            healthBarStatus -= 0.34f;
            UpdateHealthBarStatus();
        }
    }
}
