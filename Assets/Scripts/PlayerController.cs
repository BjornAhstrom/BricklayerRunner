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
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public bool runTroughCheckPoint1 = false;
    [HideInInspector] public bool runTroughCheckPoint2 = false;
    [HideInInspector] public bool playerDied = false;

    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    [Range(0, 0.01f)] public float playerHealthBarStatusSpeed = 0.005f;
    [Range(0, 5)] public float playerHitDistanceLeftAndRightSideOn = 1.2f;
    [Range(0, 5)] public float makeplayerBigger = 1.4f;
    [Range(0, 5)] public float boxCastWidth = 0.3f;
    [Range(0, 5)] public float boxCastHeigt = 0.3f;
    [Range(0, 2)] public float healthBarStatus = 1f;
    [Range(0, 100)] public float greenStatusBarHeight = 1f;

    public string currentLevel = "Level1";
    public int startLives = 3;
    public int currentLives;

    private float playerOriginalSize = 0.3f;
    private float bigPlayerHealtBarStatusSpeed = 0;
    private float smallPlayerHealtBarStatusSpeed = 0.005f;
    private int resetLives;
    private float resetHealthBarStatus;

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
        currentLives = startLives;

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

    void Update()
    {
        CheckIfPlayerCollideWithEnemy();
        HealtBarStatus();
    }

    public bool CheckGround()
    {
        Vector2 middle = new Vector2(transform.position.x, transform.position.y); 

        RaycastHit2D groundMiddle = Physics2D.BoxCast(middle, new Vector2(boxCastWidth, boxCastHeigt), 0, Vector2.down, playerDistanceToGround, groundMask);

        if (groundMiddle.collider == null)
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * playerDistanceToGround, new Vector3(boxCastWidth, boxCastHeigt, 0));
    }

    void CheckIfPlayerCollideWithEnemy()
    {
        Vector2 originPlayer = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D leftSide = Physics2D.Raycast(originPlayer, Vector2.left, playerHitDistanceLeftAndRightSideOn, enemyMask);
        RaycastHit2D rightSide = Physics2D.Raycast(originPlayer, Vector2.right, playerHitDistanceLeftAndRightSideOn, enemyMask);

        if (leftSide.collider != null || rightSide.collider != null)
        {
            healthBarStatus -= playerHealthBarStatusSpeed;
            MakePlayerSmaller();
        }
    }

    public void HealtBarStatus()
    {
        if (healthBarStatus < 0)
        {
            currentLives--;
            GameManager.Instance.GetComponentInChildren<LifeController>().RemoveLives();
            playerDied = true;
            healthBarStatus = 1.0f;

            SceneManager.LoadScene(currentLevel);
            
            if (currentLives < 0)
            {
                healthBarStatus = 0;
                SaveScoreToDevice();
                gameObject.SetActive(false);
                healthBarStatus = 1f;
                startLives = 3;
                GameManager.Instance.GameOver();
            }
        }
    }

    public void MakePlayerBigger()
    {
        playerHealthBarStatusSpeed = bigPlayerHealtBarStatusSpeed;
        transform.localScale = new Vector2(playerOriginalSize * makeplayerBigger, playerOriginalSize * makeplayerBigger);
        playerDistanceToGround *= makeplayerBigger;
    }

    public void MakePlayerSmaller()
    {
        playerHealthBarStatusSpeed = smallPlayerHealtBarStatusSpeed;
        transform.localScale = new Vector2(playerOriginalSize, playerOriginalSize);
        playerDistanceToGround = 1.2f;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Water"))
    //    {
    //        Debug.Log("Hit the " + collision.name);
    //        GameManager.Instance.GetComponent<InputController>().moveSpeed = 200f;
    //        //moveSpeed = 150f;
    //        //jumpForce = 2.5f;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Water"))
    //    {
    //        Debug.Log("Not hit the " + collision.name);
    //        GameManager.Instance.GetComponent<InputController>().moveSpeed = 450f;
    //    }
    //}

    public void SaveScoreToDevice()
    {
        PlayerPrefs.SetInt("NewScore", playerScore);
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
        }
        else
        {
            int highScore = PlayerPrefs.GetInt("HighScore");
            if (highScore < playerScore)
            {
                PlayerPrefs.SetInt("HighScore", playerScore);
            }
        }
    }
}
