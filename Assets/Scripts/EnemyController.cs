using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] GameObject bar;

    [HideInInspector] public Transform positions;

    //[Range(100, 1000)] public float speed = 400f;
    [Range(0, 0.1f)] public float enemyHealthBarStatusSpeed = 0.01f;
    [Range(0, 5)] public float boxColliderWidth = 0.5f;
    [Range(0, 5)] public float boxColliderHeight = 0.3f;
    [Range(0, 10)] public float boxColliderHitDistance = 1f;
    [Range(0, 5)] public float greenStatusBarHeight = 1f;
    public int nrOfHits = 5;
    //public bool enemyIsAlive = false;
    public bool jumpOnHead = false;
    public bool runRight = false;
    public bool runLeft = false;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer sprite;
    private float healthBarStatus = 1f;
    private float enemyHeight;
    private YieldInstruction deathDelay = new WaitForSeconds(0.2f);
    private bool hit = true;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        //enemyIsAlive = true;
    }

    private void Update()
    {
        enemyHeight = transform.position.y / 2;
        WhenEnemyFollowThePlayerFlipXValue();
        HealtBarStatus();
        //    Debug.Log("Run left is " + runLeft);
    }

    void WhenEnemyFollowThePlayerFlipXValue()
    {
        if (PlayerController.Instance.transform.position.x + 1 >= transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (PlayerController.Instance.transform.position.x <= transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) // && enemyIsAlive == true)
        {
            CheckIfPlayerJumpOnHead();
        }

        if (collision.transform.CompareTag("Brick") && hit) // && enemyIsAlive == true)
        {
            PlayerController.Instance.playerScore += 10;
            healthBarStatus -= (1f / (float) nrOfHits);
            UpdateHealthBarStatus();
            
            hit = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        Gizmos.DrawWireCube(origin, new Vector3(boxColliderWidth, boxColliderHeight, 0));

        Gizmos.color = Color.red;

        Vector2 originEnemy = new Vector2(transform.position.x, transform.position.y);
        Gizmos.DrawRay(originEnemy, Vector2.left*100);
        Gizmos.DrawRay(originEnemy, Vector2.right*100);
    }

    public void CheckIfPlayerJumpOnHead()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D checkIfPlayerJumpOnHead = Physics2D.BoxCast(origin, new Vector2(boxColliderWidth, boxColliderHeight), 0, Vector2.up, boxColliderHitDistance, layerMask);

        if (checkIfPlayerJumpOnHead.collider != null && hit != false)
        {
            checkIfEnemyCanRunToLeftOrRight();
            PlayerController.Instance.playerScore += 10;
            healthBarStatus -= (1f / (float)nrOfHits);
            UpdateHealthBarStatus();
            jumpOnHead = true;
        }
    }

    void checkIfEnemyCanRunToLeftOrRight()
    {
        Vector2 originEnemy = new Vector2(gameObject.transform.position.x, transform.position.y);
        
        RaycastHit2D hitWallLeftSide = Physics2D.Raycast(originEnemy, Vector2.left, Mathf.Infinity ,wallLayerMask);
        RaycastHit2D hitWallRightSide = Physics2D.Raycast(originEnemy, Vector2.right, Mathf.Infinity ,wallLayerMask);
        
        if (hitWallRightSide.collider != null && hitWallLeftSide.collider != null)
        {
            if (hitWallRightSide.distance > hitWallLeftSide.distance)
            {
                // Gå till höger
                runRight = true;

            }
            else if(hitWallLeftSide.distance < hitWallRightSide.distance)
            {
                // Gå till vänster
                runLeft = true;
            }
        }
        else if (hitWallLeftSide.collider != null)
        {
            // Gå till höger
            runRight = true;
        }
        else if ( hitWallRightSide.collider != null)
        {
            // Gå till vänster
            runLeft = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Brick") && hit == false)
        {
            StartCoroutine(SetHitTo(true));
        }
    }

    void HealtBarStatus()
    {
        if (healthBarStatus <= enemyHealthBarStatusSpeed)
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

        yield return deathDelay;
        healthBarColor.a = 0;
        healtBar.color = healthBarColor;
        color.a = 0;
        sprite.color = color;

        yield return deathDelay;
        healthBarColor.a = 1;
        healtBar.color = healthBarColor;
        color.a = 1;
        sprite.color = color;

        yield return deathDelay;
        healthBarColor.a = 0;
        healtBar.color = healthBarColor;
        color.a = 0;
        sprite.color = color;

        gameObject.SetActive(false);

        if (gameObject.CompareTag("Boss1"))
        {
            SceneHandler.Instance.ChangeLevelTo("Level2");
        }
    }
}
