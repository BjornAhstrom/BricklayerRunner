using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [Range(0, 1000)] public float playerHitWater = 200f;

    private float playerMoveSpeed;

    private void Start()
    {
        playerMoveSpeed = GameManager.Instance.GetComponent<InputController>().moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit water");
            GameManager.Instance.GetComponent<InputController>().moveSpeed = playerHitWater;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Unhit water");
            GameManager.Instance.GetComponent<InputController>().moveSpeed = playerMoveSpeed;
        }
            
    }
}
