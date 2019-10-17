using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLifeSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Transform positionTransform;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject heart = Instantiate(heartPrefab);
            heart.transform.position = positionTransform.position;
            gameObject.SetActive(false);
        }
    }
}
