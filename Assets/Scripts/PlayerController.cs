using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask enemyMask;
    [HideInInspector] public int playerScore = 0;
    public LifeController lifeController;

    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    [Range(0, 0.01f)] public float playerHealthBarStatusSpeed = 0.005f;
    [Range(0, 5)] public float playerHitDistanceLeftAndRightSideOn = 1.2f;

    public float greenStatusBarHeight = 1f;
    public float healthBarStatus = 1f;
    public bool gameOver = false;
    public int startLives = 3;

    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("Player")).GetComponent<PlayerController>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        playerScore = 0;

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        lifeController = GameObject.FindGameObjectWithTag("Life").GetComponent<LifeController>();
        lifeController.InitializeLife(startLives);
    }

    void Update()
    {
        CheckIfPlayerCollideWithEnemy();
        HealtBarStatus();
    }

    public bool CheckGround()
    {
        Vector2 middle = new Vector2(transform.position.x, transform.position.y); 

        RaycastHit2D groundMiddle = Physics2D.BoxCast(middle, new Vector2(0.3f, 0.3f), 0, Vector2.down, playerDistanceToGround, groundMask);

        if (groundMiddle.collider == null)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * playerDistanceToGround, new Vector3(0.3f,0.3f, 0));
    }

    void CheckIfPlayerCollideWithEnemy()
    {
        Vector2 originPlayer = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSide = Physics2D.Raycast(originPlayer, Vector2.left, playerHitDistanceLeftAndRightSideOn, enemyMask);
        RaycastHit2D rightSide = Physics2D.Raycast(originPlayer, Vector2.right, playerHitDistanceLeftAndRightSideOn, enemyMask);

        if (leftSide.collider != null || rightSide.collider != null)
        {
            healthBarStatus -= playerHealthBarStatusSpeed;
        }
    }

    public void HealtBarStatus()
    {
        if (healthBarStatus < 0.001f)
        {
            Debug.Log("Loos life");
            lifeController.RemoveLives();
            healthBarStatus = 1.0f;
            startLives--;

            if (startLives <= 0)
            {

                Debug.Log("Dead");
                //gameObject.SetActive(false);
                //gameOver = true;
                //healthBarStatus = 1.0f;

                //SceneManager.LoadScene("MainMenu");
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            //Debug.Log("Hit the " + collision.name);
            //moveSpeed = 150f;
            //jumpForce = 2.5f;
        }
        else if (collision.CompareTag("Grass"))
        {
            //moveSpeed = 450f;
            //jumpForce = 5f;
        }
        else if (collision.CompareTag("OverWater"))
        {
            //jumpForce = 1f;
        }
    }

}
