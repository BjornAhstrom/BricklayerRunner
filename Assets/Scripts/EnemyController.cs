using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    //[SerializeField] GameObject brickPrefab;

    [HideInInspector] public Transform positions;

    //[SerializeField] PlayerController playerController;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    SpriteRenderer sprite;
    public GameObject bar;
    [SerializeField] public int playerScore = 0;
    
    float enemyHeight;
    float deathDelay = 0.2f;

    [Range(100, 1000)] public float speed = 400f;
    [Range(0, 0.1f)] public float enemyHealthBarStatusSpeed = 0.01f;
    public float greenStatusBarHeight = 1f;

    private float healthBarStatus = 1.01f;
    private bool hit = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>();

        //if (playerController == null)
        //{
        //    playerController = GetComponent<PlayerController>(); //GameObject.FindGameObjectWithTag("Player");
        //}
    }

    private void Update()
    {
        enemyHeight = transform.position.y / 2;
        WhenEnemyFollowThePlayerFlipXValue();
        HealtBarStatus();
    }


    //private void FixedUpdate()
    //{
    //    //MoveEnemyInPlayersDirection();
    //}

    //void MoveEnemyInPlayersDirection()
    //{
    //    Vector2 current = rb.transform.position;
    //    Vector2 player = playerController.transform.position; // playerTransform.position;

    //    Vector2 direction = new Vector2((player - current).normalized.x, 0);

    //    //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    //    //rb.velocity = Vector2.ClampMagnitude(rb.velocity, 7);


    //    rb.velocity = new Vector2(direction.x * speed * Time.fixedDeltaTime, rb.velocity.y);

    //    //rb.MovePosition(current + direction * speed * Time.fixedDeltaTime);

    //    //rb.MovePosition(Vector2.MoveTowards(current, player, speed * Time.fixedDeltaTime));

    //    //rb.transform.position = Vector2.MoveTowards(current, player, speed * Time.deltaTime);

    //    //rb.MovePosition((Vector2)transform.position + (direction * enemyMoveSpeed * Time.deltaTime));
    //}

    void WhenEnemyFollowThePlayerFlipXValue()
    {
        Vector2 scale = transform.localScale;

        if (PlayerController.Instance.transform.position.x + 1 >= transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (PlayerController.Instance.transform.position.x <= transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    //// If player jump on enemy, the enemy will disappear
    //void CheckIfCollideWithPlayerOrBricks()
    //{
    //    Vector2 topLeft = new Vector2(transform.position.x - 0.8f, transform.position.y);
    //    Vector2 topMiddle = new Vector2(transform.position.x, transform.position.y);
    //    Vector2 topRight = new Vector2(transform.position.x + 0.8f, transform.position.y);

    //    RaycastHit2D hitTopLeft = Physics2D.Raycast(topLeft, Vector2.up, 1f, layerMask);
    //    RaycastHit2D hitTopMiddle = Physics2D.Raycast(topMiddle, Vector2.up, 1f, layerMask);
    //    RaycastHit2D hitTopRight = Physics2D.Raycast(topRight, Vector2.up, 1f, layerMask);

    //    if (hitTopLeft.collider != null || hitTopMiddle.collider != null || hitTopRight.collider != null)
    //    {
    //        healthBarStatus -= enemyHealthBarStatusSpeed;
    //        UpdateHealthBarStatus();
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.up * 1f, new Vector3(0.5f, 0.3f, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Vector2 direction = collision.transform.position - transform.position;

            //Debug.Log("Head hit 1");

            //if (direction.y > enemyHeight)
            //{
            //    Debug.Log("Head hit 2");

            //    healthBarStatus -= 0.34f;
            //    UpdateHealthBarStatus();
            //    playerController.GetComponent<PlayerController>().playerScore += 10;

            //    hit = false;
            //}
            CheckIfPlayerJumpOnHead();
        }

        if (collision.transform.CompareTag("Brick") && hit)
        {
            PlayerController.Instance.playerScore += 10;
            //Debug.Log("Enemy hit " + playerScore + " points");
            healthBarStatus -= 0.34f;
            UpdateHealthBarStatus();
            

            hit = false;
        }
    }

    void CheckIfPlayerJumpOnHead()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D checkPlayerOnHead = Physics2D.BoxCast(origin, new Vector2(0.5f, 0.3f), 0, Vector2.up, 1f, layerMask);

        if (checkPlayerOnHead.collider != null && hit != false)
        {
            //Debug.Log("Player jump on head");
            PlayerController.Instance.playerScore += 10;
            healthBarStatus -= 0.34f;
            UpdateHealthBarStatus();
            
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Brick") && hit == false) // || collision.transform.CompareTag("Player")
        {
            StartCoroutine(SetHitTo(true));
        }
    }

    void HealtBarStatus()
    {
        if (healthBarStatus <= 0.01f)
        {
            StartCoroutine(InactivateEnemy());
            StartCoroutine(SetHitTo(true));
        }
    }

    void UpdateHealthBarStatus()
    {
        bar.transform.localScale = new Vector2(healthBarStatus, greenStatusBarHeight);
    }

IEnumerator SetHitTo(bool trueOrFalse)
    {
        yield return new WaitForSeconds(0.2f);
        hit = trueOrFalse;
    }

    IEnumerator InactivateEnemy()
    {
        Color color = sprite.color;
        SpriteRenderer healtBar = GetComponent<SpriteRenderer>();

        Color healthBarColor = healtBar.color;

        yield return new WaitForSeconds(deathDelay);
        healthBarColor.a = 0;
        healtBar.color = healthBarColor;
        color.a = 0;
        sprite.color = color;

        yield return new WaitForSeconds(deathDelay);
        healthBarColor.a = 1;
        healtBar.color = healthBarColor;
        color.a = 1;
        sprite.color = color;

        yield return new WaitForSeconds(deathDelay);
        healthBarColor.a = 0;
        healtBar.color = healthBarColor;
        color.a = 0;
        sprite.color = color;

        gameObject.SetActive(false);
    }
}
