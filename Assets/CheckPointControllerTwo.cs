using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControllerTwo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.runTroughCheckPoint2 = true;
            PlayerController.Instance.runTroughCheckPoint1 = false;
            gameObject.SetActive(false);
        }
    }
}
