using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject bar;

    [HideInInspector] public Transform positions;

    //[Range(100, 1000)] public float speed = 400f;
    [Range(0, 0.1f)] public float enemyHealthBarStatusSpeed = 0.01f;
    [Range(0, 5)] public float boxColliderWidth = 0.5f;
    [Range(0, 5)] public float boxColliderHeight = 0.3f;
    //[Range(-5, 5)] public float boxColliderMoveLeftOrRight = 0f;
    //[Range(-5, 5)] public float boxColliderMoveUpOrDown = 0f;
    [Range(0, 10)] public float boxColliderHitDistance = 1f;
    [Range(0, 5)] public float greenStatusBarHeight = 1f;
    public int nrOfHits = 5;
    public bool enemyIsAlive = false;


    private SpriteRenderer spriteRenderer;
    private SpriteRenderer sprite;
    private float healthBarStatus = 1f;
    private float enemyHeight;
    private float deathDelay = 0.2f;
    private bool hit = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>();
        enemyIsAlive = true;
    }

    private void Update()
    {
        enemyHeight = transform.position.y / 2;
        WhenEnemyFollowThePlayerFlipXValue();
        HealtBarStatus();
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
        if (collision.transform.CompareTag("Player") && enemyIsAlive == true)
        {
            CheckIfPlayerJumpOnHead();
        }

        if (collision.transform.CompareTag("Brick") && hit && enemyIsAlive == true)
        {
            PlayerController.Instance.playerScore += 10;
            healthBarStatus -= (1f / (float) nrOfHits);
            UpdateHealthBarStatus();
            
            hit = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);//boxColliderMoveLeftOrRight, boxColliderMoveUpOrDown);

        Gizmos.DrawWireCube(origin, new Vector3(boxColliderWidth, boxColliderHeight, 0));
    }

    void CheckIfPlayerJumpOnHead()
    {
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);//boxColliderMoveLeftOrRight, boxColliderMoveUpOrDown);

        RaycastHit2D checkPlayerOnHead = Physics2D.BoxCast(origin, new Vector2(boxColliderWidth, boxColliderHeight), 0, Vector2.up, boxColliderHitDistance, layerMask);

        if (checkPlayerOnHead.collider != null && hit != false)
        {
            PlayerController.Instance.playerScore += 10;
            healthBarStatus -= (1f / (float)nrOfHits);
            UpdateHealthBarStatus();
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

        if (gameObject.CompareTag("Boss1"))
        {
            SceneHandler.Instance.ChangeLevelTo("Level2");
        }
    }
}
