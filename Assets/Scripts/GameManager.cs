using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public Vector2 playerMoveVelocity;
    public LayerMask groundMask;
    public LayerMask playerLayerMask;
    public float greenStatusBarHeight = 1f;

    [SerializeField] TextMeshPro scoreText;
    [SerializeField] GameObject bar;

    [Range(0, 20)] public float playerMaxSpeed = 10f;
    [Range(0, 30)] public float playerJumpVelocity = 17f;
    [Range(0, 10)] public float playerDistanceToGround = 1.2f;
    [Range(0, 10)] public float enemyMoveSpeed = 5f;


    private void Update()
    {
        scoreText.text = "Score" + "\n" + PlayerController.Instance.playerScore;
        UpdateHealtbarStatus();
    }

    void UpdateHealtbarStatus()
    {
        bar.transform.localScale = new Vector2(PlayerController.Instance.healthBarStatus, greenStatusBarHeight);
    }
}
