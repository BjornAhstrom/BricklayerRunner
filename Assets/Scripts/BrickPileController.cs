using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPileController : MonoBehaviour
{
    [SerializeField] PlayerThrowsBrickController throwsBrickController;
    
    private int bricksAmount = 4;

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
