using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayerBiggerSpawnerController : MonoBehaviour
{
    [SerializeField] GameObject biggerPlayerPrefab;
    [SerializeField] Transform positionTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject biggerPrefab = Instantiate(biggerPlayerPrefab);
            biggerPrefab.transform.position = positionTransform.position;
            gameObject.SetActive(false);
        }
    }
}
