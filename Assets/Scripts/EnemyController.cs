using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    [HideInInspector] public Transform positions;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprite = GetComponent<SpriteRenderer>();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.up * 1f, new Vector3(0.5f, 0.3f, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            CheckIfPlayerJumpOnHead();
        }

        if (collision.transform.CompareTag("Brick") && hit)
        {
            PlayerController.Instance.playerScore += 10;
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
            PlayerController.Instance.playerScore += 10;
            healthBarStatus -= 0.34f;
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
    }
}
