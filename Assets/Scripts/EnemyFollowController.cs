using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowController : MonoBehaviour
{

    Transform playerTransform;
    [SerializeField] LayerMask playerLayerMask;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject bar;

    [Range(100, 1000)] public float speed = 400f;
    [Range(0, 0.01f)] public float enemyHealthBarStatusSpeed = 0.005f;
    public float greenStatusBarHeight = 1f;

    private bool collideWithPlayer = false;
    private float healthBarStatus = 1.01f;

    private void Start()
    {

        bar = GameObject.FindGameObjectWithTag("Bar");   //Find("Bar");

        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        WhenEnemyFollowThePlayerChangeScale();
        CheckIfCollideWithPlayer();
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, playerLayerMask);

        if (hit.collider != null)
        {
            Debug.Log("Hit enemy");
            collideWithPlayer = true;
            HealtBarStatus();
        }

        void HealtBarStatus()
        {
            if (collideWithPlayer == true)
            {
                healthBarStatus -= enemyHealthBarStatusSpeed;
                bar.transform.localScale = new Vector2(healthBarStatus, greenStatusBarHeight);
            }
            else if (healthBarStatus <= 0.01f)
            {
                Debug.Log("Enemy DEAD ");
                gameObject.SetActive(false);
                collideWithPlayer = false;
            }
        }
    }

}
