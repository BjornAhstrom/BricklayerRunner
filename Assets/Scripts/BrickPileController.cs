using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPileController : MonoBehaviour
{
    PlayerThrowsBrickController throwsBrickController;

    [Range(0, 10)] public int bricksAmount = 4;

    private void Start()
    {
        throwsBrickController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerThrowsBrickController>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            throwsBrickController.CollectBricks(bricksAmount);
            throwsBrickController.initializeBrickPrefabObject();

            GameObject brickPile = transform.gameObject;
            Destroy(brickPile);

        }
    }
}
