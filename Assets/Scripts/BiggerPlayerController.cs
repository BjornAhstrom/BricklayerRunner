using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerPlayerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Big");
            PlayerController.Instance.MakePlayerBigger();
            gameObject.SetActive(false);
        }
    }
}
