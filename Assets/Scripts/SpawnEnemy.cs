﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPos;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collision is player
        if (collision.CompareTag("Player"))
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPos.position;

            gameObject.SetActive(false);
        }
        
        
    }

}
