using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPos;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collision is player
        //if (collision.CompareTag(""))
        //{

        //}
        GameObject enemy =  Instantiate(enemyPrefab);
        enemy.transform.position = spawnPos.position;

        gameObject.SetActive(false);
    }

}
