using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControllerOne : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.runTroughCheckPoint1 = true;
            PlayerController.Instance.runTroughCheckPoint2 = false;
            gameObject.SetActive(false);
        }
    }
}
