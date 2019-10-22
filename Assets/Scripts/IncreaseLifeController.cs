using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLifeController : MonoBehaviour
{
    [Range(0, 1)] public float increaseLife = 0.08f;

    private float playerHealthBar = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.Instance.healthBarStatus < playerHealthBar)
        {
            PlayerController.Instance.healthBarStatus += increaseLife;

            if (PlayerController.Instance.healthBarStatus > playerHealthBar)
            {
                PlayerController.Instance.healthBarStatus = playerHealthBar;
            }
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
