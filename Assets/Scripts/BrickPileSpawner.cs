using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPileSpawner : MonoBehaviour
{
    [SerializeField] GameObject brickPilePrefab;
    [SerializeField] Transform spawnPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject brickPile = Instantiate(brickPilePrefab);
            brickPile.transform.position = spawnPosition.position;

            gameObject.SetActive(false);
        }
    }
}
