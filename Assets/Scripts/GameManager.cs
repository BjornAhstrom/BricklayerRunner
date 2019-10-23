using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;

    public LayerMask groundMask;
    public LayerMask playerLayerMask;
    public float greenStatusBarHeight = 1f;

    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro gameOverText;
    [SerializeField] GameObject bar;
    //[SerializeField] LifeController lifeController;

    [Range(0, 20)] public float playerMaxSpeed = 10f;
    [Range(0, 30)] public float playerJumpVelocity = 17f;
    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    [Range(0, 10)] public float enemyMoveSpeed = 5f;


    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load<GameObject>("GameManager")).GetComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameOverMenu.SetActive(false);
    }

    private void Update()
    {
        scoreText.text = "Score" + "\n" + PlayerController.Instance.playerScore;
        UpdateHealtbarStatus();
    }

    void UpdateHealtbarStatus()
    {
        bar.transform.localScale = new Vector2(PlayerController.Instance.healthBarStatus, greenStatusBarHeight);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayButton()
    {
        SceneHandler.Instance.ChangeLevelTo(PlayerController.Instance.currentLevel);
        PlayerController.Instance.playerScore = 0;
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
    }

    public void MenuButton()
    {
        SceneHandler.Instance.ChangeLevelTo("MainMenu");
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
    }
}
